using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondDisplay : MonoBehaviour
{
    static Text diamondText;
    // Start is called before the first frame update
    void Awake()
    {
        diamondText = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //CurrencyManager.Instance.ResetDiamond();
        CurrencyManager.Instance.LoadCurrency();
    }

    // Update is called once per frame
    public static void UpdateText(int diamond)
    {
        diamondText.text = diamond.ToString();
    }
}
