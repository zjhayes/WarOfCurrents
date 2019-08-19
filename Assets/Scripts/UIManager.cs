using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        Text currentBalanceText;

        void Start()
        {
            UpdateBalanceText(); // Set initial balance.
        }

        void OnEnable()
        {
            GameManager.OnUpdateBalance += UpdateBalanceText;
        }

        void OnDisable()
        {
            GameManager.OnUpdateBalance -= UpdateBalanceText;
        }

        public void UpdateBalanceText()
        {
            currentBalanceText.text = GameManager.instance
                .CurrentBalance.ToString("c2");
        }
    }
}
