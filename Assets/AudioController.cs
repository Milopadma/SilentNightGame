using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 


//this script controls the Audio in the game
public class AudioController : MonoBehaviour
{
    //audio clips for the appropriate scenes
    [SerializeField] AudioClip _MainMenuMusic;
    [SerializeField] AudioClip _BarMusic;
    [SerializeField] AudioClip _RoadMusic;
    [SerializeField] AudioClip _ForestMusic;
    [SerializeField] AudioClip _HouseMusic;
    [SerializeField] AudioClip _CreditsMusic;
    [SerializeField, Range(0f, 1f)] float _volume = 0.125f;
    private Slider _volumeSlider;

    private void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        //play _mainMenuMusic on a loop

        PlayMusic(_MainMenuMusic);
        __initializeMainMenu();


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        //check what scene we are in and play the appropriate music
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            //check if settingsButton is active
            if(GameObject.FindGameObjectWithTag("settingsButtons") != null){

                //get the volume slider
                _volumeSlider = GameObject.FindGameObjectWithTag("gameVolSlider").GetComponent<Slider>();

                //set the volume slider to the current volume
                _volume = _volumeSlider.value;
                
                //disable the settingsButton
                GameObject.FindGameObjectWithTag("settingsButtons").SetActive(false); 
            }
        }
        if(SceneManager.GetActiveScene().name == "Level_1"){
            PlayMusic(_BarMusic);
        }
        if(SceneManager.GetActiveScene().name == "Level_2"){
            PlayMusic(_RoadMusic);
        }
        if(SceneManager.GetActiveScene().name == "Level_3"){
            PlayMusic(_ForestMusic);
        }
        if(SceneManager.GetActiveScene().name == "Level_4"){
            PlayMusic(_HouseMusic);
        }
        if(SceneManager.GetActiveScene().name == "Credits"){
            PlayMusic(_CreditsMusic);
        }


    }

    //play the music
    void PlayMusic(AudioClip clip)
    {
        AudioSource _audio = GetComponent<AudioSource>(); //get the audio source
        _audio.clip = clip; //set the audio source clip to the clip
        _audio.loop = true;
        _audio.Play(); //play the audio source
    }

    void Update()
    {
        setVolumeSlider();
    }

    void setVolumeSlider()
    {
        //check if the current scene is MainMenu
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            //set the volume of the audio source to the volume
            GetComponent<AudioSource>().volume = _volume;
            //check if the volume slider is not null
            if(_volumeSlider != null){
                //set the volume to the volume slider value
                _volume = _volumeSlider.value;
            }
        }
    }

    void __initializeMainMenu(){
                //check what scene we are in and play the appropriate music
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            //check if settingsButton is active
            if(GameObject.FindGameObjectWithTag("settingsButtons") != null){

                //get the volume slider
                _volumeSlider = GameObject.FindGameObjectWithTag("gameVolSlider").GetComponent<Slider>();

                //set the volume slider to the current volume
                _volume = _volumeSlider.value;
                
                //disable the settingsButton
                GameObject.FindGameObjectWithTag("settingsButtons").SetActive(false); 
            }
    }
}
}
