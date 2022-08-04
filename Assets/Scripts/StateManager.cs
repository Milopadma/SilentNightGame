using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 

public class StateManager : MonoBehaviour
{
    public void ReloadCurrentScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reloads the current scene/level
    }

    public void ChangeSceneByName(string name){
        if(name != null){
        SceneManager.LoadSceneAsync(name);
        }
    }
}

