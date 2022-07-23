using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private GameObject _player;
    private UnityEngine.Rendering.Universal.Light2D _light;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
            if (_light.intensity < 1.5f)
            {
                _light.intensity += 0.01f;
            }
        }
        else
        {
            _light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>(); //get the light component from this gameObject
            //decrementally decrease the light intensity until it is 0
            if (_light.intensity > 0)
            {
                _light.intensity -= 0.01f;
            }
        }
    }
}
