using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this controls all audio about the player (footsteps, interact)
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private AudioClip _interactSound;

    void Start()
    {
        //get the audio source
        _audio = GetComponent<AudioSource>();   
    }

    //check if the player is moving
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            PlayFootstepSound();
        } else {
            return;
        }
    }

    //tilemap checker
    public void PlayFootstepSound()
    {
        //get the tilemap
        Tilemap _tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
        //get the tilemap layer
        TilemapLayer _layer = _tilemap.GetLayer("Ground");
        //get the tilemap layer tile
        TileBase _tile = _layer.GetTile(new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0));
        //get the tilemap layer tile name
        string _tileName = _tile.name;
        //check if the tile is a wood tile
        if(_tileName.Contains("interior free_21")){
            //play the wood footstep sound
            _audio.PlayOneShot(_footstepSounds[0]);
        }
        //check if the tile is a grass tile
        if(_tileName.Contains("Grass")){
            //play the grass footstep sound
            _audio.PlayOneShot(_footstepSounds[1]);
        }
        //check if the tile is a road tile
        if(_tileName.Contains("Stone")){
            //play the road footstep sound
            _audio.PlayOneShot(_footstepSounds[2]);
        }
    }

    //play the interact sound
    public void PlayInteractSound()
    {
        _audio.PlayOneShot(_interactSound);
    }
}
