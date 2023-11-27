using System;
using Manager;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace ObjectActions
{
    public class BakeProcesess : TriggerAction
    {
        [Header("UI Settings")] [SerializeField]
        private Image fillImage;

        [Header("Fill Speed")] [SerializeField]
        private float fillSpeed = 0.5f;

        [Header("Transfrom References")] [SerializeField]
        private GameObject _playerSpawnHolder;

        [SerializeField] private Transform _spawnHolder;

        [Header("Partical Effect")] [SerializeField]
        private ParticleSystem _smokePartical;

        [SerializeField] private GameObject _pizzaOnbject;

        private float _currentHeight = 0f;
        private float _heightIncrement = 0.2f;
        private float _spawnDelay = 2f;
        private bool isDestroy = false;

        protected override void ExecuteTriggerActionStay()
        {
            if (ListManager.Instance.isBakeInHand)
                return;

            FillProgressBar();
        }

        protected override void ExecuteTriggerActionEnter()
        {
            isDestroy = false;
        }

        protected override void ExecuteTriggerActionExit()
        {
            if (!isDestroy)
                return;

            int numberOfPizza = ListManager.Instance.pizzaList.Count;
            _currentHeight = numberOfPizza * _heightIncrement;

            StartCoroutine(CoreUtility.Instance.SpawnObjectsCoroutine(
                ListManager.Instance.doughList, ListManager.Instance.pizzaList, _spawnHolder,
                _currentHeight, _heightIncrement, _spawnDelay));

            _heightIncrement = 0.2f;
            ParticalAndBakeObjectVisibility();
        }

        private void FillProgressBar()
        {
            if (fillImage.fillAmount < 1f && CountManager.Instance.ObjectCount > 0)
            {
                fillImage.fillAmount += fillSpeed * Time.deltaTime;
            }
            else
            {
                DestroyObjects();
                isDestroy = true;
                ListManager.Instance.isDoughInHand = false;
            }
        }

        private void DestroyObjects()
        {
            CoreUtility.Instance.DestroyChildren(_playerSpawnHolder);
            CountManager.Instance.ObjectCount = 0;
            ResetFillBar();
            ParticalAndBakeObjectVisibility();
        }

        private void ResetFillBar()
        {
            fillImage.fillAmount = 0;
        }

        private void ParticalAndBakeObjectVisibility()
        {
            if (ListManager.Instance.doughList.Count > 0)
            {
                _smokePartical.Play();
                _pizzaOnbject.SetActive(true);
            }
            else
            {
                _smokePartical.Stop();
                _pizzaOnbject.SetActive(false);
            }
        }
    }
}