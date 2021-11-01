using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppleMove : MonoBehaviour
{
    private const float iniSpeed = 1.5f;
    private float speed = 1.5f;

    private bool isJudgment = false;

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameOver)
            Pool();
    }

    private void OnEnable()
    {
        speed = iniSpeed - (GameManager.Instance.time * .01f);
        if (speed <= 0)
            speed = .01f;
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
        ObjectPool.Instance.ReturnObject(PoolObjectType.Apple, gameObject);
    }
}