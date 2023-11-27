using UnityEngine;

namespace Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }
    }
}