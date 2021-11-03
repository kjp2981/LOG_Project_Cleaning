using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] scenes = null;
    [SerializeField]
    private GameObject Setting;
    [SerializeField]
    private GameObject RestartButton;
    [SerializeField]
    private GameObject MenuButton;

    public bool isGameStart = false;
    public bool isOpenMenuSecen = true;
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
        isGameStart = true;
        isOpenMenuSecen = false;
        scenes[0].SetActive(false);
        scenes[1].SetActive(true);
        scenes[2].SetActive(false);
        GameManager.Instance.GameStart();
    }

    public void MenuScene()
    {
        isGameStart = false;
        isOpenMenuSecen = true;
        GameManager.Instance.gameOver = false;
        GameManager.Instance.index = 1;
        GameManager.Instance.backgroundMusic.AudioChange();
        scenes[0].SetActive(true);
        scenes[1].SetActive(false);
        scenes[2].SetActive(false);
    }
    
    public void OpenSetting()
    {
        if (isGameStart)
            RestartButton.SetActive(true);
        else
            RestartButton.SetActive(false);
        if (isOpenMenuSecen)
            MenuButton.SetActive(false);
        else
            MenuButton.SetActive(true);
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
    public void OnClickRestart()
    {
        CloseSetting();
        //GameManager.Instance.GameOver();
        GameManager.Instance.Reset();
        GameManager.Instance.Ui.UpdateUi();
        GameManager.Instance.ChangeLifeImage();
        GameManager.Instance.StopCoroutine("TrashSpawn");
        GameManager.Instance.gameOver = true;
        GameManager.Instance.StartCoroutine("TrashSpawn");
    }
    public void OnClickMenu()
    {
        CloseSetting();
        GameManager.Instance.GameOver();
        MenuScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
