using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private string newSceneVerify;


    public string sceneName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.nextSceneVerify = newSceneVerify;
            //SceneManager.LoadScene(sceneName);
            FindAnyObjectByType<SceneFade>().FadeTo(sceneName);
        }
    }
}
