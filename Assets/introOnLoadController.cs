using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 
public class introOnLoadController : MonoBehaviour
{
    public GameObject _dialoguePanel;
    public GameObject _playerCharacter;
    public GameObject _GUI;
    private GameObject dialogueInstance;
    public string _dialogueText;
    public bool isRun = false;
    private string _returnedString;

    GameObject _keepAlive;

    //when the scene loads, zoom the camera in, show the player getting up, zoom it out, show dialogue about the game, and then allow movement 
    void Start(){
        //get the keepAliveInScenes script from MainManager GameObject
        _keepAlive = GameObject.FindGameObjectWithTag("MainManager");
        //reset
        // _keepAlive.GetComponent<keepAliveInScenes>().AddData("isIntroFinished", "false");
        //get data from _data Dictionary
        _returnedString = _keepAlive.GetComponent<keepAliveInScenes>().GetData("isIntroFinished");
        //turn _returnedString to bool
        bool.TryParse(_returnedString, out bool isRun);
        if(!isRun){
            StartCoroutine(onLoad());
        }
        else{
            //set player position to near the door
            _playerCharacter.transform.position = new Vector3(-0.98f, -3.35f, 0.077f); 
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){ //if user presses E, skip the dialogue
            Destroy(dialogueInstance);
                SelfDestruct();
;        }
    }

    IEnumerator onLoad()
    {
        yield return new WaitForSeconds(0.251f);
        //disable all user input
        _playerCharacter.GetComponent<PlayerMovementController>().enabled = false;
        //hide GUI by getting CanvasScaler and setting it to 0 (doing this because there are other scripts that need GUI to be active during Awake)
        _GUI.GetComponent<CanvasScaler>().scaleFactor = 0;
        //zoooom the camera in
        Camera.main.orthographicSize = 2;
        //set the character 90 degrees so it looks like sleeping
        _playerCharacter.transform.rotation = Quaternion.Euler(0, 0, 90);
        yield return new WaitForSeconds(1);
        //zoom the camera out until 5
        while(Camera.main.orthographicSize < 5){
            Camera.main.orthographicSize += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        //set the character straight up
        _playerCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.25f);
        //now, set the GUI to be visible by scaling it back
        _GUI.GetComponent<CanvasScaler>().scaleFactor = 1;
        //enable user input
        _playerCharacter.GetComponent<PlayerMovementController>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        showDialogueSimple(_dialogueText);
        isRun = true;
        //add isRun to the keepAliveInScenes.cs Dictionary to prevent this script from running again
        _keepAlive.GetComponent<keepAliveInScenes>().AddData("isIntroFinished", "true");

    }
    
    void showDialogueSimple(string _dialogueText)
    {
        //instantiate the dialogue panel on default position
        dialogueInstance = (GameObject)Instantiate(_dialoguePanel, _dialoguePanel.transform.position, Quaternion.identity);
        //set the scale of the dialogue panel
        dialogueInstance.transform.localScale = new Vector3(1, 1, 1);
        //set parent to be GUI
        dialogueInstance.transform.SetParent(GameObject.Find("GUI").transform);
        //set the text of the dialogue panel
        dialogueInstance.GetComponentInChildren<TextMeshProUGUI>().text = _dialogueText;   
    }

    void SelfDestruct()
    {
        Destroy(dialogueInstance);
    }

}
