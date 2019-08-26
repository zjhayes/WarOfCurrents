using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        Text currentBalanceText, companyNameText;

        void Start()
        {
            UpdateBalanceText(); // Set initial balance.
        }

        void OnEnable()
        {
            GameManager.OnUpdateBalance += UpdateBalanceText;
            LoadGameData.OnLoadDataComplete += UpdateUI;
        }

        void OnDisable()
        {
            GameManager.OnUpdateBalance -= UpdateBalanceText;
            LoadGameData.OnLoadDataComplete -= UpdateUI;
        }

        public void UpdateBalanceText()
        {
            currentBalanceText.text = GameManager.instance
                .CurrentBalance.ToString("c2");
        }

        public void UpdateCompanyNameText()
        {
            companyNameText.text = GameManager.instance.CompanyName;
        }

        public void UpdateUI()
        {
            UpdateBalanceText();
            UpdateCompanyNameText();
        }
    }
}
