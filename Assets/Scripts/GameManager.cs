using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : PersistenSingleton<GameManager>
{
    public static GameManager instance;

    public bool gameIsPaused;

    public int coins, diamonds;
    public Text coinsText, diamondsText;
    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    //     //DontDestroyOnLoad(gameObject);
    // }
    // Start is called before the first frame update

    // protected override void Awake()
    // {
    //     LoadCurrency();
    //     base.Awake();
    // }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
