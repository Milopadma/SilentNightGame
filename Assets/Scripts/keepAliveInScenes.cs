using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//place this into gameObjects to keep them alive in multiple scenes
public class keepAliveInScenes : MonoBehaviour
{
    public List<string> _sceneNames;
    public string _instanceName;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //check for duplicateInstances
        checkDuplicateInstances();
        //checkIfSceneIsValid
        checkIfSceneIsValid();
    }
    
    void checkDuplicateInstances()
    {
        keepAliveInScenes[] collection = FindObjectsOfType<keepAliveInScenes>();
        foreach (keepAliveInScenes obj in collection)
        {
            if(obj._instanceName == _instanceName)
            {
                DestroyImmediate(obj.gameObject);
            }
        }
    }
    
    void checkIfSceneIsValid()
    {
        string _currentScene = SceneManager.GetActiveScene().name; //get the name of the current scene
        if(_sceneNames.Contains(_currentScene))
        {
            //do nothing
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; //remove the event listener
            DestroyImmediate(this.gameObject);
        }
    }
}
