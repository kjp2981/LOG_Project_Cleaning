using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SojuMove : MonoBehaviour
{
    private float speed = 1.5f;

    private bool isJudgment = false;

    private void OnEnable()
    {
        transform.DOMoveX(Vector3.zero.x, speed).SetEase(Ease.OutSine);
        transform.DOMoveY(Vector3.zero.y, speed).SetEase(Ease.InSine).OnComplete(() =>
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
            transform.DOKill();
            GameManager.Instance.AddScore(10);
            GameManager.Instance.Ui.UpdateUi();
            Pool();
        }
        if (collision.CompareTag("NoJudgment"))
        {
            transform.DOKill();
            GameManager.Instance.Dead();
            Pool();
        }
        isJudgment = false;
    }

    private void Pool()
    {
        ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
    }
}
