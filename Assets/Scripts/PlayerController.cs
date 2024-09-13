using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;

    public static PlayerController instance;
    private Rigidbody2D rb;
    private float moveHorizontal, moveVertical;

    public string nextSceneVerify;

    private SpriteRenderer sp;
    bool isttt;

    float time;

        public float hurtTime;
    private float hurtCounter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed;
        //Debug.Log(moveHorizontal);
        if (moveHorizontal > 0) {
            sp.flipX = false;
        } else if (moveHorizontal < 0) {
            sp.flipX = true;
        }

         if (hurtCounter <= 0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
        }
    }

    public void HurtShader() {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtTime;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveHorizontal, moveVertical);
        
        // if (isttt)
        // {
        //     Debug.Log("moveHorizontal:"+ moveHorizontal + ",moveVertical:"+ moveVertical);
        //         //rb.AddForce(Vector2.right, ForceMode2D.Impulse);
        //         //rb.AddForce(new Vector2(moveHorizontal, moveVertical) * -8f, ForceMode2D.Impulse);
        //         isttt = false;
        // } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     // 计算击飞方向
        //     Vector2 direction = (transform.position - collision.transform.position).normalized;

        //     // 应用击飞效果
        //     Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //     rb.velocity = direction * 2;
        // }
        // if (collision.gameObject.tag == "Enemy"){
        //     //Vector2 distance = collision.transform.position - transform.position;
        //     Vector2 distance = (collision.transform.position - transform.position).normalized;

        //     //Debug.Log(distance);
        //     collision.transform.position = new Vector2(collision.transform.position.x + 2 * distance.x, collision.transform.position.y + 2 * distance.y);
        // }
    }
    
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Enemy")
    //     {
    //         Debug.Log("Enemy Attack Player");

    //         Vector2 distance = collision.transform.position - this.transform.position;
    //         this.transform.position = new Vector2(this.transform.position.x + distance.x, this.transform.position.y + distance.y);

    //         // Debug.Log("Enemy Attack Player");
    //         // //TODO
    //         // //给敌人造成伤害，减少敌人hp

    //         // // 获取怪物的Rigidbody组件
    //         // Rigidbody monsterRigidbody = collision.gameObject.GetComponent<Rigidbody>();
 
    //         // // 如果存在，给予一个向外的力
    //         // if (monsterRigidbody != null)
    //         // {
    //             // 给怪物应用力
    //             // rb.AddForce(-transform.forward * 1000f, ForceMode2D.Force);
    //         // rb.AddForce(Vector2.up * 100f, ForceMode2D.Force);
    //         //rb.AddForce(Vector2.up * 100f, ForceMode2D.Impulse);
    //         isttt = true;
    //     }
    // }

}
