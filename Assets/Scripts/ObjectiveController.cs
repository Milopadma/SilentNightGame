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

    //create an instance of ObjectiveController
    public static ObjectiveController instance;

    void Awake()
    {
        if(ObjectiveController.instance == null) instance = this;
        else Destroy(gameObject);
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
    }

    public void nextObjective()
    {
        //move on to the next objective in the _objectivesArray
        _index++;
        //if there is no more objective in the array, set the index to 0
        if(_index >= _objectivesArray.Length)
        {
            _index = 0;
        }
        //show the next objective in the array
        showObjectiveText();
    }
}
