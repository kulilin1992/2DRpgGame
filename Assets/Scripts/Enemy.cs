using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackRange;

    public Transform wayPoint01, wayPoint02;
    private Transform wayPointTarget;

    private SpriteRenderer sp;

    private Animator anim;
    public GameObject projectile;
    public Transform firePoint;

    void Start()
    {
        wayPointTarget = wayPoint01;
        //sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
        {
            anim.SetBool("isAttack", true);
            //Attack();
        }
        else
        {
            anim.SetBool("isAttack", false);
            Patrol();
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, wayPoint01.position) <= 0.01f)
        {
            wayPointTarget = wayPoint02;
            //sp.flipX = true;
            TurnAround();
        }
        if (Vector2.Distance(transform.position, wayPoint02.position) <= 0.01f)
        {
            wayPointTarget = wayPoint01;
            //sp.flipX = false;
            TurnAround();
        }
    }


    private void TurnAround()
    {
        Vector3 localTemp = transform.localScale;
        localTemp.x *= -1;
        transform.localScale = localTemp;
    }

    public void Attack()
    {
        Instantiate(projectile, firePoint.position, Quaternion.identity);
    }
}
