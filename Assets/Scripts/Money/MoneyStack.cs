using DG.Tweening;
using Manager;
using Singleton;
using UnityEngine;

namespace Money
{
    public class MoneyStack : Singleton<MoneyStack>
    {
        public GameObject moneyPrefab;
        private float zOffset = 0;
        private float yOffset = 0;
        private float xOffset = 0;

        public void InstantiateMoney(Transform parentPos, Transform endPos)
        {
            Vector3 endPosition = new Vector3(
                endPos.position.x + xOffset,
                endPos.position.y + yOffset,
                endPos.position.z + zOffset);

            Quaternion rotation = Quaternion.Euler(-90, 90, 0);
            var moneyObject = Instantiate(moneyPrefab, parentPos.transform.position, rotation);
            moneyObject.transform.DOLocalJump(endPosition, 2f, 2, 2f);
            ListManager.Instance.moneyList.Add(moneyObject);
            
            zOffset += 1f;

            if (zOffset >= 4f)
            {
                zOffset = 0f;
                xOffset += 1.5f;

                if (xOffset >= 3f)
                {
                    xOffset = 0f;
                    yOffset += 0.5f;
                }
            }
        }

        public void ResetOffsets()
        {
            zOffset = 0f;
            yOffset = 0f;
            xOffset = 0f;
        }
    }
}