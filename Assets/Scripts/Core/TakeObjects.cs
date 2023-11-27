using Manager;
using UnityEngine;

namespace ObjectActions
{
    public class TakeObjects : TriggerAction
    {
        [Header("Transfrom References")] [SerializeField]
        private Transform _playerSpawnHolder;

        [SerializeField] private GameObject _spawnHolder;
        [SerializeField] private GameObject _plateGameObject;

        private float _currentHeight = 0f;
        private float _heightIncrement = 0.2f;
        private float _spawnDelay = 0.01f;

        private bool isDestoyed = false;

        protected override void ExecuteTriggerActionStay() { }

        protected override void ExecuteTriggerActionEnter()
        {
            if (ListManager.Instance.isDoughInHand)
                return;

            DestroyObjects();
        }

        protected override void ExecuteTriggerActionExit()
        {
            if (!isDestoyed)
                return;

            StartCoroutine(CoreUtility.Instance.SpawnObjectsCoroutine(
                ListManager.Instance.pizzaList, ListManager.Instance.standList,
                _playerSpawnHolder, _currentHeight, _heightIncrement, _spawnDelay));

            isDestoyed = false;
            ListManager.Instance.isBakeInHand = true;
            CoreUtility.Instance.TakeTweenAnimaton(_plateGameObject);
            
        }

        private void DestroyObjects()
        {
            CoreUtility.Instance.DestroyChildren(_spawnHolder);
            isDestoyed = true;
        }
    }
}