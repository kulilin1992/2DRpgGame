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
            PoolManager.Release(destoryEffect, transform.position, Quaternion.identity);
            //Instantiate(destoryEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            lifeTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<HealthBar>().hp -= 10;
            other.GetComponentInChildren<HealthBar>().UpdateHp();
            PoolManager.Release(attackEffect, transform.position, Quaternion.identity);
            //Instantiate(attackEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            lifeTimer = 0;
        }

        if (other.gameObject.tag == "Wall")
        {
            PoolManager.Release(attackEffect, transform.position, Quaternion.identity);
            //Instantiate(attackEffect, transform.position, Quaternion.identity);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            lifeTimer = 0;
        }
    }
}
