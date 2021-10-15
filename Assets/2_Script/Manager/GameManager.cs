using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public Vector2 MaxPosition { get; private set; }
    public Vector2 MinPosition { get; private set; }

    [SerializeField]
    private GameObject lifeObject = null;
    [SerializeField]
    private float delay = 0;
    [SerializeField]
    private Text highScoreTexture = null;
    [SerializeField]
    private Sprite[] lifeImage;
    [SerializeField]
    private GameObject[] scenes = null;

    private float time = 0;
    private int life = 3;
    private int score;
    private int highScore = 0;

    public int Score { get { return score; } }
    private UiManager uiManager;
    public UiManager Ui { get { return uiManager; } }
    private ButtonManager buttonManager = null;

    public bool gameOver = false;
    void Start()
    {
        Application.targetFrameRate = 60;
        uiManager = FindObjectOfType<UiManager>();
        buttonManager = GetComponent<ButtonManager>();
        MaxPosition = new Vector2(4f, 6f);
        MinPosition = new Vector2(-4f, -6f);
        buttonManager.MenuScene();
    }

    void Update()
    {
        time += Time.deltaTime;
        DelayMinus();
    }
    public void GameStart()
    {
        life = 3;
        time = 0;
        score = 0;
        Ui.UpdateUi();
        ChangeLifeImage();
        StartCoroutine(TrashSpawn());
    }

    private IEnumerator TrashSpawn()
    {
        while (true)
        {
            if (life <= 0)
            {
                yield break;
            }
            int randomSpawn = Random.Range(1, 5);
            float RandomX = Random.Range(MaxPosition.x, MinPosition.x);
            float RandomY = Random.Range(MaxPosition.y, MinPosition.y);

            GameObject newTrash = null;
            switch (randomSpawn)
            {
                case 1:
                    newTrash = ChangeTrash(); // 拉率
                    newTrash.transform.position = new Vector2(RandomX, MaxPosition.y);
                    break;
                case 2:
                    newTrash = ChangeTrash(); // 酒贰率
                    newTrash.transform.position = new Vector2(RandomX, MinPosition.y);
                    break;
                case 3:
                    newTrash = ChangeTrash(); // 哭率
                    newTrash.transform.position = new Vector2(MinPosition.x, RandomY);
                    break;
                case 4:
                    newTrash = ChangeTrash(); // 坷弗率
                    newTrash.transform.position = new Vector2(MaxPosition.x, RandomY);
                    break;
                default:
                    break;
            }
            InfiniteLoopDetector.Run();
            yield return new WaitForSeconds(delay);
        }
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        if(highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
    }

    private GameObject ChangeTrash()
    {
        int random = Random.Range(0, 101);
        if(time < 10)
        {
            return ObjectPool.Instance.GetObject(PoolObjectType.WastePaper);
        }
        else if(time < 20)
        {
            if (random < 80)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.WastePaper);
            }
            else
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Paper);
            }
        }
        else if(time < 30)
        {
            if (random < 50)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.WastePaper);
            }
            else if(random < 80)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Paper);
            }
            else
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Apple);
            }
        }
        else if(time < 40)
        {
            if (random < 40)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.WastePaper);
            }
            else if (random < 60)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Paper);
            }
            else if(random < 80)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Apple);
            }
            else
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Chicken);
            }
        }
        else
        {
            if (random < 45)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.WastePaper);
            }
            else if (random < 60)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Paper);
            }
            else if (random < 75)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Apple);
            }
            else if(random < 90)
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Chicken);
            }
            else
            {
                return ObjectPool.Instance.GetObject(PoolObjectType.Soju);
            }
        }
    }

    private void DelayMinus()
    {
        if (time == 0)
            delay = 5;
        else if (time > 0 && time < 11)
            delay = 4.5f;
        else if (time >= 11 && time < 21)
            delay = 4;
        else if (time >= 21 && time < 31)
            delay = 3.5f;
        else if (time >= 31 && time < 41)
            delay = 2.5f;
        else if (time >= 41 && time < 51)
            delay = 2;
        else if (time >= 51 && time < 61)
            delay = 1.5f;
        else
            delay = 1;
    }

    public void Dead()
    {
        life--;
        ChangeLifeImage();
        if (life <= 0)
        {
            gameOver = true;
            scenes[2].SetActive(true);
            scenes[1].SetActive(false);
            highScoreTexture.text = string.Format("{0}", PlayerPrefs.GetInt("HIGHSCORE"));
        }
    }
    public void ChangeLifeImage()
    {
        if(life == 3)
        {
            lifeObject.GetComponent<Image>().sprite = lifeImage[0];
        }
        else if (life == 2)
        {
            lifeObject.GetComponent<Image>().sprite = lifeImage[1];
        }
        else if (life == 1)
        {
            lifeObject.GetComponent<Image>().sprite = lifeImage[2];
        }
        else
        {
            lifeObject.GetComponent<Image>().sprite = lifeImage[3];
        }
    }
}