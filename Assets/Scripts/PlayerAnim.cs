using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;

    private bool isHitting;
    private float recoveryTime = 1;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
        OnAttackAnim();
        timeCount += Time.deltaTime;

        if (isHitting)
        {
            if(timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }


    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.isRolling)
            {
                anim.SetTrigger("isRoll");
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }


    void OnRun()
    {
        if (player.isRunning)
        {
            anim.SetInteger("transition", 2);
        }
    }

    #endregion

    #region Attack
    
    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
    }

    public void OnAttackAnim()
    {
        if (player.isAttacking)
        {
            anim.SetTrigger("isAttacking");
        }
    }

    #endregion

    public void OnHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("hurt");
            isHitting = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
