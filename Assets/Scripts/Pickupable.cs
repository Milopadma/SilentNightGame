using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private GameObject _player;
    private UnityEngine.Rendering.Universal.Light2D _light;
    private float x;
    private bool isPickedUp = false; //is the item picked up?
    public string _itemName; //the name of the item
    private Vector3 _initialPosition; //the initial position of the item (before it got picked up)

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _initialPosition = transform.position; //get the initial position of the item
    }

    // Update is called once per frame
    void Update()
    {
        //check if gameObject "player" is nearby
        if (Vector3.Distance(transform.position, _player.transform.position) < 1)
        {
            // GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
            _light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>(); //get the light component from this gameObject
            
            //incrementally increase the light intensity until it is 1.5f
            if (_light.intensity < 1.5f){_light.intensity += 0.01f;   }
            
            //if the item is picked up by the player
            if (Input.GetKeyDown(KeyCode.E) && !isPickedUp)
            {
                isPickedUp = true; //sets this to true so that the item is not picked up again
                // destroy the Interactable Instance of this gameobject
                // Destroy(GetComponent<Interactable>().interactInstance);
                //disable the Pickupable instance of this gameobject
                // this.enabled = false;
            }

        }
        //only when the player moves away and item is already pickedUp, disable the dialogue and E button  
        if(Vector3.Distance(_initialPosition, _player.transform.position) > 1 && isPickedUp)
        {
            //disable the Dialogue instance of this gameobject
            Destroy(GetComponent<Interactable>().dialogueInstance);
            //disable the E button instance of this gameobject
            Destroy(GetComponent<Interactable>().interactInstance);
            //disable the interactable instance of this gameobject
            Destroy(GetComponent<Interactable>());
            this.enabled = false; //disable Pickupable component of this gameobject
        }
        //when the player moves away OR the item is pickedUp, reset the light intensity to 0
        else if (Vector3.Distance(_initialPosition, _player.transform.position) > 1 || isPickedUp)
        {
            _light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>(); //get the light component from this gameObject
            //decrementally decrease the light intensity until it is 0
            if (_light.intensity > 0)
            {
                _light.intensity -= 0.01f;
            }
        }
    }
    public bool passCheck()
    {
        return isPickedUp;
    }
}
//note. functions with double underlines implies it should only be run once and never interacted by Update();
