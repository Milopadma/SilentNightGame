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
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //get the player
        _dialoguePanel = GameObject.FindGameObjectWithTag("Dialogue"); //get the dialogue panel
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is nearby, if yes, show the interact button above this gameObject
        if (Vector3.Distance(transform.position, _player.transform.position) < 1) 
        {
            //show the interact "E" button
            _interactButton.SetActive(true);
            //change the interact button transform position to above the gameObject
            _interactButton.transform.position = transform.position + new Vector3(0, 0.75f, 0);
            //check if user pressed the 'E' button;
            if (Input.GetKeyDown(KeyCode.E))
            {
                //enable the _dialoguePanel
                _dialoguePanel.SetActive(true);
                //show the dialogue in the dialogueText child of the _dialoguePanel
                _dialoguePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _displayedDialogue;
            }
        }
        else
        {
            // _dialoguePanel.SetActive(false);
            // _interactButton.SetActive(false);
            //disable this script instance in this gameobject
        }
    }
}
