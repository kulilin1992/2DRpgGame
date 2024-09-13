using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : PersistenSingleton<CurrencyManager>
{

    int coin;
    int diamond;

    int currentCoin;
    int currentDiamond;

    // Start is called before the first frame update
    public void ResetCoin() {
        coin = 0;
        currentCoin = 0;
        CoinDisplay.UpdateText(coin);
    }

    public void ResetDiamond() {
        diamond = 0;
        currentDiamond = 0;
        DiamondDisplay.UpdateText(diamond);
    }
    public void ResetCoinAndDiamond() {
        ResetCoin();
        ResetDiamond();
    }

    public void AddCoin(int amount) {
        currentCoin += amount;
        StartCoroutine(nameof(AddCoinCoroutine));
    }

    public void AddDiamond(int amount) {
        currentDiamond += amount;
        StartCoroutine(nameof(AddDiamondCoroutine));
    }

    IEnumerator AddCoinCoroutine() {
        Debug.Log(currentCoin);
        while (coin < currentCoin) {
            coin +=1;
            CoinDisplay.UpdateText(coin);
            yield return null;
        }
    }

    IEnumerator AddDiamondCoroutine() {
        while (diamond < currentDiamond) {
            diamond +=1;
            DiamondDisplay.UpdateText(diamond);
            yield return null;
        }
    }

    public int GetCoin() {
        return currentCoin;
    }

    public int GetDiamond() {
        return currentDiamond;
    }

    internal void SetCoin(int v)
    {
        currentCoin = v;
        CoinDisplay.UpdateText(currentCoin);
    }

    internal void SetDiamond(int v)
    {
        currentDiamond = v;
    }

    public void SaveCurrency() {
        PlayerPrefs.SetInt("Coins", currentCoin);
        PlayerPrefs.SetInt("Diamonds", currentDiamond);
    }

    public void LoadCurrency() {
        // if (PlayerPrefs.HasKey("Coins")) {
        //     currentCoin = PlayerPrefs.GetInt("Coins");
        // } else {
        //     currentCoin = 0;
        // }
        currentCoin = PlayerPrefs.GetInt("Coins");
        coin = currentCoin;
        CoinDisplay.UpdateText(currentCoin);

        currentDiamond = PlayerPrefs.GetInt("Diamonds");
        diamond = currentDiamond;
        DiamondDisplay.UpdateText(currentDiamond);
        //CurrencyManager.Instance.SetDiamond(PlayerPrefs.GetInt("Diamonds"));
    }
}
