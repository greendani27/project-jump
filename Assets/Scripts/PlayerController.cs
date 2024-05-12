using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter
{
    #region consts
    private const float footCheckPointRange = .1f;
    #endregion

    #region components
    [SerializeField] Transform leftFootCheckPoint;
    [SerializeField] Transform rightFootCheckPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Rigidbody2D rbb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sr;
    #endregion

    public float health;

    private float horizontalAxis;

    public float jumpForce;
    public float attackRange;

    public float minSpeed;
    public float maxSpeed;
    private float speed;

    private bool onGround;

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");

        onGround = Physics2D.OverlapCircle(leftFootCheckPoint.position, footCheckPointRange, groundLayer) || Physics2D.OverlapCircle(rightFootCheckPoint.position, footCheckPointRange, groundLayer);

        if (onGround == true)
            animator.SetBool("onGround", true);
        else
            animator.SetBool("onGround", false);

        Move();
        Jump();
        Punch();
    }

    #region heritage methods

    public void Move()
    {
        if (horizontalAxis != 0)
        {
            animator.SetBool("Running", true);

            Sprint();
            Flip();

            rbb.velocity = new Vector2(horizontalAxis * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
            animator.SetBool("Running", false);
    }

    public void Punch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Punch");
        }
    }
    
    public void Flip()
    {
        if (horizontalAxis < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    #endregion

    #region private methods
    private void Sprint()
    {
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
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rbb.velocity = new Vector2(rbb.velocity.x, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rbb.velocity.y > 0)
        {
            rbb.velocity = new Vector2(rbb.velocity.x, rbb.velocity.y * .5f);
        }
    }

    

    #endregion

    #region debugging methods
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), transform.TransformDirection(Vector3.forward) * 1000, Color.green);
        Gizmos.DrawWireSphere(leftFootCheckPoint.position, footCheckPointRange);
        Gizmos.DrawWireSphere(rightFootCheckPoint.position, footCheckPointRange);
    }
    #endregion
}
