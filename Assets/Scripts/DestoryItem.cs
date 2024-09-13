using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryItem : MonoBehaviour
{
    private Animator anim;
    private CameraController cameraController;
    private BoxCollider2D boxCollider;
    public GameObject slashEffect;

    //掉落物品
    private int randNum;
    public GameObject[] dropItems;

    private bool isBroken;

    void Start()
    {
        anim = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon" && !FindObjectOfType<Weapon>().isInHand && !isBroken)
        {
            Debug.Log("is lalalalal");
            isBroken = true;
            anim.SetTrigger("isDestoryed");
            cameraController.isShaked = true;
            cameraController.CameraShake(0.2f);
            //Instantiate(slashEffect, transform.position, Quaternion.identity);
            
            boxCollider.enabled = false;
            Instantiate(slashEffect, transform.position, Quaternion.identity);
            DropRandomItem();
        }
    }

    public void DropRandomItem()
    {
        randNum = Random.Range(0, 2);
        Instantiate(dropItems[randNum], transform.position, Quaternion.identity);
    }
}
