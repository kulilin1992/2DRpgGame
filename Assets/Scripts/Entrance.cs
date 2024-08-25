using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{

    [SerializeField] private string entranceSceneVerify;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance.nextSceneVerify == entranceSceneVerify)
        {
            PlayerController.instance.transform.position = transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
