using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] scenes = null;
    [SerializeField]
    private GameObject Setting;

    private bool isOpenSetting = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpenSetting)
                CloseSetting();
            else
                OpenSetting();
        }
    }
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
        Setting.SetActive(true);
        isOpenSetting = true;
        Time.timeScale = 0;
    }
    public void CloseSetting()
    {
        Setting.SetActive(false);
        isOpenSetting = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
