using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Image gameMenuImage;
    public Toggle BGMToggle;

    private AudioSource BGMMusic;
    private bool isBGMPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        gameMenuImage.gameObject.SetActive(false);
        BGMMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log(GameManager);
            if (GameManager.Instance.gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        BGMManager();
        
    }

    public void Resume() {
        Debug.Log("Resume");
        gameMenuImage.gameObject.SetActive(false);
        GameManager.Instance.gameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause() {
        Debug.Log("Pause");
        gameMenuImage.gameObject.SetActive(true);
        GameManager.Instance.gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void BGMToggleChanged() {
        if (BGMToggle.isOn) {
            PlayerPrefs.SetInt("BGM", 1);
            Debug.Log("BGM ON");
        }
        else {
            PlayerPrefs.SetInt("BGM", 0);
            Debug.Log("BGM OFF");
        }
        isBGMPlaying = false;
    }

    private void BGMManager()
    {
        if (isBGMPlaying) {
            return;
        }
        if (PlayerPrefs.GetInt("BGM") == 1 && BGMMusic.enabled && BGMToggle.isOn) {
            isBGMPlaying = true;
        }
        if (PlayerPrefs.GetInt("BGM") == 0 && !BGMMusic.enabled && !BGMToggle.isOn) {
            isBGMPlaying = true;
        }
        
        if (PlayerPrefs.GetInt("BGM") == 1)
        {
            BGMMusic.enabled = true;
            BGMToggle.isOn = true;
            // if (!isBGMPlaying)
            // {
            //     //BGMMusic.enabled = true;
            //     isBGMPlaying = false;
            // }
        }
        else if (PlayerPrefs.GetInt("BGM") == 0)
        {
            BGMMusic.enabled = false;
            BGMToggle.isOn = false;
            // if (isBGMPlaying)
            // {
            //     //BGMMusic.enabled = false;
            //     isBGMPlaying = false;
            //     //BGMToggle.isOn = false;
            // }
        }
    }

    public void SaveButton(){
        CurrencyManager.Instance.SaveCurrency();
    }

}
