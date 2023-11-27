using System;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class DoughMachineProcesses : TriggerAction
    {
        [Header("UI Elements")] [SerializeField]
        private Image fillImage;

        [Header("Fill Speed")] [SerializeField]
        private float fillSpeed = 0.5f;

        [Header("Object Spawning")] [SerializeField]
        private GameObject doughPrefab;

        [SerializeField] private GameObject _pizzaPrefab;
        [SerializeField] private Transform spawnPoint;

        [Header("Rotation Settings")] [SerializeField]
        private GameObject rotationObject;

        [SerializeField] private float rotationDuration = 2f;

        [Header("Animation Settings")] [SerializeField]
        private GameObject _plateObject;

        private float _currentHeight = 0f;
        private float _objectHeightIncrement = 0.35f;

        private CoreUtility _coreUtility;

        private void Start()
        {
            _coreUtility = CoreUtility.Instance;
        }

        protected override void ExecuteTriggerActionEnter()
        {
            UIPageController.Instance.PlayerUICountTextEnter();

            if (ListManager.Instance.isDoughInHand)
                return;

            _currentHeight = 0f;
        }

        protected override void ExecuteTriggerActionStay()
        {
            if (ListManager.Instance.isBakeInHand)
                return;

            RotationMachine();
            FillProgressBar();
        }

        protected override void ExecuteTriggerActionExit()
        {
            UIPageController.Instance.PlayerUICountTextExit();

            DOTween.Kill(true);

            if (ListManager.Instance.isDoughInHand)
                return;


            _currentHeight = 0f;
        }

        private void FillProgressBar()
        {
            if (fillImage.fillAmount < 1f)
            {
                fillImage.fillAmount += fillSpeed * Time.deltaTime;
            }
            else if (CountManager.Instance.ObjectCount < CountManager.Instance.ObjectAmount)
            {
                SpawnNewObject();
                CoreUtility.Instance.TakeTweenAnimaton(_plateObject);
                ListManager.Instance.isDoughInHand = true;
            }
        }

        private void SpawnNewObject()
        {
            CoreUtility.Instance.InstantiateObject(spawnPoint, doughPrefab, _currentHeight);
            _currentHeight += _objectHeightIncrement;
            CountManager.Instance.ObjectCount++;
            ListManager.Instance.doughList.Add(_pizzaPrefab);
            ResetFillBar();
        }

        private void ResetFillBar()
        {
            fillImage.fillAmount = 0f;
        }

        private void RotationMachine()
        {
            rotationObject.transform.DOLocalRotate(Vector3.back * 360, rotationDuration, RotateMode.LocalAxisAdd);
        }
    }
}