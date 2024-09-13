using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackRange;

    public Transform wayPoint01, wayPoint02;
    private Transform wayPointTarget;

    public SpriteRenderer sp;

    private Animator anim;
    public GameObject projectile;
    public Transform firePoint;


    public float hurtTime;
    private float hurtCounter;
    public bool isHurt = false;
    //private SpriteRenderer sr;

void Awake()
{
    sp = GetComponent<SpriteRenderer>();
}
    void Start()
    {
        wayPointTarget = wayPoint01;
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
            //Patrol();
        }
        // if (isHurt)
        // {

        // }
        Debug.Log("hurtCounter" + hurtCounter);
        if (hurtCounter <= 0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
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
    public void HurtShader() {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtTime;
    }


    private void TurnAround()
    {
        Vector3 localTemp = transform.localScale;
        localTemp.x *= -1;
        transform.localScale = localTemp;
    }

    public void Attack()
    {
        PoolManager.Release(projectile, firePoint.position, Quaternion.identity);
        //Instantiate(projectile, firePoint.position, Quaternion.identity);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Enemy Attack Player");
    //         //TODO
    //         //给敌人造成伤害，减少敌人hp

    //         // 获取怪物的Rigidbody组件
    //         Rigidbody monsterRigidbody = collision.gameObject.GetComponent<Rigidbody>();
 
    //         // 如果存在，给予一个向外的力
    //         if (monsterRigidbody != null)
    //         {
    //             // 给怪物应用力
    //             monsterRigidbody.AddForce(-transform.forward * 1000f);
    //         }
    //     }
    // }
}
