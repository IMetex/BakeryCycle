using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class StandProceses : TriggerAction
    {
        [Header("UI Settings")] [SerializeField]
        private Image fillImage;

        [Header("Fill Speed")] [SerializeField]
        private float fillSpeed = 0.5f;

        [Header("Transfrom References")] [SerializeField]
        private GameObject _playerSpawnHolder;

        [SerializeField] private Transform _spawnHolder;
        [SerializeField] private GameObject _pizzaPrefab;

        private float _currentHeight = 0f;
        private float _heightIncrement = 0.2f;
        private const float _spawnDelay = 0.01f;
        private bool _isDestroy = false;
        
        protected override void ExecuteTriggerActionStay()
        {
            if (ListManager.Instance.isDoughInHand)
                return;

            FillProgressBar();
        }

        protected override void ExecuteTriggerActionEnter() { }

        protected override void ExecuteTriggerActionExit()
        {
            if (!_isDestroy)
                return;

            int numberOfCustomers = ListManager.Instance.customerList.Count;
            _currentHeight = numberOfCustomers * _heightIncrement;

            StartCoroutine(CoreUtility.Instance.SpawnObjectsCoroutine(
                ListManager.Instance.standList, ListManager.Instance.customerList,
                _spawnHolder,  _currentHeight, _heightIncrement, _spawnDelay));
            
            _isDestroy = false;
          
        }

        private void FillProgressBar()
        {
            if (fillImage.fillAmount < 1f && !_isDestroy)
            {
                fillImage.fillAmount += fillSpeed * Time.deltaTime;
            }
            else
            {
                DestroyObjects();
                _isDestroy = true;
                ListManager.Instance.isBakeInHand = false;
            }
        }

        private void DestroyObjects()
        {
            CoreUtility.Instance.DestroyChildren(_playerSpawnHolder);
            ResetFillBar();
        }
        private void ResetFillBar()
        {
            fillImage.fillAmount = 0;
        }
    }
}