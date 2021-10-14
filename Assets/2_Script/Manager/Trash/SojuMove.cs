using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SojuMove : MonoBehaviour
{
    private float speed = 1.5f;

    private Transform targetPosition = null;

    private bool isJudgment = false;

    void Start()
    {
        targetPosition = FindObjectOfType<RBMove>().transform;

        transform.DOMove(new Vector2(transform.position.x * .5f, transform.position.y * .5f), speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.position = Vector3.Slerp(transform.position, Vector3.zero, 1f);
            ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
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
            ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
        }
        if (collision.CompareTag("NoJudgment"))
        {
            transform.DOKill();
            GameManager.Instance.Dead();
            ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
        }
        isJudgment = false;
    }
}
