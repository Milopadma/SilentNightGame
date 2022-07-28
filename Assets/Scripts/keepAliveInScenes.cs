// // using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// //place this into gameObjects to keep them alive in multiple scenes
// public class keepAliveInScenes : MonoBehaviour
// {
//     public List<string> _sceneNames;
//     public string _instanceName;

//     //data to keep track between scenes
//     public static Dictionary<string, bool> _data = new Dictionary<string, bool>();


//     private void Start()
//     {
//         DontDestroyOnLoad(this.gameObject);
//         SceneManager.sceneLoaded += OnSceneLoaded;
//     }

//     //add data to the dictionary
//     public void AddData(string key, bool value)
//     {
//         if (!_data.ContainsKey(key)){_data.Add(key, value);}
//     }

//     //get data from the dictionary
//     public string GetData(string key)
//     {
//         if (_data.ContainsKey(key)){return _data[key].ToString();}
//         else return null;
//     }

//     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         //check for duplicate Instances
//         checkDuplicateInstances();
//         //checkIfSceneIsValid
//         checkIfSceneIsValid();
//     }
    
//     void checkDuplicateInstances()
//     {
//         keepAliveInScenes[] collection = FindObjectsOfType<keepAliveInScenes>();
//         foreach (keepAliveInScenes obj in collection)
//         {
//             if(obj._instanceName == _instanceName)
//             {
//                 Debug.Log("keepAliveInScenes-- Duplicate Instance Found, Deleting!");
//                 DestroyImmediate(obj.gameObject);
//             }
//         }
//     }
    
//     void checkIfSceneIsValid()
//     {
//         string _currentScene = SceneManager.GetActiveScene().name; //get the name of the current scene
//         if(_sceneNames.Contains(_currentScene))
//         {
//             //do nothing
//         }
//         else
//         {
//             SceneManager.sceneLoaded -= OnSceneLoaded; //remove the event listener
//             DestroyImmediate(this.gameObject);
//         }
//     }
// }
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 
 /// <summary>
 /// Attach this component to objects that you want to keep alive (e.g. theme songs) in certain scene transitions. 
 /// For reusability, this component uses the scene names as strings to decide whether it survives or not after a scene is loaded 
 /// </summary>
 public class keepAliveInScenes : MonoBehaviour
 {
     [Tooltip("Names of the scenes this object should stay alive in when transitioning into")]
     public List<string> sceneNames;
 
     [Tooltip("A unique string identifier for this object, must be shared across scenes to work correctly")]
     public string instanceName;
 
     public static Dictionary<string, string> _data = new Dictionary<string, string>();

     // for singleton-like behaviour: we need the first object created to check for other objects and delete them in the scene during a transition
     // since Awake() callback preceded OnSceneLoaded(), place initialization code in Start()
     private void Start()
     {
        //reseet Dictionary
        _data.Clear();
        Debug.Log("keepAliveInScenes-- Dictionary Cleared");
         DontDestroyOnLoad(this.gameObject);
 
         // subscribe to the scene load callback
         SceneManager.sceneLoaded += OnSceneLoaded;
     }
 
     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
         // delete any potential duplicates that might be in the scene already, keeping only this one 
         CheckForDuplicateInstances();
 
         // check if this object should be deleted based on the input scene names given 
         CheckIfSceneInList();
     }
 
     void CheckForDuplicateInstances()
     {
         // cache all objects containing this component
         keepAliveInScenes[] collection = FindObjectsOfType<keepAliveInScenes>();
 
         // iterate through the objects with this component, deleting those with matching identifiers
         foreach (keepAliveInScenes obj in collection)
         {
             if(obj != this) // avoid deleting the object running this check
             {
                 if (obj.instanceName == instanceName)
                 {
                     Debug.Log("Duplicate object in loaded scene, deleting now...");
                     DestroyImmediate(obj.gameObject);
                 }
             }
         }
     }
 
     void CheckIfSceneInList()
     {
         // check what scene we are in and compare it to the list of strings 
         string currentScene = SceneManager.GetActiveScene().name;
 
         if (sceneNames.Contains(currentScene))
         {
             // keep the object alive 
         }
         else
         {
             // unsubscribe to the scene load callback
             SceneManager.sceneLoaded -= OnSceneLoaded;
             DestroyImmediate(this.gameObject);
         }
     }
     //add data to the dictionary
     public void AddData(string key, string value)
     {
         if (!_data.ContainsKey(key)){_data.Add(key, value);}
     }

     //get data from the dictionary
     public string GetData(string key)
     {
         if (_data.ContainsKey(key)){return _data[key].ToString();}
         else return null;
     }

     public void SetData(string key, string value)
     {
         if (_data.ContainsKey(key)){_data[key] = value;}
     }

 }
 