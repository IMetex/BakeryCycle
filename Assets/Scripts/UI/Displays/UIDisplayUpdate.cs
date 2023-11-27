using System;
using Manager;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIDisplayUpdate : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyValueText;
        [SerializeField] private TMP_Text _countValueText;
        [SerializeField] private TMP_Text[] _salesValueText;
        [SerializeField] private TMP_Text _upgaredCountValueText;
        [SerializeField] private TMP_Text _upgaredSpeedValueText;

        private CountManager _count;

        private void Start()
        {
            _count = CountManager.Instance;
        }

        private void OnEnable()
        {
            CountManager.Instance.MoneyChanged += OnUpdateDisplay;
            CountManager.Instance.CountDisplay += OnUpdateDisplay;
            CountManager.Instance.SalesChanged += OnUpdateDisplay;
            UpdateUI();
        }

        private void OnDisable()
        {
            CountManager.Instance.MoneyChanged -= OnUpdateDisplay;
            CountManager.Instance.CountDisplay -= OnUpdateDisplay;
            CountManager.Instance.SalesChanged -= OnUpdateDisplay;
        }

        private void OnUpdateDisplay(int newCount)
        {
            UpdateUI();
        }

        private void UpdateMoneyUI(int moneyValue)
        {
            _moneyValueText.text = moneyValue.ToString();
        }

        private void UpdateCountUI(int countValue)
        {
            _countValueText.text = countValue.ToString() + '/' + CountManager.Instance.ObjectAmount;

            _upgaredCountValueText.text = "Current: " + CountManager.Instance.ObjectAmount.ToString();
            _upgaredSpeedValueText.text = "Current: " + CountManager.Instance.MoveSpeed.ToString();
        }

        private void UpdateSalesUI(int salesValue)
        {
            foreach (var salesText in _salesValueText)
            {
                salesText.text = salesValue.ToString();
            }
        }

        private void UpdateUI()
        {
            UpdateMoneyUI(CountManager.Instance.Money);
            UpdateCountUI(CountManager.Instance.ObjectCount);
            UpdateSalesUI(CountManager.Instance.SalesValue);
        }
    }
}