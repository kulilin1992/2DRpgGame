using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;
    public Image hpImage;
    public Image hpEffectImage;

    [HideInInspector] public float hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     hpImage.fillAmount = hp / maxHp;
    //     if (hpEffectImage.fillAmount > hpImage.fillAmount)
    //     {
    //         hpEffectImage.fillAmount -= hurtSpeed;
    //     }
    //     else
    //     {
    //         hpEffectImage.fillAmount = hpImage.fillAmount;
    //     }
    // }

    public void UpdateHp()
    {
        StartCoroutine(UpdateHpCo());
    }

    IEnumerator UpdateHpCo()
    {
        hpImage.fillAmount = hp / maxHp;
        while(hpEffectImage.fillAmount >= hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
            yield return new WaitForSeconds(0.005f);
            //Debug.Log("A");
        }
        if(hpEffectImage.fillAmount < hpImage.fillAmount)
        {   
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
}
