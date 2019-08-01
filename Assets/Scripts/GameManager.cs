// Zachary Hayes - zachary.j.hayes@gmail.com - 08/01/2019
// v.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class GameManager : MonoBehaviour
    {
        float _currentBalance;
        public Text currentBalanceText;

        // Start is called before the first frame update
        void Start()
        {
            _currentBalance = 2;
            currentBalanceText.text = _currentBalance.ToString("c2");


        }

        // Update is called once per frame
        void Update()
        {


        }

        // Add amount to current balance.
        public void AddToBalance(float amount)
        {
            _currentBalance += amount;
            currentBalanceText.text = _currentBalance.ToString("c2");
        }

        // Checks if amount can be afforded by current balance.
        public bool CanBuy(float amount)
        {
            if(amount > _currentBalance)
                return false;
            else
                return true;
        }
    }
}

