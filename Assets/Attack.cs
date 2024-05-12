using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;

    public float attackRange;
    private RaycastHit2D hit;
    private bool confirmHit = false;

    //TODO se resta la vida en el momento en el que termina la animacion, deberia de restarse en el momento en el que colisiona

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = Physics2D.Raycast(new Vector2(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y + animator.gameObject.transform.localScale.y / 2), Vector2.right, attackRange, enemyLayer);

        if (hit && !confirmHit)
        {
            hit.collider.gameObject.GetComponent<EnemyController>().health -= damage;
            confirmHit = true;
        }
    }   

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        confirmHit = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
