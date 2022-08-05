 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;

 //signed //I GUSTI BAGUS MILO PADMA WIJAYA

 public class keepAliveInScenes : MonoBehaviour
 {
     public static Dictionary<string, string> _data = new Dictionary<string, string>(); //the dictionary of data
     public string instanceName; //the name of the instance of this script
     public List<string> sceneNames; //list of scene names to keep alive
     //add data to the dictionary
     public void AddData(string key, string value)
     {
         if (!_data.ContainsKey(key)){_data.Add(key, value);}
     }

     //get data from the dictionary
     public string GetData(string key)
     {
         if (_data.ContainsKey(key)){return _data[key].ToString();}
         else {return null;}
     }

     public void SetData(string key, string value)
     {
         if (_data.ContainsKey(key)){_data[key] = value;}
     }

     private void Start()
     {
        _data.Clear(); //reset Dictionary
        Debug.Log("(GAME STARTED)keepAliveInScenes.cs -- Dictionary Cleared");
         DontDestroyOnLoad(this.gameObject); // make this object persistent
         SceneManager.sceneLoaded += OnSceneLoaded; //subscribe to the event of sceneloads from scene manager
     }

     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
         duplicateInstanceCheck(); //check if there is another instance of this script
         sceneinListCheck(); //check if the scene is in the list of scenes to keep alive
     }

     void duplicateInstanceCheck()
     {
         keepAliveInScenes[] collection = FindObjectsOfType<keepAliveInScenes>(); //get all instances of this script
         foreach (keepAliveInScenes obj in collection) //loop through the collection of keepAliveInScene instances
         {
             if(obj != this) //if the object is not this instance
             {
                 if (obj.instanceName == instanceName) // if the instance name is equals to the instanceName
                 {DestroyImmediate(obj.gameObject);} //destroy the duplicate instance
             }
         }
     }

     void sceneinListCheck()
     {
         string currentScene = SceneManager.GetActiveScene().name; //get the name of the current scene
         if (!(sceneNames.Contains(currentScene))) //if the current scene is not in the list of scenes to keep alive
         {
            SceneManager.sceneLoaded -= OnSceneLoaded; //unsubscribe from the event of sceneloads from scene manager
            DestroyImmediate(this.gameObject); //destroy this instance
         }
     }
 }
 