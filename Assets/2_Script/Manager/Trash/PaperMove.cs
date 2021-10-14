using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperMove : MonoBehaviour
{
    private float speed = 1.5f;

    private Transform targetPosition = null;

    private bool isJudgment = false;

    void Start()
    {
        targetPosition = FindObjectOfType<RBMove>().transform;

        SetRotation();
        transform.DOMove(Vector2.zero, speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.Paper, gameObject);
        });
    }

    private void SetRotation()
    {
        if (targetPosition != null)
        {
            Vector2 direction = new Vector2(
                transform.position.x - targetPosition.position.x,
                transform.position.y - targetPosition.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, speed * Time.deltaTime);
            transform.rotation = angleAxis;
        }
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
