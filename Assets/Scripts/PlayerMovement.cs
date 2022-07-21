using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        private Animator animator;

        //dash
        private bool canDash = true;
        private bool isDashing;
        public float dashPower;
        private float dashingTime = 0.2f;
        private float dashingCooldown = 1f;

        [SerializeField] private TrailRenderer tr;

        private void Start()
        {
            animator = GetComponent<Animator>();
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
                    animator.SetInteger("Direction", 3);
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
                    animator.SetInteger("Direction", 2);
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
                    animator.SetInteger("Direction", 1);
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
                    animator.SetInteger("Direction", 0);
                }
            }

            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
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
            //short cooldown after dash
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }

    }
}
