using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    private void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusic(_MainMenuMusic);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        //check what scene we are in and play the appropriate music
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
        _audio.Play(); //play the audio source
    }
}
