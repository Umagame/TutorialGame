using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public UnityEngine.UI.Image MoneyIcon;

    public Player PlayerObj;

    void Start()
    {
        
    }

    void Update()
    {
        MoneyIcon.GetComponent<RectTransform>().Rotate(0, 0.3f, 0);

        MoneyText.text = ":" + PlayerObj.Money;
    }
}
