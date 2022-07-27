using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    //listen for assignedObjective event to happen from Interactible.cs, when it does, show the corresponding text in the objective GUI HUD
    public string[] _objectivesArray; //this is where the objectives will be stored
    private string _objText; //text of TMProUGUI
    private int _index = 0; //index of the objective array
    
    GameObject _keepAlive; //refers to the keepAliveInScenes.cs
    private string _returnedString; //string returned from the keepAliveInScenes.cs
    //create an instance of ObjectiveController
    public static ObjectiveController instance;

    void Start()
    {
        if(ObjectiveController.instance == null) instance = this;
        else Destroy(gameObject);
        //add to the objectiveIndex Dictionary in the keepAliveInScenes.cs 
        _keepAlive = GameObject.FindGameObjectWithTag("MainManager");        
        //get the current index of the objective dictionary
        _returnedString = _keepAlive.GetComponent<keepAliveInScenes>().GetData("objectiveIndex");
        if(_returnedString != null)
        {
            //parse the returned string to an int
            _index = int.Parse(_returnedString);
        }
        else
        {
            _index = 0;
        }
    }

    void FixedUpdate()
    {
        //show the objective text in the HUD
        showObjectiveText();
    }

    void showObjectiveText()
    {
        //set the text of the TMProUGUI to the current objective
        _objText = _objectivesArray[_index];
        //show the objective text in GUI HUD
        this.GetComponent<TextMeshProUGUI>().text = _objText;
        Debug.Log("ObjectiveController-- _index: " + _index);

    }

    public void nextObjective() //this is only called AFTER an objective is completed
    {
        //move on to the next objective in the _objectivesArray
        _index++;
        //parse int to string
        string _sendString = _index.ToString();
        //current index added to Dictionary
        _keepAlive.GetComponent<keepAliveInScenes>().AddData("objectiveIndex", _sendString);
        //show the next objective in the array
        showObjectiveText();
    }
}
