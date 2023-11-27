using Manager;
using Path;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class ValuesUpgarededOnButton : MonoBehaviour
    {
        [SerializeField] private Button _countAmountBtn;
        [SerializeField] private Button _speedBtn;

        private void OnEnable()
        {
            _countAmountBtn.onClick.AddListener(OnCountAmountButtonClicked);
            _speedBtn.onClick.AddListener(OnSpeedButtonClicked);
        }

        private void OnDisable()
        {
            _countAmountBtn.onClick.RemoveListener(OnCountAmountButtonClicked);
            _speedBtn.onClick.RemoveListener(OnSpeedButtonClicked);
        }

        private void OnCountAmountButtonClicked()
        {
            if (IsEnoughMoney())
            {
                CountManager.Instance.UpdateCountAmount();
                UIPageController.Instance.IsEnoughMoneyF();
            }
            else
            {
                UIPageController.Instance.IsEnoughMoneyT();
            }
        }

        private void OnSpeedButtonClicked()
        {
            if (IsEnoughMoney())
            {
                CountManager.Instance.UpdateSpeedValue();
                UIPageController.Instance.IsEnoughMoneyF();
            }
            else
            {
                UIPageController.Instance.IsEnoughMoneyT();
            }
        }

        private bool IsEnoughMoney()
        {
            int upgradeCost = CountManager.Instance.SalesValue;
            return CountManager.Instance.Money >= upgradeCost;
        }
    }
}