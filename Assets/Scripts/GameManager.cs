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
        public static GameManager instance;

        float _currentBalance;

        // Start is called before the first frame update
        void Start()
        {
            _currentBalance = 2;
            UIManager.instance.UpdateBalanceText();


        }

        // Update is called once per frame
        void Update()
        {


        }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        // Add amount to current balance.
        public void AddToBalance(float amount)
        {
            _currentBalance += amount;
            UIManager.instance.UpdateBalanceText();
        }

        // Checks if amount can be afforded by current balance.
        public bool CanBuy(float amount)
        {
            if(amount > _currentBalance)
                return false;
            else
                return true;
        }

        public float GetCurrentBalance()
        {
            return _currentBalance;
        }
    }
}

