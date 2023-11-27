using Animation;
using System;
using Manager;
using UI;
using UnityEngine;

namespace Path
{
    public class CharacterPathFollower : MonoBehaviour
    {
        [Header("Path Settings")] public Transform pathsParent;

        [Header("Movement Settings")] public float moveSpeed = 5.0f;
        public float rotationSpeed = 5.0f;
        public float minDistance = 0.1f;

        private Transform[] pathPoints;
        private Transform currentPathPoint;
        private int currentPathIndex = 0;

        public AudioSource footSteps;
        private bool isMoving = false;

        private void Start()
        {
            moveSpeed = CountManager.Instance.MoveSpeed;

            pathPoints = new Transform[pathsParent.childCount];
            for (int i = 0; i < pathsParent.childCount; i++)
            {
                pathPoints[i] = pathsParent.GetChild(i);
            }

            if (pathPoints.Length > 0)
            {
                currentPathPoint = pathPoints[currentPathIndex];
            }
        }

        private void Update()
        {
            HandleTouchInput();

            if (Input.GetKey(KeyCode.Space) || isMoving)
            {
                MoveTowardsPathPoint();
                AnimationController.Instance.SetBoolean("Walk", true);
                UIPageController.Instance.PlayTextUIMove();
                footSteps.enabled = true;
            }
            else
            {
                AnimationController.Instance.SetBoolean("Walk", false);
                UIPageController.Instance.PlayTextUINotMove();
                footSteps.enabled = false;
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isMoving = true;
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isMoving = false;
                        break;
                }
            }
        }

        private void MoveTowardsPathPoint()
        {
            if (currentPathPoint == null)
                return;

            Vector3 targetPosition = currentPathPoint.position;
            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance <= minDistance)
            {
                currentPathIndex++;

                if (currentPathIndex >= pathPoints.Length)
                {
                    currentPathIndex = 0;
                }

                currentPathPoint = pathPoints[currentPathIndex];
            }

            Vector3 moveDirection = targetPosition - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}