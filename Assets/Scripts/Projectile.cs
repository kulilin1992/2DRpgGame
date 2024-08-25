using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxLifeTime = 2.0f;

    private float lifeTimer;
    private Transform target;
    // Start is called before the first frame update

    public GameObject destoryEffect;
    public GameObject attackEffect;
    void Start()
    {
        target = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxLifeTime)
        {
            Instantiate(destoryEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
