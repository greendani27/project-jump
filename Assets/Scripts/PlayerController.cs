using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform leftFootCheckPoint;
    [SerializeField] Transform rightFootCheckPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rbb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sr;

    private float horizontalAxis;

    public float jumpForce;

    public float minSpeed;
    public float maxSpeed;

    private float speed;

    private bool onGround;

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");

        Move();

        onGround = Physics2D.OverlapCircle(leftFootCheckPoint.position, .1f, groundLayer) || Physics2D.OverlapCircle(rightFootCheckPoint.position, .1f, groundLayer);

        if (onGround == true)
            animator.SetBool("onGround", true);
        else
            animator.SetBool("onGround", false);

        Jump();

        Punch();
    }

    private void Move()
    {
        if (horizontalAxis != 0)
        {
            animator.SetBool("Running", true);

            if (horizontalAxis < 0)
            {
                sr.flipX = true;
            }
            else {
                sr.flipX = false;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = maxSpeed;
                animator.speed = 2;
            }
            else
            {
                speed = minSpeed;
                animator.speed = 1;
            }

            rbb.velocity = new Vector2(horizontalAxis * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
            animator.SetBool("Running", false);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rbb.velocity = new Vector2(rbb.velocity.x, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rbb.velocity.y > 0)
        {
            rbb.velocity = new Vector2(rbb.velocity.x, jumpForce * .5f);
        }
    }

    private void Punch() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Punch");
        }
        
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(leftFootCheckPoint.position, .1f);
        Gizmos.DrawWireSphere(rightFootCheckPoint.position, .1f);
    }*/
}
