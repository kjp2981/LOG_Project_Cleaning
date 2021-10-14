using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChickenMove : MonoBehaviour
{
    private float speed = 1.5f;

    private bool isJudgment = false;

    void Start()
    {
        Destroy(gameObject.GetComponent<GameManager>());

        transform.DOMove(Vector2.zero, speed).OnComplete(() =>
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.Chicken, gameObject);
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
            ObjectPool.Instance.ReturnObject(PoolObjectType.Chicken, gameObject);
        }
        if (collision.CompareTag("NoJudgment"))
        {
            GameManager.Instance.Dead();
            ObjectPool.Instance.ReturnObject(PoolObjectType.Chicken, gameObject);
        }
        isJudgment = false;
    }
}
