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
        transform.DOMove(new Vector2(transform.position.x * .5f, transform.position.y * .5f), speed).OnComplete(() =>
        {
            transform.DOMoveX(Vector2.zero.x, .5f).SetEase(Ease.OutQuad);
            transform.DOMoveY(Vector2.zero.y, .5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                Pool();
            });
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
