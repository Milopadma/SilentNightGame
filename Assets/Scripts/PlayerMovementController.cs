using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    private Animator animator;

    //dash
    private bool canDash = true;
    private bool isDashing;
    public float dashPower;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float _cooldownValue;

    [SerializeField] private GameObject _staminaGUI;
    [SerializeField] private TrailRenderer tr; //get the trailrenderer from this gameObject

    private void Start()
    {
        animator = GetComponent<Animator>(); //get the animator component from this gameObject
        _cooldownValue = 100f;
        UpdateStaminaGUI();
    }


    private void Update()
    {
        Vector2 dir = Vector2.zero;
        
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
                animator.CrossFade("sideWalk", 0);
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
                animator.CrossFade("sideWalk", 0);
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
                animator.CrossFade("upWalk", 0);
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
                animator.CrossFade("downWalk", 0);
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
}
