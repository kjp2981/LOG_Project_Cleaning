using UnityEngine;
using DG.Tweening;

public class WastePaperMove : MonoBehaviour
{
    private float speed = 1.5f;

    private bool isJudgment = false;

    void Start()
    {
       gameObject.GetComponent<WastePaperMove>();

        transform.DOMove(Vector2.zero, speed).OnComplete(() =>
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.WastePaper, gameObject);
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
            ObjectPool.Instance.ReturnObject(PoolObjectType.WastePaper, gameObject);
        }
        if (collision.CompareTag("NoJudgment"))
        {
            transform.DOKill();
            GameManager.Instance.Dead();
            ObjectPool.Instance.ReturnObject(PoolObjectType.WastePaper, gameObject);
        }
        isJudgment = false;
    }
}