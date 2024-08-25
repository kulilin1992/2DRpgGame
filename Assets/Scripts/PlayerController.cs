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
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveHorizontal, moveVertical);
    }
}
