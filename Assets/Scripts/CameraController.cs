using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float smoothSpeed = 3.0f;

    [SerializeField] private float minX, minY, maxX, maxY;
    private Transform target;

    private float shakeAmplitude;
    private Vector3 shakeActive;
    public bool isShaked;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (shakeAmplitude > 0)
        {
            shakeActive = new Vector3(Random.Range(-shakeAmplitude, shakeAmplitude), Random.Range(-shakeAmplitude, shakeAmplitude), 0);
            shakeAmplitude -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
            isShaked = false;
        }
        if (isShaked)
        {
            transform.position += shakeActive;
        }
    }

    public void CameraShake(float _shakeAmount)
    {
        shakeAmplitude = _shakeAmount;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                         Mathf.Clamp(transform.position.y, minY, maxY),
                                         transform.position.z);
    }
}
