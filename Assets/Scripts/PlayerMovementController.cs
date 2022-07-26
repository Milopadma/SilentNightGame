using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    private Animator animator;

    //dash
    private bool canDash = true;
    private bool isDashing;
    public float dashPower;
    private float dashingTime = 0.2f;
    private float _cooldownValue;

    public GameObject _escPanel;

    [SerializeField] private GameObject _staminaGUI;
    [SerializeField] private TrailRenderer tr; //get the trailrenderer from this gameObject

    private void Start()
    {
        animator = GetComponent<Animator>(); //get the animator component from this gameObject
        _cooldownValue = 100f;
        UpdateStaminaGUI();
        _escPanel.SetActive(false);
    }


    private void Update()
    {
        Vector2 dir = Vector2.zero;
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseGame();
        }


        //basic wasd movement
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            if(isDashing){
                dir.x = -dashPower;
            }
            else{
                dir.x = -1;
                dir.Normalize();
                transform.localScale = new Vector3(1, 1, 1);
                // animator.CrossFade("sideWalk", 0);
                PlayAnimation();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if(isDashing){
                dir.x = dashPower;
            }
            else{
                dir.x = 1;
                dir.Normalize();
                //flip the sprite 
                transform.localScale = new Vector3(-1, 1, 1);
                // animator.CrossFade("sideWalk", 0);
                PlayAnimation();
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if(isDashing){
                dir.y = dashPower;
            }
            else{
                dir.y = 1;
                dir.Normalize();
                // animator.CrossFade("upWalk", 0);
                PlayAnimation();
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if(isDashing){
                dir.y = -dashPower;
            }
            else{
                dir.y = -1;
                dir.Normalize();
                // animator.CrossFade("downWalk", 0);
                PlayAnimation();
            }
        }
        //if player is not moving, play idle animation
        if (dir.x == 0 && dir.y == 0)
        {
            animator.CrossFade("idle", 0);
        }

        GetComponent<Rigidbody2D>().velocity = speed * dir;
        //for the stamina bar
        UpdateStaminaGUI();
    }

    //dash
    private IEnumerator Dash()
    {
        //beforedash
        canDash = false;
        isDashing = true;
        //call playDashSound from PlayerAudioController
        GetComponent<PlayerAudioController>().PlayDashSound();
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //after dash
        tr.emitting = false;
        isDashing = false;
        //set cooldownValue to 0 and increment it by 1 every second
        _cooldownValue = 0;
        while(_cooldownValue < 100)
        {
            _cooldownValue += Time.deltaTime * 50;
            yield return null;
        }
        //short cooldown after dash
        // yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void UpdateStaminaGUI()
    {
        //get the stamina bar slider from staminaBar gameObject GUI
        Slider staminaBar = _staminaGUI.GetComponent<Slider>();
        staminaBar.value = _cooldownValue;
    }

    //when player presses ESC, pause the game
    public void pauseGame()
    {
        if (Time.timeScale == 1)
        {
            _escPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            _escPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void PlayAnimation()
    {
        // animator.CrossFade(animationName, 0);
        //when player is moving at an angle, play the sideWalk animation
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            animator.CrossFade("sideWalk", 0);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            animator.CrossFade("sideWalk", 0);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            animator.CrossFade("sideWalk", 0);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            animator.CrossFade("sideWalk", 0);
        }
        //else, play the animation based on the direction the player is moving
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.CrossFade("sideWalk", 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.CrossFade("sideWalk", 0);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                animator.CrossFade("upWalk", 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.CrossFade("downWalk", 0);
            }
        }

    }
}
