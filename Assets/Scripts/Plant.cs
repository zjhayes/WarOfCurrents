using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class Plant : MonoBehaviour
    {
        public GameManager gameManager;
        public Slider progressSlider;
        public Button buyButton;
        public Text buyButtonText;
        public Text plantCountText;

        [SerializeField]
        float _basePlantCost, _basePlantProfit, _plantTimer,
            _storeMultiplier = 1.0f;
        [SerializeField]
        int _plantCount;
        [SerializeField]
        bool _managerUnlocked;

        float _currentTimer;
        bool _startTimer;
        float _nextPlantCost;


        // Start is called before the first frame update.
        void Start()
        {
            plantCountText.text = _plantCount.ToString();
            _nextPlantCost = _basePlantCost;
            UpdateBuyButton();
        }

        // Update is called once per frame.
        void Update()
        {
            UpdateTimer();
            progressSlider.value = _currentTimer / _plantTimer;                 // Update slider.
            SetBuyButton();
        }

        public void BuyPlantOnClick()
        {
            if (gameManager.CanBuy(_nextPlantCost))
            {
                _plantCount += 1;                                               // Update PlantCount.
                plantCountText.text = _plantCount.ToString();

                gameManager.AddToBalance(-_nextPlantCost);                      // Subtract cost.

                _nextPlantCost = (_basePlantCost *
                    Mathf.Pow(_storeMultiplier,_plantCount));                   // Update next store cost.

                UpdateBuyButton();
            } // else can't afford it.
        }

        public void PowerOnClick()
        {
            if (!_startTimer && _plantCount > 0)
            {
                _startTimer = true;
                ToggleBuyButton(false);
            }
        }

        // Update text on buy button to reflect cost of next plant.
        private void UpdateBuyButton()
        {
            buyButtonText.text = "Buy " + _nextPlantCost.ToString("c2");
        }

        // Show or hide buy button depending on bool.
        private void ToggleBuyButton(bool showButton)
        {
            buyButton.interactable = showButton;
        }

        private void UpdateTimer()
        {
            if (_startTimer)
            {
                _currentTimer += Time.deltaTime;

                if (_currentTimer > _plantTimer)
                {
                    if (!_managerUnlocked)
                    {
                        _startTimer = false;
                    }
                    _currentTimer = 0f;
                    gameManager.AddToBalance(_basePlantProfit * _plantCount);
                }
            }
        }

        // Enables or disables buy button based on current game state.
        private void SetBuyButton()
        {
            if (gameManager.CanBuy(_nextPlantCost) && _currentTimer <= 0f)
            {
                ToggleBuyButton(true);
            }
            else
            {
                ToggleBuyButton(false);
            }
        }
    }

}