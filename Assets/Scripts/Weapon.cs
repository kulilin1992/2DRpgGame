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

    [HideInInspector] public Vector3 targetPos;

    private bool isClicked;
    private bool isDamage;

    private bool canCallBack;
    private bool isBack;
    private bool isReachTarget;
    private Transform playerTransform;

    private CameraController cameraController;

    public bool isInHand;

    public bool check;
    private Vector3 wallPosition;

    //Trail Effect
    private TrailRenderer tr;

    //MARKER EFFECT
    [SerializeField] private GameObject slashEffect;
    [SerializeField] private GameObject weaponReturnEffect;
    // Start is called before the first frame update


    AStarFindPath mapBehaviour;
    bool reachEnd = false;
    bool isStarted = false;
    void Start()
    {
        isInHand = true;
        playerTransform = PlayerController.instance.transform;
        cameraController = FindObjectOfType<CameraController>();

        tr = GetComponentInChildren<TrailRenderer>();
        mapBehaviour = FindObjectOfType<AStarFindPath>();
        tr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameIsPaused) {
            return;
        }
        SelfRotation();
        if (Input.GetMouseButtonDown(0) && isClicked == false)
        {
            isClicked = true;
            targetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
        if (isClicked && !isReachTarget)
        {
            isRotating = true;
            ThrowWeapon();
        }

        if (check) {
            ReachAtWallPosition();
        } else {
            ReachAtMousePosition();
        }

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
            isReachTarget = false;

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
            isReachTarget = true;
        }
    }

    private void ReachAtWallPosition()
    {
         if (Vector2.Distance(transform.position, wallPosition) < 0.01f)
        {
            isRotating = false;
            isDamage = false;
            canCallBack = true;
            tr.enabled = false;
            isReachTarget = true;
            check = false;
            canCallBack = true;
            isBack = true;
        }
    }

    private void ThrowWeapon()
    {
        // if (Vector2.Distance(transform.position, playerTransform.position) > 0.01f)
        // {
        //     weaponReturnEffect.gameObject.SetActive(false);
        // }
        //Debug.Log("ThrowWeapon");
        //transform.position = Input.mousePosition;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // targetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        //                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        isInHand = false;
        isRotating = true;
        isDamage = true;
        if (check) {
            transform.position = Vector2.MoveTowards(transform.position, wallPosition, moveSpeed * Time.deltaTime);
            //check = false;
        } else {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        tr.enabled = true;
    }

    private void BackWeapon()
    {
        Debug.Log("BackWeapon");
        //Debug.Log(mapBehaviour);
        // if (mapBehaviour!= null && mapBehaviour.aa.Count > 0 && !isStarted) {
        //     isStarted = true;
        //     StartCoroutine(FindWayPoint(mapBehaviour.aa));
        // }
        //StartCoroutine(FindWayPoint(mapBehaviour.aa));
        //Debug.Log("BackWeapon");
        tr.enabled = true;
        isRotating = true;
        isDamage = true;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * 5 * Time.deltaTime);
        if (Vector2.Distance(transform.position, playerTransform.position) < 0.01f)
        {
            // isStarted = false;
            // StopCoroutine(FindWayPoint(mapBehaviour.aa));
            StartCoroutine(CallBackEffect());
            PoolManager.Release(weaponReturnEffect, transform.position, Quaternion.identity);
            //Instantiate(weaponReturnEffect, transform.position, Quaternion.identity);
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

            // Vector2 distance = (other.transform.position - playerTransform.position).normalized;

            // //Debug.Log(distance);
            // other.transform.position = new Vector2(other.transform.position.x + distance.x, other.transform.position.y + distance.y);
            //TODO
            //给敌人造成伤害，减少敌人hp

            other.GetComponentInChildren<HealthBar>().hp -= 20;
            other.GetComponentInChildren<HealthBar>().UpdateHp();
            other.GetComponent<EnemyController>().HurtShader();

            cameraController.isShaked = true;
            cameraController.CameraShake(0.2f);

            Instantiate(slashEffect, transform.position, Quaternion.identity);
        }
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Wall position:" + transform.position) ;
            wallPosition = transform.position;
            check = true;
        }
    }

    IEnumerator CallBackEffect()
    {
        cameraController.isShaked = true;
        cameraController.CameraShake(0.2f);
        yield return new WaitForSeconds(0.6f);

        cameraController.isShaked = false;
    }

    IEnumerator FindWayPoint(List<Vector3> path)
    {
        foreach (Vector3 v in mapBehaviour.aa)
            {
                //transform.position = mapBehaviour.tilemap.WorldToCell(v);
                transform.position = new Vector3(v.x + 0.5f, v.y+0.5f, 0);
                yield return new WaitForSeconds(0.01f);
                //Debug.Log(transform.position.ToString());
                if (transform.position == mapBehaviour.endPointaa) {
                    //reachEnd = true;
                    StopCoroutine(FindWayPoint(mapBehaviour.aa));
                }
            }
    }
}
