using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public RawImage BackgroundImage;
    public float ScrollSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        BackgroundImage.uvRect = new Rect(BackgroundImage.uvRect.position + new Vector2(ScrollSpeed, 0) * Time.deltaTime, BackgroundImage.uvRect.size);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}