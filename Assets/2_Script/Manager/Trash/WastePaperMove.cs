using UnityEngine;
using DG.Tweening;

public class WastePaperMove : MonoBehaviour
{
    private float speed = 1f;

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
        ObjectPool.Instance.ReturnObject(PoolObjectType.WastePaper, gameObject);
    }
}