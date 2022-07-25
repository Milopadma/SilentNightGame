using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    private GameObject _player; //the player
    private GameObject _dialoguePanel; //the dialogue panel
    public GameObject _interactButton; //the interact button
    public string _displayedDialogue; //the dialogue to be displayed

    private GameObject interactInstance; //the instance of the interact button
    private GameObject dialogueInstance; //the instance of the dialogue panel
    public bool run = false; //whether the interact button should be displayed

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
            if(!run){ //only run once
                run = true; 
                interactInstance = instantiateGameObject(_interactButton, transform.position + new Vector3(0, 1, 0), Quaternion.identity); //create a new instance of the interact button
            }

            if (Input.GetKeyDown(KeyCode.E) && dialogueInstance == null) //check if user pressed the 'E' button;
            {
                //if the _displayedDialogue string is longer than 10 words, put the words that doesnt fit into new panel after the first 10 words
                if(_displayedDialogue.Length > 10)
                {
                    //split the string into an array of words
                    string[] wordArray = _displayedDialogue.Split(' ');
                    //new string with the first 10 words
                    string newString = "";
                    for(int i = 0; i < 10; i++)
                    {
                        newString += wordArray[i] + " ";
                    }
                    //show the first 10 words in the dialogue panel
                    dialoguePanel(newString);
                    //wati coroutine to show the rest of the words in the dialogue panel
                    StartCoroutine(showRestOfDialogue(wordArray));
                }
                else{
                    dialoguePanel(_displayedDialogue); //create a new instance of the dialogue panel
                }
            }
        }
        else //when player is not nearby
        {
            run = false;
            //destroy the instance of the interact button if it exists
            if(interactInstance != null){
                Destroy(interactInstance);
            }
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
        // yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);
        if(Input.GetKeyDown(KeyCode.Return)){
            destroyGameObject(dialogueInstance); //destroy the dialogue panel
            //new string with the rest of the words
            string secondBatchString = "";
            for(int i = 10; i < wordArray.Length; i++)
            {
                secondBatchString += wordArray[i] + " ";
            }
            dialoguePanel(secondBatchString);
        }
    }
}

//note. solution 1; could set this to be a prefab so only calls its own dialogue panel and its own interact button
//      solution 2; could have a different script to check if the player is nearby and then call the dialogue panel and interact button
