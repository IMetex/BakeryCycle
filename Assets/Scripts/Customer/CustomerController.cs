using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Manager;
using Money;
using ObjectPooler;
using Singleton;
using UnityEngine;

namespace Customer
{
    public class CustomerController : Singleton<CustomerController>
    {
        public GameObject parent;
        public Transform _moneySpwanPoint;
        public Transform endPosition;
        public ParticleSystem _moneyBrust;
        public bool IsEnter { get; private set; }

        [Header("Customer Instatiate Setting")] [SerializeField]
        private GameObject _customerPrefab;

        [SerializeField] private Transform _customerSpwanPoint;
            
        private void Start()
        {
            StartCoroutine(GenerateCustomers());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Customer"))
            {
                HandleCustomerExit();
                MoneyStack.Instance.InstantiateMoney(_moneySpwanPoint, endPosition);
                _moneyBrust.Play();
            }
        }

        private void HandleCustomerExit()
        {
            if (parent == null)
                return;

            Transform[] children = parent.GetComponentsInChildren<Transform>();
            for (int i = children.Length - 1; i >= 0; i--)
            {
                if (children[i] != parent.transform)
                {
                    Destroy(children[i].gameObject);
                    int lastIndex = ListManager.Instance.customerList.Count - 1;
                    ListManager.Instance.customerList.RemoveAt(lastIndex);
                    break;
                }
            }
        }

        private IEnumerator GenerateCustomers()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);

                int customerCount = ListManager.Instance.customerList.Count;

                if (customerCount > 0)
                {
                    InstantiateCustomer();
                }
                else
                {
                    StopCoroutine(GenerateCustomers());
                }
            }
        }

        private void InstantiateCustomer()
        {
            Quaternion rotation = Quaternion.Euler(0, -90, 0);
            GameObject newCustomer = Instantiate(
                _customerPrefab, _customerSpwanPoint.position, rotation);
        }
    }
}