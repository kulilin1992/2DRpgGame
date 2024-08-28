using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isRotating;

    [SerializeField] private float moveSpeed;

    private Vector3 targetPos;

    private bool isClicked;
    private bool isDamage;

    private bool canCallBack;
    private bool isBack;
    private Transform playerTransform;

    private CameraController cameraController;

    public bool isInHand;

    //Trail Effect
    private TrailRenderer tr;

    //MARKER EFFECT
    [SerializeField] private GameObject slashEffect;
    [SerializeField] private GameObject weaponReturnEffect;
    // Start is called before the first frame update
    void Start()
    {
        isInHand = true;
        playerTransform = PlayerController.instance.transform;
        cameraController = FindObjectOfType<CameraController>();

        tr = GetComponentInChildren<TrailRenderer>();
        tr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SelfRotation();
        if (Input.GetMouseButtonDown(0) && isClicked == false)
        {
            isClicked = true;
            targetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
        if (isClicked && isInHand)
        {
            isRotating = true;
            ThrowWeapon();
        }

        ReachAtMousePosition();

        if (Input.GetMouseButtonDown(0) && canCallBack)
        {
            isBack = true;
        }
        if (isBack)
        {
            BackWeapon();
        }

        ReachAtPlayerPosition();

        if (!isClicked && !isBack && !canCallBack)
        {
            transform.position = playerTransform.position;
        }
    
    }

    private void ReachAtPlayerPosition()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < 0.01f)
        {
            isRotating = false;
            isDamage = false;
            canCallBack = false;
            isClicked = false;
            isBack = false;
            isInHand = true;

            //transform.rotation = Quaternion.identity;
            transform.rotation = new Quaternion(0, 0, 0, 0);

            tr.enabled = false;
        }
    }

    private void ReachAtMousePosition()
    {
         if (Vector2.Distance(transform.position, targetPos) < 0.01f)
        {
            isRotating = false;
            isDamage = false;
            canCallBack = true;
            tr.enabled = false;
            isInHand = false;
        }
    }

    private void ThrowWeapon()
    {
        //transform.position = Input.mousePosition;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // targetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        //                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        isRotating = true;
        isDamage = true;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        tr.enabled = true;
    }

    private void BackWeapon()
    {
        tr.enabled = true;
        isRotating = true;
        isDamage = true;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * 5 * Time.deltaTime);
        if (Vector2.Distance(transform.position, playerTransform.position) < 0.01f)
        {
            StartCoroutine(CallBackEffect());
            Instantiate(weaponReturnEffect, transform.position, Quaternion.identity);
        }
    }

    

    private void SelfRotation()
    {
        if (isRotating)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && isDamage)
        {
            //TODO
            //给敌人造成伤害，减少敌人hp

            cameraController.isShaked = true;
            cameraController.CameraShake(0.2f);

            Instantiate(slashEffect, transform.position, Quaternion.identity);
        }
    }

    IEnumerator CallBackEffect()
    {
        cameraController.isShaked = true;
        cameraController.CameraShake(0.2f);
        yield return new WaitForSeconds(0.6f);

        cameraController.isShaked = false;
    }
}
