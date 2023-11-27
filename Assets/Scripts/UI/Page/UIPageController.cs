using System;
using DG.Tweening;
using Singleton;
using UnityEngine;

namespace UI
{
    public class UIPageController : Singleton<UIPageController>
    {
        [SerializeField] private GameObject _moneyScreen;
        [SerializeField] private GameObject _settingScreen;
        [SerializeField] private GameObject _upgaredeScreen;
        [SerializeField] private GameObject _playTextScreen;
        [SerializeField] private GameObject _playerCountTextScreen;
        [SerializeField] private GameObject _notEnoughMoney;

        [SerializeField] private Transform _containerUpgarededTransform;

        private void Start()
        {
            _moneyScreen.SetActive(true);
            _settingScreen.SetActive(true);
            _upgaredeScreen.SetActive(false);
        }

        public void UpgarededUIEnter()
        {
            _containerUpgarededTransform.DOLocalMoveY(0, 0.8f)
                .From(-700).SetEase(Ease.OutBack);
            _upgaredeScreen.SetActive(true);
        }

        public void UpgarededUIExit()
        {
            _containerUpgarededTransform.DOLocalMoveY(-700, 0.5f)
                .From(0).SetEase(Ease.InBack)
                .OnComplete(() => _upgaredeScreen.SetActive(false));
        }

        public void PlayTextUIMove()
        {
            _playTextScreen.SetActive(false);
        }

        public void PlayTextUINotMove()
        {
            _playTextScreen.SetActive(true);
        }

        public void PlayerUICountTextEnter()
        {
            _playerCountTextScreen.SetActive(true);
        }

        public void PlayerUICountTextExit()
        {
            _playerCountTextScreen.SetActive(false);
        }

        public void IsEnoughMoneyT()
        {
            _notEnoughMoney.SetActive(true);
        }

        public void IsEnoughMoneyF()
        {
            _notEnoughMoney.SetActive(false);
        }
    }
}