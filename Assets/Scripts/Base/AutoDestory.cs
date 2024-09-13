using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] bool isDestoryGameObject;
    [SerializeField] float lifeTime = 2f;

    WaitForSeconds waitLiftTime;

    void Awake()
    {
        waitLiftTime = new WaitForSeconds(lifeTime);
    }

    void OnEnable()
    {
        StartCoroutine(DestoryCoroutine());
    }

    IEnumerator DestoryCoroutine()
    {
        yield return waitLiftTime;
        if (isDestoryGameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
