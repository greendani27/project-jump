using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform leftFootCheckPoint;
    [SerializeField] Transform rightFootCheckPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    private float horizontalAxis;

    public float jumpForce;
    public float speed;

    private bool onGround;

    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        Move();

        onGround = Physics2D.OverlapCircle(leftFootCheckPoint.position, .1f, groundLayer) || Physics2D.OverlapCircle(rightFootCheckPoint.position, .1f, groundLayer);

        if (onGround == true)
        {
            animator.SetBool("onGround", true);
        }
        else {
            animator.SetBool("onGround", false);
        }

        Jump();
    }

    private void Move() {
        rb.velocity = new Vector2(horizontalAxis * speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        // al añadir el controlador del salto, permite volver a saltar aunque no haya tocado el suelo
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * .5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(leftFootCheckPoint.position, .1f);
        Gizmos.DrawWireSphere(rightFootCheckPoint.position, .1f);
    }
}
