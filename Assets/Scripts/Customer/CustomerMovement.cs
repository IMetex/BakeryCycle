using System;
using Animation;
using DG.Tweening;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Material[] materials;

        private Animator _animator;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Rigidbody _rigidbody;
        private float firtPosValue = 20f;
        private float secondPosValue = 40f;

        private void Awake()
        {
            DOTween.SetTweensCapacity(1250, 100);
            GetReferances();
        }

        private void GetReferances()
        {
            _animator = GetComponent<Animator>();
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SetRandomMaterial();
        }

        private void Update()
        {
            if (CanMove())
            {
                MoveCharacter();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                GetNextMovePosition();
            }
        }

        private void MoveCharacter()
        {
            var endPosition = new Vector3(firtPosValue, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
        }

        private void GetNextMovePosition()
        {
            if (ListManager.Instance.customerList == null || ListManager.Instance.customerList.Count == 0)
                return;

            MoveToNextPosition();
        }

        private void MoveToNextPosition()
        {
            transform.DORotate(Vector3.zero, 0.2f)
                .SetDelay(1f)
                .OnComplete(() =>
                {
                    transform.DORotate(Vector3.zero, 0.2f)
                        .SetDelay(1f)
                        .OnComplete(() =>
                        {
                            var nextPosition = new Vector3(transform.position.x, transform.position.y, secondPosValue);
                            DOTween.To(() => transform.position, x => transform.position = x, nextPosition, 10f)
                                .SetEase(Ease.Linear)
                                .OnComplete(() =>
                                {
                                    gameObject.SetActive(false);
                                    DOTween.Kill(transform); 
                                });
                        });
                });
        }

        private void SetRandomMaterial()
        {
            int materialIndex = Random.Range(0, materials.Length);
            _skinnedMeshRenderer.material = materials[materialIndex];
        }

        private bool CanMove()
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, transform.forward * 4f, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
            {
                return hit.collider.CompareTag("Finish");
            }

            return true;
        }
    }
}