using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class Plant : MonoBehaviour
    {

        public Slider progressSlider;
        public Button buyButton;
        public Text buyButtonText;
        public Text plantCountText;

        [SerializeField]
        float _basePlantCost, _basePlantProfit, _plantTimer,
            _plantMultiplier = 1.0f, _timeDivisor = 2;
        [SerializeField]
        int _plantCount, _bonusDivisible = 20;
        [SerializeField]
        bool _managerUnlocked, _plantUnlocked;

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
            if(_plantUnlocked)
            {
                UpdateTimer();
                progressSlider.value = _currentTimer / _plantTimer;                 // Update slider.
            }
            else
            {
                CheckIfPlantLocked();
            }

            SetBuyButton();
        }

        public void BuyPlantOnClick()
        {
            if (GameManager.instance.CanBuy(_nextPlantCost))
            {
                _plantCount += 1;                                               // Update PlantCount.
                plantCountText.text = _plantCount.ToString();

                GameManager.instance.AddToBalance(-_nextPlantCost);                      // Subtract cost.

                _nextPlantCost = (_basePlantCost *
                    Mathf.Pow(_plantMultiplier,_plantCount));                   // Update next store cost.

                UpdateBuyButton();

                CheckAndApplyBuyBonus();
                
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
                    GameManager.instance.AddToBalance(_basePlantProfit * _plantCount);
                }
            }
        }

        // Enables or disables buy button based on current game state.
        private void SetBuyButton()
        {
            if (GameManager.instance.CanBuy(_nextPlantCost) && (_currentTimer <= 0f ||
                _managerUnlocked))
            {
                ToggleBuyButton(true);
            }
            else
            {
                ToggleBuyButton(false);
            }
        }

        // Shows plant when affordable.
        private void CheckIfPlantLocked()
        {
            CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();

            if (!_plantUnlocked && !GameManager.instance.CanBuy(_basePlantCost))
            {
                cg.interactable = false;
                cg.alpha = 0;
            }
            else // Show plant.
            {
                cg.interactable = true;
                cg.alpha = 1;
                _plantUnlocked = true;
            }
        }

        // Applies a speed bonus when number of plants owned is divisible by
        // the Time Divisor.
        private void CheckAndApplyBuyBonus()
        {
            if (_plantCount % _bonusDivisible == 0)
            {
                _plantTimer /= _timeDivisor;
            }
        }
    }

}