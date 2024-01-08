using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        MoneyIcon.GetComponent<RectTransform>().Rotate(0, 3f, 0);

        MoneyText.text = ":" + PlayerObj.Money;
    }
    public void CompleteStage()
    {
        
    }
    public void RetryButton()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1.0f;
    }
}