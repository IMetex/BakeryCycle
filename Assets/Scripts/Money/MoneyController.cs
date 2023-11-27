using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using Money;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private int value = 25;

    [SerializeField] private RectTransform _monyeIcon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ListManager.Instance.moneyList == null)
                return;

            MoneyInceraseUI();
            IconTweenAnim(_monyeIcon);
            DestoyMoneyObject();

            DOTween.Kill(true);
        }
    }

    private void MoneyInceraseUI()
    {
        int unitPrice = ListManager.Instance.moneyList.Count * value;

        int totalMoney = CountManager.Instance.Money + unitPrice;

        DOTween.To(() => CountManager.Instance.Money,
            x => CountManager.Instance.Money = x, totalMoney, 1f);
    }

    private void IconTweenAnim(RectTransform obj)
    {
        var scale = Vector3.one;
        Vector3 doScale = scale * 1.5f;

        obj.transform.DOScale(doScale, 0.04f).OnComplete(() =>
            obj.transform.DOScale(scale, 0.04f));
    }

    private void DestoyMoneyObject()
    {
        GameObject[] destroyCash = GameObject.FindGameObjectsWithTag("Cash");

        foreach (GameObject cash in destroyCash)
        {
            Destroy(cash);
        }

        ListManager.Instance.moneyList.Clear();
        MoneyStack.Instance.ResetOffsets();
    }
}