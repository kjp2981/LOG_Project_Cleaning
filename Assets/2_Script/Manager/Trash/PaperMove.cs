using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperMove : MonoBehaviour
{
    private float speed = 1.5f;

    private bool isJudgment = false;

    private void Update()
    {
        //SetRotation();
    }
    private void OnEnable()
    {
        transform.DOShakePosition(1, new Vector3(1, 1, 0), 1, 90);
        transform.DOMove(Vector2.zero, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.Paper, gameObject);
        });
    }

    private void SetRotation()
    {
        Vector2 direction = new Vector2(
        transform.position.x - Vector2.zero.x,
        transform.position.y - Vector2.zero.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, speed * Time.deltaTime);
        transform.rotation = angleAxis;
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
            ObjectPool.Instance.ReturnObject(PoolObjectType.Paper, gameObject);
        }
        if (collision.CompareTag("NoJudgment"))
        {
            transform.DOKill();
            GameManager.Instance.Dead();
            ObjectPool.Instance.ReturnObject(PoolObjectType.Paper, gameObject);
        }
        isJudgment = false;
    }
}
