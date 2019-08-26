// Zachary Hayes - zachary.j.hayes@gmail.com - 08/01/2019
// v. 0.1.1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class GameManager : MonoBehaviour
    {
        public delegate void UpdateBalance();
        public static event UpdateBalance OnUpdateBalance;

        public static GameManager instance;

        private const float INITIAL_BALANCE = 0.00f; // Set in GameData.xml

        string _companyName;
        float _currentBalance;

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        public float CurrentBalance
        {
            get { return _currentBalance; }
        }

        // Called before Start.
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            FireOnUpdateBalanceEvent();
        }

        // Add amount to current balance.
        public void AddToBalance(float amount)
        {
            _currentBalance += amount;
            FireOnUpdateBalanceEvent();
        }

        // Checks if amount can be afforded by current balance.
        public bool CanBuy(float amount)
        {
            if(amount > _currentBalance)
                return false;
            else
                return true;
        }

        private void FireOnUpdateBalanceEvent()
        {
            if (OnUpdateBalance != null)
            {
                OnUpdateBalance();
            }
        }
    }
}

