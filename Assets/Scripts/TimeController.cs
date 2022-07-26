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

    void Awake()
    {
        //get the global lighting
         _light = _globalLighting.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        //set the _time to _initialTime
        _time = _initialTime;
        StartCoroutine(cycler());
    }

    void Update()
    {
        showTimeGUI();
    }

    void TimeTick()
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
            Debug.Log("Time added");
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

    IEnumerator cycler()
    {
        while (true)
        {
            TimeTick();
            yield return new WaitForSeconds(1);
        }
    }

}
