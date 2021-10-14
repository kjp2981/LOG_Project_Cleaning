using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText = null;

    public void UpdateUi()
    {
        scoreText.text = string.Format("{0}", GameManager.Instance.Score);
    }
}
