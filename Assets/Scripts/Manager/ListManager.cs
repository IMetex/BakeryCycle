using System.Collections.Generic;
using Singleton;
using UnityEngine;

namespace Manager
{
    public class ListManager : Singleton<ListManager>
    {
        #region Public Lists
        
        public List<GameObject> doughList = new List<GameObject>();
        public List<GameObject> pizzaList = new List<GameObject>();
        public List<GameObject> standList = new List<GameObject>();
        public List<GameObject> customerList = new List<GameObject>();
        
        public List<GameObject> moneyList = new List<GameObject>();

        #endregion

        [Space]
        
        #region Public Booleans

        public bool isDoughInHand = false;
        public bool isBakeInHand = false;

        #endregion
        
        
    }
}