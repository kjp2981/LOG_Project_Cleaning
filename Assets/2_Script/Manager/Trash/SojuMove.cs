using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SojuMove : MonoBehaviour
{
    private float speed = .75f;

    private bool isJudgment = false;

    //private void OnEnable()
    //{
    //    transform.DOMove(new Vector2(transform.position.x * .5f, transform.position.y * .5f), speed).SetEase(Ease.Linear).OnComplete(() =>
    //    {
    //        transform.DOMoveX(Vector3.zero.x, .5f).SetEase(Ease.OutQuad);
    //        transform.DOMoveY(Vector3.zero.y, .5f).SetEase(Ease.InQuad).OnComplete(() =>
    //        {
    //            ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
    //        });
    //        GameObject newSoju = ObjectPool.Instance.GetObject(PoolObjectType.Soju);
    //        newSoju.transform.position = transform.position;
    //        newSoju.transform.DOMoveX(Vector3.zero.x, .5f).SetEase(Ease.InQuad);
    //        newSoju.transform.DOMoveY(Vector3.zero.y, .5f).SetEase(Ease.OutQuad).OnComplete(() =>
    //        {
    //            ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
    //        });
    //    });
    //}

    private void OnEnable()
    {
        transform.DOMove(new Vector2(transform.position.x * .5f, transform.position.y * .5f), speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveX(Vector3.zero.x, .5f).SetEase(Ease.OutQuad);
            transform.DOMoveY(Vector3.zero.y, .5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
            });
            //int random = Random.Range(0, 101);
            //if (random > 50)
            //{
            //    transform.DOMoveX(Vector3.zero.x, .5f).SetEase(Ease.OutQuad);
            //    transform.DOMoveY(Vector3.zero.y, .5f).SetEase(Ease.InQuad).OnComplete(() =>
            //    {
            //        ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
            //    });
            //}
            //else
            //{
            //    transform.DOMoveX(Vector3.zero.x, .5f).SetEase(Ease.InQuad);
            //    transform.DOMoveY(Vector3.zero.y, .5f).SetEase(Ease.OutQuad).OnComplete(() =>
            //    {
            //        ObjectPool.Instance.ReturnObject(PoolObjectType.Soju, gameObject);
            //    });
            //}
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
