using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChickenMove : MonoBehaviour
{
    private float speed = 1.5f;

    private bool isJudgment = false;

    private void Update()
    {
        if (GameManager.Instance.gameOver)
            Pool();
    }

    private void OnEnable()
    {
        transform.DOMove(Vector2.zero, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Pool();
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isJudgment) return;
        isJudgment = true;
        if (collision.CompareTag("Judgment"))
        {
            GameManager.Instance.AddScore(10);
            GameManager.Instance.Ui.UpdateUi();
            Pool();
        }
        if (collision.CompareTag("NoJudgment"))
        {
            GameManager.Instance.Dead();
            GameManager.Instance.delay -= 1f;
            Pool();
        }
        isJudgment = false;
    }

    private void Pool()
    {
        ObjectPool.Instance.ReturnObject(PoolObjectType.Chicken, gameObject);
    }
}
