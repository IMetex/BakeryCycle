using System;
using System.Collections.Generic;
using Path;
using Singleton;
using UnityEngine;

namespace Manager
{
    public class CountManager : Singleton<CountManager>
    {
        [SerializeField] private CharacterPathFollower _pathFollower;

        #region Game Int Values

        private int _objectAmount = 5;
        private int _objectCount = 0;
        private int _objectMax = 10;

        private float _maxSpeed = 12f;

        #endregion

        #region Events

        public event Action CountAmountChanged;
        public event Action MoveSpeedChanged;

        public event Action<int> CountDisplay;
        public event Action<int> MoneyChanged;
        public event Action<int> SalesChanged;
        public event Action<int> ObjectAmountChanged;

        #endregion

        #region HashKeys

        private const string MoneyKey = "money";
        private const string SalesValueKey = "salesValue";
        private const string ObjectAmountKey = "objectAmount";
        private const string MoveSpeedKey = "moveSpeed";

        #endregion


        public int Money
        {
            get => PlayerPrefs.GetInt(MoneyKey, 0);
            set
            {
                PlayerPrefs.SetInt(MoneyKey, value);
                SaveAndInvoke(MoneyChanged, value);
            }
        }

        public int SalesValue
        {
            get => PlayerPrefs.GetInt(SalesValueKey, 75);
            set
            {
                PlayerPrefs.SetInt(SalesValueKey, value);
                SaveAndInvoke(SalesChanged, value);
            }
        }

        public int ObjectAmount
        {
            get => PlayerPrefs.GetInt(ObjectAmountKey, 5);
            set
            {
                PlayerPrefs.SetInt(ObjectAmountKey, value);
                SaveAndInvoke(ObjectAmountChanged, value);
            }
        }

        public int ObjectCount
        {
            get => _objectCount;
            set
            {
                _objectCount = value;
                CountDisplay?.Invoke(_objectCount);
            }
        }

        public float MoveSpeed
        {
            get => PlayerPrefs.GetFloat(MoveSpeedKey, 7f);
            set
            {
                PlayerPrefs.SetFloat(MoveSpeedKey, value);
                PlayerPrefs.Save();
                _pathFollower.moveSpeed = value;
                SaveAndInvoke(MoveSpeedChanged, value);
            }
        }

        public void UpdateCountAmount()
        {
            int currentMoney = Money;

            if (currentMoney >= SalesValue && ObjectAmount < _objectMax)
            {
                currentMoney -= SalesValue;
                SalesValue *= 2;
                ObjectAmount++;
                Money = currentMoney;
                CountAmountChanged?.Invoke();
            }
        }

        public void UpdateSpeedValue()
        {
            int currentMoney = Money;
            
            if (currentMoney >= SalesValue && MoveSpeed < _maxSpeed)
            {
                currentMoney -= SalesValue;
                SalesValue *= 2;
                MoveSpeed++;
                Money = currentMoney;
                MoveSpeedChanged?.Invoke();
            }
        }

        private void SaveAndInvoke(Action<int> action, int value)
        {
            PlayerPrefs.Save();
            action?.Invoke(value);
        }

        private void SaveAndInvoke(Action action, float value)
        {
            PlayerPrefs.Save();
            action?.Invoke();
        }
    }
}