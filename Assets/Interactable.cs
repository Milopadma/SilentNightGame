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
        //check if player is nearby, if yes, show the interact button above this gameObject
        if (Vector3.Distance(transform.position, _player.transform.position) < 1) 
        {
            //show the interact "E" button
            //instantiate the interact button
            if(!run){
                run = true;
                interactInstance = instantiateGameObject(_interactButton, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }

            //change the interact button transform position to above the gameObject
            //check if user pressed the 'E' button;
            if (Input.GetKeyDown(KeyCode.E) && dialogueInstance == null)
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
    //function that returns the instantiated gameobject
    public GameObject instantiateGameObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        return Instantiate(gameObject, position, rotation);
    }

    void destroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}

//note. solution 1; could set this to be a prefab so only calls its own dialogue panel and its own interact button
//      solution 2; could have a different script to check if the player is nearby and then call the dialogue panel and interact button
