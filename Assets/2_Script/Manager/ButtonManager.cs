using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] scenes = null;
    public void StartScene()
    {
        scenes[0].SetActive(false);
        scenes[1].SetActive(true);
        scenes[2].SetActive(false);
        GameManager.Instance.GameStart();
    }

    public void MenuScene()
    {
        GameManager.Instance.gameOver = false;
        scenes[0].SetActive(true);
        scenes[1].SetActive(false);
        scenes[2].SetActive(false);
    }
    
    public void OpenSetting()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
