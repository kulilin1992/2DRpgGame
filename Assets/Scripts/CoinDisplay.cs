using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    static Text coinText;

    void Awake()
    {
        coinText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //CurrencyManager.Instance.ResetCoin();
        CurrencyManager.Instance.LoadCurrency();
    }

    // Update is called once per frame
    public static void UpdateText(int coin)
    {
        coinText.text = coin.ToString();
    }
}
