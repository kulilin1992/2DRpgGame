using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryItem : MonoBehaviour
{
    private Animator anim;
    private CameraController cameraController;
    private BoxCollider2D boxCollider;
    public GameObject slashEffect;

    void Start()
    {
        anim = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon" && !FindObjectOfType<Weapon>().isInHand)
        {
            anim.SetTrigger("isDestoryed");
            cameraController.isShaked = true;
            cameraController.CameraShake(0.2f);
            //Instantiate(slashEffect, transform.position, Quaternion.identity);
            
            boxCollider.enabled = false;
            Instantiate(slashEffect, transform.position, Quaternion.identity);
        }
    }
}
