using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] private float attackRange;

    [SerializeField] protected float minAttackInterval;
    [SerializeField] protected float maxAttackInterval;

    [SerializeField] protected GameObject[] enemyBullets;
    [SerializeField] protected Transform enemyFirePoint;

    [SerializeField] protected float changeDirectionTime = 0;
    [SerializeField] protected float randomTime = 1;

    private float[] xDirection = { -1, 0, 1 };
    private float[] yDirection = { -1, 0, 1 };

    //随机xy
    public int xIndex = 0;
    public int yIndex = 0;

    //最终的坐标
    public float x = 0;
    public float y = 0;

    private Vector2 vector2;
    private Rigidbody2D rb;

    // 0 行走
    // 1 碰撞
    private int enemyState = 0;

    private Animator anim;
    public GameObject projectile;
    public Transform firePoint;

    private Transform flagPoint;

    private string defaultDirection = "right";

    public SpriteRenderer sp;
        public float hurtTime;
    private float hurtCounter;
    private bool flag;

    protected virtual void Awake()
    {

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }
    protected virtual void OnEnable()
    {
        //StartCoroutine(RandomMoveCoroutine());
        //StartCoroutine(EnemyAttackCoroutine());
        // StartCoroutine(nameof(RandomMoveCoroutine));
        // StartCoroutine(nameof(EnemyAttackCoroutine));
    }
    public void HurtShader() {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtTime;
    }

    void OnDisable()
    {
        // StopAllCoroutines();
    }
    IEnumerator RandomMoveCoroutine()
    {
        while (gameObject.activeSelf)
        {
           
            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual IEnumerator EnemyAttackCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minAttackInterval, maxAttackInterval));

            //if (GameManager.GameState == GameState.GameOver) yield break;

            foreach (var enemyBullet in enemyBullets)
            {
                PoolManager.Release(enemyBullet, enemyFirePoint.position);
            }
        }
    }

    void Update()
    {
         if (hurtCounter <= 0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) <= attackRange)
        {
            string getPlayerDirection = GetPlayerDirection(PlayerController.instance.transform, transform);
            TurnAround(getPlayerDirection);
            //anim.SetBool("isAttack", true);
        }
        else
        {
            //anim.SetBool("isAttack", false);
            //MoveController();
        }
    }

    private void MoveController()
    {
        // switch (enemyState)
        // {
        //     case 0:
        //         NormalMove();
        //         break;
        //     case 1:
        //         break;
        // }
        NormalMove();
    }

    public void NormalMove()
    {
    
        changeDirectionTime += Time.fixedDeltaTime;

        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        rb.velocity = vector2 * moveSpeed;
        if (changeDirectionTime >= randomTime)
        {
            randomTime = Random.Range(1, 5);
            xIndex = Random.Range(0, 3);
            yIndex = Random.Range(0, 3);
            while (xIndex != 1 && yIndex != 1)
            {
                xIndex = Random.Range(0, 3);
                yIndex = Random.Range(0, 3);
            }
            x = xDirection[xIndex];
            y = yDirection[yIndex];
            changeDirectionTime = 0;
        }
    }
    private void TurnAround(string getPlayerDirection)
    {
        if (defaultDirection.Equals(getPlayerDirection)) {
            return;
        }
        Debug.Log("开始转向玩家");
        Vector3 localTemp = transform.localScale;
        localTemp.x *= -1;
        transform.localScale = localTemp;
        defaultDirection = getPlayerDirection;
    }

    bool IsToTheLeft(Transform playerTransform, Transform enemyTransform)
{
    // 获取玩家和敌人之间的向量
    //Vector2 playerToEnemy = enemyTransform.position - playerTransform.position;

    Vector2 enemyToPlayer = playerTransform.position - enemyTransform.position;
 
    // 如果敌人在玩家左侧且非直接对着玩家，则认为敌人在左边
    return Mathf.Abs(Vector2.SignedAngle(enemyToPlayer, Vector2.right)) < 90;

}
    string GetPlayerDirection(Transform playerTransform, Transform enemyTransform)
    {
        Vector2 enemyToPlayer = playerTransform.position - enemyTransform.position;

        if (Mathf.Abs(Vector2.SignedAngle(enemyToPlayer, Vector2.right)) < 90)
        {
            return "right";
        }
        else
        {
            return "left";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Player"))
        // {
        //     collision.GetComponent<PlayerController>().HurtShader();
        //     //Debug.Log("敌人碰到玩家");
        //     Vector2 distance = (collision.transform.position - transform.position).normalized * 5;

        //     Debug.Log(distance);
        //     collision.transform.position = new Vector2(collision.transform.position.x + distance.x, collision.transform.position.y + distance.y);
        // }
    }
}
