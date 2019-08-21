using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class Plant : MonoBehaviour
    {
        [SerializeField]
        Text plantCountText;
        [SerializeField]
        float basePlantCost, basePlantProfit, plantTimer,
            plantMultiplier = 1.0f, timeDivisor = 2;
        [SerializeField]
        int plantCount, bonusDivisible = 20;
        [SerializeField]
        bool managerUnlocked, plantUnlocked;

        float _currentTimer;
        bool _startTimer;
        float _nextPlantCost;

        public int PlantCount
        {
            get { return plantCount; }
        }
        public float CurrentTimer
        {
            get { return _currentTimer; }
        }
        public float PlantTimer
        {
            get { return plantTimer; }
            set { plantTimer = value; }
        }
        public float BasePlantCost
        {
            get { return basePlantCost; }
            set { basePlantCost = value; }
        }
        public float BasePlantProfit
        {
            get { return basePlantProfit; }
            set { basePlantProfit = value; }
        }
        public float NextPlantCost
        {
            get { return _nextPlantCost; }
        }
        public bool PlantUnlocked
        {
            get { return plantUnlocked; }
            set { plantUnlocked = value; }
        }
        public bool ManagerUnlocked
        {
            get { return managerUnlocked; }
        }


        // Start is called before the first frame update.
        void Start()
        {
            plantCountText.text = plantCount.ToString();
            _nextPlantCost = basePlantCost;
        }

        // Update is called once per frame.
        void Update()
        {
            if(plantUnlocked)
            {
                UpdateTimer();
            }
        }

        public void BuyPlantIfAffordable()
        {
            if (GameManager.instance.CanBuy(_nextPlantCost))
            {
                BuyPlant();
            } // else can't afford it.
        }

        private void BuyPlant()
        {
            plantCount += 1;                                                   // Update PlantCount.

            float _invPlantCost = -_nextPlantCost;                              // Hold inverse of plant cost.

            _nextPlantCost = basePlantCost *
                Mathf.Pow(plantMultiplier, plantCount);                       // Update next store cost.

            GameManager.instance.AddToBalance(_invPlantCost);                   // Subtract cost.

            CheckAndApplyBuyBonus();
        }

        public void PowerOn()
        {
            if (!_startTimer && plantCount > 0)
            {
                _startTimer = true;
            }
        }

        private void UpdateTimer()
        {
            if (_startTimer)
            {
                _currentTimer += Time.deltaTime;

                if (_currentTimer > plantTimer)
                {
                    if (!managerUnlocked)
                    {
                        _startTimer = false;
                    }
                    _currentTimer = 0f;
                    GameManager.instance
                        .AddToBalance(basePlantProfit * plantCount);
                }
            }
        }


        // Applies a speed bonus when number of plants owned is divisible by
        // the Time Divisor.
        private void CheckAndApplyBuyBonus()
        {
            if (plantCount % bonusDivisible == 0)
            {
                plantTimer /= timeDivisor;
            }
        }
    }

}