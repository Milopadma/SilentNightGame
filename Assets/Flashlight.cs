using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//what does this do?
//on start, check if it was pickedup already or not, if not then just keep checking
public class Flashlight : MonoBehaviour
{
    //for the flashlight item, this controls the light intensity and direction, and whether it is on or off
    private GameObject _player; //the player
    private UnityEngine.Rendering.Universal.Light2D _light; //the light component
    private bool isPickedUp; //is the item picked up?
    private bool isOn; //is the flashlight on?
    public float intensity; //the intensity of the light
    private bool _isRun = false; //did this section run already?
    GameObject _keepAlive; //the keepAlive object
    private string itemCheckString; //the returned string to check if the item is picked up from Dictionary

    //whenever loaded on new Scene, get if flashlight was pickedUp, and if it was off
    void Start()
    {
        StartCoroutine(__initializeFlashlight());
    }

    void Update()
    {
        eventHandler();//check mouseposition for light direction, set position to player, and toggles
        isPickedUpCheck(); //check if the item is picked up
    }

    //in the case where the Flashlight hasn't been picked up yet, this will run
    IEnumerator __initializeFlashlight()
    {
        yield return new WaitForSeconds(0.25f);
        itemCheckString = _keepAlive.GetComponent<keepAliveInScenes>().GetData("hasFlashlight");
        if(itemCheckString == null){_keepAlive.GetComponent<keepAliveInScenes>().SetData("hasFlashlight", "false");}
        if (itemCheckString == "true") //if the item is picked up
        {
            isPickedUp = true; //sets this to true so that the item is not picked up again
            _isRun = false;
        }
        else //if the item is not picked up
        {
            isPickedUp = false; //sets this to false so that the item can be picked up
        }

        
        //*Debug Log*/
        Debug.Log("Flashlight.cs: __initializeFlashlight() called");
        //*Debug Log*/
    }

    //this checks wether or not flashlight was pickedup or not
    public void isPickedUpCheck()
    {
        itemCheckString = _keepAlive.GetComponent<keepAliveInScenes>().GetData("hasFlashlight"); //get data from Dictionary, if it is null, return false
        isPickedUp = GetComponent<Pickupable>().passCheck(); //check if item was already pickedup from Pickupable
        if(itemCheckString == "false" || isPickedUp == false){return;}
        if(!_isRun){ //only runs once
            if(itemCheckString == "true" || isPickedUp == true){__loadFlashlight(); _isRun = false;}
        }

        //*Debug Log*/
        Debug.Log("Flashlight.cs: isPickedUpCheck() called");
        //*Debug Log*/
    }

    //load the flashlight GameObject (also does tiptexts)
    void __loadFlashlight()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>(); //get the light component from this gameObject
        //set the intensity of the light
        intensity = _light.intensity;
        //change the color of the existing light to be yellow
        _light.color = Color.yellow;
        //change the inner spot angle of the light to be 45
        _light.pointLightInnerAngle = 45;
        //change the outer spot angle of the light to be 45
        _light.pointLightOuterAngle = 85;
        //change the outer radius of the light to be 4.2
        _light.pointLightOuterRadius = 8.2f;
        //change the inner radius of the light to be 0.5
        _light.pointLightInnerRadius = 0.5f;
        isOn = true;
        //calling __showOnTipText from TipTextController, to show the tip text "Press F to toggle flashlight"
        GameObject tipText = GameObject.FindGameObjectWithTag("TipText");
        tipText.GetComponent<TipTextController>().__showOnTipText("Tip: Press 'F' to toggle flashlight");
    }
    //this keeps checking the mouse position and stick flashlight to player, also listens for toggles
    void eventHandler()
    {
        flashlightPositionRotation(isPickedUp); //check if the player is nearby
        if(Input.GetKeyDown(KeyCode.F)){StartCoroutine(toggleFlashlight());} //toggle the flashlight on and off
    }
    //this checks the mouseposition and sets the light direction, also sets the flashlight position to the player
    void flashlightPositionRotation(bool isPickedUp)
    {
        if(isPickedUp)
        {
            //get the mouse position and aim the light at mouse position 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            //set the rotation of the light to be the direction of the mouse position
            _light.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - _light.transform.position);
            //sets the location of the flashlight GameObject to be the same as the player
            transform.position = _player.transform.position;
            transform.rotation = _player.transform.rotation;
        }
        else{return;}
    }
    //this toggles the flashlight on and off
    IEnumerator toggleFlashlight()
    {
        yield return new WaitForSeconds(0.1f);
        if(!isOn) //if flashlight is off (false)
        {
            isOn = true;
            _light.intensity = intensity;
            _keepAlive.GetComponent<keepAliveInScenes>().SetData("flashlightisOn", "true");
        }
        else
        {
            isOn = false;
            _light.intensity = 0;
            _keepAlive.GetComponent<keepAliveInScenes>().SetData("flashlightisOn", "false");
        }
        yield return null;
    }
}
//note. functions with double underlines implies it should only be run once and never interacted by Update();
//note. i have a hard time distinguishing which one is which sometimes so this is a good fix for me //milo
