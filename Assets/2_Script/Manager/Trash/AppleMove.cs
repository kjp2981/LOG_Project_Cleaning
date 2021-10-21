using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppleMove : MonoBehaviour
{
    private float speed = 1.5f;

    private bool isJudgment = false;

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
            Pool();
        }
        isJudgment = false;
    }

    private void Pool()
    {
        ObjectPool.Instance.ReturnObject(PoolObjectType.Apple, gameObject);
    }
}