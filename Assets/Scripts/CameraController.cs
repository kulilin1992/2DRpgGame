using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float smoothSpeed = 3.0f;

    [SerializeField] private float minX, minY, maxX, maxY;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = PlayerController.instance.transform;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    // }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                         Mathf.Clamp(transform.position.y, minY, maxY),
                                         transform.position.z);
    }
}
