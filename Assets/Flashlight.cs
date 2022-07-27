using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    //for the flashlight item, this controls the light intensity and direction, and whether it is on or off
    private GameObject _player;
    private UnityEngine.Rendering.Universal.Light2D _light;
    private float x;
    private bool isPickedUp; //is the item picked up?
    private bool isOn; //is the flashlight on?
    public float intensity; //the intensity of the light
    private bool _isRun = false;

    void FixedUpdate()
    {
        //keeps checking if isPickedUp is true from Pickupable.cs (as every pickupables have their own custom items)
        isPickedUp = GetComponent<Pickupable>().passCheck();       
        if(isPickedUp && !_isRun) //ONLY run when isPickedUp returns true, and it hasnt been run yet.
        {
            __isPickedUp();
            _isRun = true;
        }
        else{
            return;
        }
    }

    public void __isPickedUp()
    {
        if(isPickedUp){
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
            Debug.Log("Flashlight is on");
            //calling __showOnTipText from TipTextController, to show the tip text "Press F to toggle flashlight"
            GameObject tipText = GameObject.FindGameObjectWithTag("TipText");
            tipText.GetComponent<TipTextController>().__showOnTipText("Tip: Press 'F' to toggle flashlight");
        }
        else
        {
            return;
        }
    }

    void Update()
    {
        getMousePosition(isPickedUp); //get the mouse position
        pickedUpByPlayer(isPickedUp); //check if the player is nearby
        //toggle the flashlight on and off
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(toggleFlashlight());
        }
    }
    void getMousePosition(bool isPickedUp)
    {
        if(isPickedUp){
            //get the mouse position and aim the light at mouse position 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            //set the rotation of the light to be the direction of the mouse position
            _light.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - _light.transform.position);
            // transform.LookAt(mousePosition);
        }
        else
        {
            return;
        }
    }
    IEnumerator toggleFlashlight()
    {
        Debug.Log(isOn);
        yield return new WaitForSeconds(0.1f);
        if(!isOn) //if flashlight is off (false)
        {
            isOn = true;
            _light.intensity = intensity;
        }
        else
        {
            isOn = false;
            _light.intensity = 0;
        }
        yield return null;
    }
    void pickedUpByPlayer(bool isPickedUp)
    {
        if(isPickedUp)
        {
            transform.position = _player.transform.position;
            transform.rotation = _player.transform.rotation;
        }

    }
}
//note. functions with double underlines implies it should only be run once and never interacted by Update();
//note. i have a hard time distinguishing which one is which sometimes so this is a good fix for me //milo
