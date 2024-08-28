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

    protected virtual void Awake()
    {

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void OnEnable()
    {
        //StartCoroutine(RandomMoveCoroutine());
        //StartCoroutine(EnemyAttackCoroutine());
        // StartCoroutine(nameof(RandomMoveCoroutine));
        // StartCoroutine(nameof(EnemyAttackCoroutine));
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
        //MoveController();
    }

    void FixedUpdate()
    {
        MoveController();
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
    
        changeDirectionTime += Time.deltaTime;

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
}
