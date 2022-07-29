using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    //tap into lighting system, whenever an hour goes by, darken the lighting
    public float _timeSpeed = 0.25f; //the speed at which time passes
    public float _initialTime = 16.52f; //the time at which the game starts
    private float _minutes; //the current minute
    private float _hour; //the current hour
    [SerializeField, Range(0, 24)] private float _time; //the current time, with range of 0 to 24, slider in inspector 

    public GameObject _TimeGUI;
    public GameObject _globalLighting;
    private UnityEngine.Rendering.Universal.Light2D _light;

    GameObject _keepAlive; //get the keepAliveInScenes instance 
    private string currentTime;

    void Start()
    {
        _keepAlive = GameObject.FindGameObjectWithTag("MainManager"); //find the keepAliveInScenes.cs 
        
        //get the global lighting
        //! this needs to be consistent between scenes
         _light = _globalLighting.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        
        //set the _time to _initialTime
        // _time = _initialTime;
        //check if theres currentTime data in the keepAliveInScenes.cs script
        currentTime = _keepAlive.GetComponent<keepAliveInScenes>().GetData("currentTime");
        if(currentTime != null)
        {
            //parse the returned string to a float of _time
            _time = float.Parse(currentTime);
        }
        else //if currentTime is null, its the first time the game is loaded
        {
            StartCoroutine(__initializeTime());
        }
        //start the coroutine cycler for timetick updates per second
        StartCoroutine(cycler());
    }

    void Update() //update the GUI every frame
    {
        showTimeGUI();
    }

    void TimeTick() //this just starts the time tick, makes sure hour is added when its 60 minutes, and light controls 
    {
        //stringify _time
        string timeString = _time.ToString("00.00");
        //take last two characters of string, and convert to float
        _minutes = float.Parse(timeString.Substring(timeString.Length - 2));
        //take the first two characters of string, and convert to float
        _hour = float.Parse(timeString.Substring(0, 2));
        _time = TimeCheckCoroutine(_minutes, _hour);
        _time += _timeSpeed * Time.deltaTime; //set the time of day
        _time = _time % 24; //if reaches 24, goes back to 0
        //lighting based on time, its ugly but hey it works
        if(_time == 0) //darkest point of cycle
        {
            _light.intensity = 0.3f;
        }
        else if(_time > 4 && _time < 9) //midpoints (dawn)
        {
            //smooth out the transition between 0.3 to 0.5
            _light.intensity = Mathf.Lerp(_light.intensity, 0.5f, 0.1f);
        }
        else if(_time >= 9 && _time < 15) //lightest point of cycle
        {
            //smooth out the transition between 0.5 to 0.7
            _light.intensity = Mathf.Lerp(_light.intensity, 0.7f, 0.1f);
        }
        else if(_time >= 15 && _time < 18) //midpoints (dusk)
        {
            //smooth out the transition between 0.7 to 0.5
            _light.intensity = Mathf.Lerp(_light.intensity, 0.5f, 0.1f);
        }
        else if(_time >= 18 && _time < 24) //darkest point of cycle
        {
            //smooth out the transition between 0.5 to 0.3
            _light.intensity = Mathf.Lerp(_light.intensity, 0.3f, 0.1f);
        }
        //update the time text
        _TimeGUI.GetComponent<TextMeshProUGUI>().text = _time.ToString("00.00");

    }
    float TimeCheckCoroutine(float _minutes, float _hour)
    {
        //check if timeCheck is 60
        if (_minutes > 60)
        {
            //increment _time by 1
            //reset timeCheck to 0
            _minutes = 0;
            return _time = _hour + 1 + _minutes;
        }
        return _time;        
    }

    void showTimeGUI()
    {
        //show the time in the GUI
        _TimeGUI.GetComponent<TextMeshProUGUI>().text = "Time: " + _time.ToString("0.00");
    }

    IEnumerator cycler() //this starts the time ticking
    {
        while(true)
        {
            TimeTick();
            //everytime the time ticks, update the currentTime in the keepAliveInScenes.cs script
            _keepAlive.GetComponent<keepAliveInScenes>().SetData("currentTime", _time.ToString());
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator __initializeTime()
    {
        yield return new WaitForSeconds(0.25f); //since keepAliveInScenes wipes the dictionary when it starts, wait a bit for it to load before initializing Time
        //add initialTime data to the keepAliveInScenes.cs script as currentTime for reference
        _keepAlive.GetComponent<keepAliveInScenes>().AddData("currentTime", _initialTime.ToString());
        currentTime = _keepAlive.GetComponent<keepAliveInScenes>().GetData("currentTime");
        _time = float.Parse(currentTime);
    }

}
