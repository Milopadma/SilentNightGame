using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 
public class Interactable : MonoBehaviour
{
    private GameObject _player; //the player
    private GameObject _dialoguePanel; //the dialogue panel
    public GameObject _interactButton; //the interact button
    public string _displayedDialogue; //the dialogue to be displayed

    public GameObject interactInstance; //the instance of the interact button
    public GameObject dialogueInstance; //the instance of the dialogue panel
    public bool run = false; //whether the interact button should be displayed

    private string[] wordArray; //the array of words in the dialogue
    string firstString = ""; //the first string of the dialogue
    string secondString = ""; //the second string of the dialogue
    private bool showNextDialogue = false; //whether the next dialogue should be displayed
    
    public int _objectiveID; //if an interactible has an objectiveID, when user interacts with it, it will call nextObjective() in ObjectiveController.cs
    public bool _isInteracted = false; //whether this instance of interactible has been interacted with

    public string _thisObjectiveText;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //get the player
        _dialoguePanel = GameObject.FindGameObjectWithTag("Dialogue"); //get the dialogue panel
        //set the default dialogue panel scale to be 0
        _dialoguePanel.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 1) //check if player is nearby, if yes, show the interact button above this gameObject 
        {
            //split the string into an array of words
            wordArray = _displayedDialogue.Split(' ');
            //this is for interact button
            if(!run){ //only run once
                run = true; 
                interactInstance = instantiateGameObject(_interactButton, transform.position + new Vector3(0, 1, 0), Quaternion.identity); //create a new instance of the interact button
            }
            //this is for dialogue
            if (Input.GetKeyDown(KeyCode.E) && dialogueInstance == null) //check if user pressed the 'E' button;
            {
                checkForObjectiveID(); //check if this instance of interactible has an objectiveID
                //if the _displayedDialogue string is longer than 10 words, put the words that doesnt fit into new panel after the first 10 words
                if(wordArray.Length > 10)
                {
                    //new string with the first 10 words
                    for(int i = 0; i < 10; i++){firstString += wordArray[i] + " ";}
                    //show the first 10 words in the dialogue panel
                    dialoguePanel(firstString);
                    //set to allow next set of dialogue to show
                    showNextDialogue = true;

                }
                else{dialoguePanel(_displayedDialogue);} //create a new instance of the dialogue panel
            }
        StartCoroutine(showRestOfDialogue(wordArray));
        }
        else //when player is not nearby
        {
            run = false;
            //destroy the instance of the interact button if it exists
            if(interactInstance != null){
                Destroy(interactInstance);
            }
            //reset everything
            firstString = "";
            secondString = "";
            Destroy(dialogueInstance);
            //disable this script instance in this gameobject
        }
    }
    //to display the dialogue panel on screen
    void dialoguePanel(string _displayedDialogue)
    {
        //instantiate the dialogue panel on GUI
        dialogueInstance = instantiateGameObject(_dialoguePanel, _dialoguePanel.transform.position, Quaternion.identity);
        //set dialogueInstance scaling to 1
        dialogueInstance.transform.localScale = new Vector3(1, 1, 1);
        //set the dialogue text to dialogueInstance
        dialogueInstance.GetComponentInChildren<TextMeshProUGUI>().text = _displayedDialogue;
        //set this gameObject parent GUI
        dialogueInstance.transform.SetParent(GameObject.Find("GUI").transform);
        //play the PlayInteractSound from PlayerAudioController.cs
        _player.GetComponent<PlayerAudioController>().PlayInteractSound();
    }

    //method to return an instantiated gameObject
    public GameObject instantiateGameObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        return Instantiate(gameObject, position, rotation);
    }
    //method to destroy gameObject
    void destroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    IEnumerator showRestOfDialogue(string[] wordArray){
        yield return new WaitForSeconds(0.35f);
        if(Input.GetKeyDown(KeyCode.E) && dialogueInstance != null && showNextDialogue == true) //if player presses E again and theres more dialogue to show, run this
        {
            destroyGameObject(dialogueInstance); //destroy the dialogue panel
            //reset firstString
            firstString = "";
            secondString = "";
            //new string with the rest of the words
            for(int i = 10; i < wordArray.Length; i++){secondString += wordArray[i] + " ";}
            //instantiate dialogue panel with second set of string
            dialoguePanel(secondString);
            //set to false to prevent next set of dialogue to show
            showNextDialogue = false;
        }
    }
    void checkForObjectiveID()
    {
        if(_objectiveID != 0 && !_isInteracted)
        {
            _isInteracted = true; //this finishes the objective when the player interacts with this instance of interactible
            // ObjectiveController.instance.nextObjective(); //calls nextObjective() in ObjectiveController.cs
            ObjectiveController.instance.setObjective(_thisObjectiveText);
        }
        else
        {
            Debug.Log("No objective ID assigned to this interactible");
        }
    }
}

//note. solution 1; could set this to be a prefab so only calls its own dialogue panel and its own interact button
//      solution 2; could have a different script to check if the player is nearby and then call the dialogue panel and interact button
