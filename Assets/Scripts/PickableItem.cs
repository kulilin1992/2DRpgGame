using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    //private Transform playerTrans;
    [SerializeField] private float moveSpeed;

    public GameObject pickupEffect;

    private bool isOwned;

    public enum ItemType
    {
        Coin,
        Diamond
    }

    public ItemType itemType;

    private void Start()
    {
        //playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && !isOwned)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.position, PlayerController.instance.transform.position) < 0.01f)
            {
                isOwned = true;
                Instantiate(pickupEffect, PlayerController.instance.transform.position, Quaternion.identity);

                if(itemType == ItemType.Coin)
                {
                    Debug.Log("Collect Coins");
                    CurrencyManager.Instance.AddCoin(1);
                }
                else if(itemType == ItemType.Diamond)
                {
                    Debug.Log("Collect Diamonds");
                    CurrencyManager.Instance.AddDiamond(1);
                }

                Destroy(gameObject);
            }
        }
    }

}
