using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private CapsuleCollider2D capsuleCollider;
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
//    private bool isCrouching;
    private float moveHorizontal;
    private float moveVertical;
    public Animator animator;
    private bool FacingRight;
    Vector2 OriginalOffset;
    Vector2 OriginalSize;
    Vector2 CrouchOffset;
    Vector2 CrouchSize;


    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
        moveSpeed = 1f;
        jumpForce = 30f;
        isJumping = false;
     //   isCrouching = false;
        FacingRight = true;

          OriginalOffset = capsuleCollider.offset;
          OriginalSize = capsuleCollider.size;

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        animator.SetFloat("fSpeed", Mathf.Abs(moveHorizontal)); 
    }

    void FixedUpdate()
    {
        if(moveHorizontal != 0)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed,0),ForceMode2D.Impulse);
            //addforce needs vector2 and forcemode2d
            if (moveHorizontal > 0 && !FacingRight) //moving right and NOTfacing right
            {
                Flip();
            }
            else if(moveHorizontal < 0 && FacingRight)//moving left and facing right
            {
                Flip();
            }
        }

        if (moveVertical !=0 && isJumping == false)
        {
          //  Debug.Log("Hello");
            if (moveVertical > 0) //jump
            {
                rb2D.velocity = new Vector3(0, jumpForce, 0);

            }

            else if(moveVertical < 0)//crouch
            {
                CrouchOffset = new Vector2(capsuleCollider.offset.x, (float)-0.09);
                CrouchSize = new Vector2((float)0.15, (float)0.1);

                if(capsuleCollider.offset != CrouchOffset)
                {
 
                    capsuleCollider.offset = CrouchOffset;
                    capsuleCollider.size = CrouchSize;
                   // isCrouching = true;
                    animator.SetBool("isCrouching", true); //"isCrouching" name ni sa parameter in animator
                }  
            }
        }

        else //uncrouch
        {
            if (capsuleCollider.offset != OriginalOffset)
            {
                capsuleCollider.offset = OriginalOffset;
                capsuleCollider.size = OriginalSize;
             //   isCrouching = false;
                animator.SetBool("isCrouching", false);
            }
        }


    }


    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        FacingRight = !FacingRight;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
            //if collider is touching the ground/platforms
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            //if collider is DONE touching the ground/platforms
        }
    }


}
