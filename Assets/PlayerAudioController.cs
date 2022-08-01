using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//this controls all audio about the player (footsteps, interact)
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField] private AudioClip _interactSound;
    [SerializeField] private AudioClip[] _dashSound;
    [SerializeField] private AudioClip _flashLightToggleSound;
    [SerializeField] private AudioClip _tipTextSound;
    [SerializeField] private AudioClip _playerHurt;
    private AudioSource _audio;

    private bool alreadyPlayed = false;
    private float stepCooldown;
    private float stepRate = 0.3f; 

    private string _tileNameString;

    void Start()
    {
        //get the audio source
        _audio = GetComponent<AudioSource>();   
    }

    //check if the player is moving
    void Update()
    {
        stepCooldown -= Time.deltaTime;
        if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && stepCooldown < 0f){
        //get the tilemap
        Tilemap _tilemap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
        Sprite _tileName = _tilemap.GetSprite(_tilemap.WorldToCell(transform.position));
        //parse _tileMap to string
        _tileNameString = _tileName.ToString();
            //play the footstep sound
            PlayFootstepSound();

            //*Debug
            Debug.Log("played");
            stepCooldown = stepRate;
        }
    }

    //tilemap checker
    public void PlayFootstepSound()
    {
        Debug.Log(_tileNameString);
        //check if the tile is a wood tile
        if(_tileNameString.Contains("interior free_21")){
            //play the wood footstep sound by selecting a random number between 0 and 2
            _audio.PlayOneShot(_footstepSounds[Random.Range(0, 2)]);
            // _audio.Play();

            //*Debug
            Debug.Log(_tileNameString);
            stepCooldown = stepRate;
        }
        if(_tileNameString.Contains("interior free_22")){
            //play the carpet footstep sound by selecting a random number between 3 and 5
            _audio.PlayOneShot(_footstepSounds[Random.Range(3, 5)]);
            // _audio.Play();

            //*Debug
            Debug.Log(_tileNameString);
            stepCooldown = stepRate;
        }
        if(_tileNameString.Contains("Stone")){
            //play the stone footstep sound by selecting a random number between 6 and 8
            _audio.PlayOneShot(_footstepSounds[Random.Range(6, 8)]);
            // _audio.Play();

            //*Debug
            Debug.Log(_tileNameString);
            stepCooldown = stepRate;
        }
        if(_tileNameString.Contains("Grass")){
            //play the grass footstep sound by selecting a random number between 9 to 11
            _audio.PlayOneShot(_footstepSounds[Random.Range(9, 11)]);
            // _audio.Play();

            //*Debug
            Debug.Log(_tileNameString);
            stepCooldown = stepRate;
        }
    }

    //play the interact sound
    public void PlayInteractSound()
    {
        _audio.PlayOneShot(_interactSound);
    }

    //play the dash sound
    public void PlayDashSound()
    {
        _audio.PlayOneShot(_dashSound[Random.Range(0, 1)]);
    }

    //play teh flashlight toggle sound
    public void PlayFlashLightToggleSound()
    {
        _audio.PlayOneShot(_flashLightToggleSound);
    }
    public void PlayTipTextSound()
    {
        _audio.PlayOneShot(_tipTextSound);
    }
    public void PlayPlayerHurtSound()
    {
        _audio.PlayOneShot(_playerHurt);
    }
}
