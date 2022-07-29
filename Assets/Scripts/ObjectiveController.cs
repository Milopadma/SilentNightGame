using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    //listen for assignedObjective event to happen from Interactible.cs, when it does, show the corresponding text in the objective GUI HUD
    // public string[] _objectivesArray; //this is where the objectives will be stored
    private string _objText; //text of TMProUGUI
    // private int _index = 0; //index of the objective array
    
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
        if(_returnedString != null) //if its not null
        {
            //parse the returned string to an int
            // _index = int.Parse(_returnedString);
            //set the _objText to the current objective
            _objText = _keepAlive.GetComponent<keepAliveInScenes>().GetData("objective");
            showObjectiveText();
        }
        else //if its null, set the _objText to the first objective
        {
            // _index = 0;
            StartCoroutine(__initializeObjective());
        }
    }

    // void FixedUpdate()
    // {
    //     //show the objective text in the HUD
    //     showObjectiveText();
    // }

    void showObjectiveText()
    {
        //set the text of the TMProUGUI to the current objective
        // _objText = _objectivesArray[_index];

        //get the current objective from the keepAliveInScenes.cs
        _objText = _keepAlive.GetComponent<keepAliveInScenes>().GetData("objective");
        //show the objective text in GUI HUD
        this.GetComponent<TextMeshProUGUI>().text = _objText;
    }

    //this public function is called whenever an interactible has an objective assigned to it
    public void setObjective(string _objective)
    {
        //set the objective in the keepAliveInScenes.cs
        _keepAlive.GetComponent<keepAliveInScenes>().SetData("objective", _objective);
        showObjectiveText();
    }

    IEnumerator __initializeObjective()
    {
        yield return new WaitForSeconds(0.25f);
        //set the objective in the keepAliveInScenes.cs
        _objText = "Press 'E' to interact with objects"; 
        _keepAlive.GetComponent<keepAliveInScenes>().AddData("objective", _objText);
        showObjectiveText();
    }

    // public void nextObjective() //this is only called AFTER an objective is completed
    // {
    //     //move on to the next objective in the _objectivesArray
    //     _index++;
    //     //parse int to string
    //     string _sendString = _index.ToString();
    //     //current index added to Dictionary
    //     _keepAlive.GetComponent<keepAliveInScenes>().AddData("objectiveIndex", _sendString);
    //     //show the next objective in the array
    //     showObjectiveText();
    // }
}
