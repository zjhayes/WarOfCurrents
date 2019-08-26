using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class Plant : MonoBehaviour
    {
        [SerializeField]
        float baseGeneratorCost, baseGeneratorProfit, timer,
            powerMultiplier, timeDivisor;
        [SerializeField]
        int generatorCount, bonusDivisible;
        [SerializeField]
        bool managerUnlocked, plantUnlocked;

        float _currentTimer;
        bool _startTimer;
        float _nextGeneratorCost;

        public int GeneratorCount
        {
            get { return generatorCount; }
            set { generatorCount = value; }
        }
        public float CurrentTimer
        {
            get { return _currentTimer; }
        }
        public float Timer
        {
            get { return timer; }
            set { timer = value; }
        }
        public float BaseGeneratorCost
        {
            get { return baseGeneratorCost; }
            set { baseGeneratorCost = value; }
        }
        public float BaseGeneratorProfit
        {
            get { return baseGeneratorProfit; }
            set { baseGeneratorProfit = value; }
        }
        public float NextGeneratorCost
        {
            get { return _nextGeneratorCost; }
            set { _nextGeneratorCost = value; }
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
        public float TimeDivisor
        {
            get { return timeDivisor; }
            set { timeDivisor = value; }
        }
        public float PowerMultiplier
        {
            get { return powerMultiplier; }
            set { powerMultiplier = value; }
        }


        // Start is called before the first frame update.
        void Start()
        {
            //_nextGeneratorCost = baseGeneratorCost;
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
            if (GameManager.instance.CanBuy(_nextGeneratorCost))
            {
                BuyPlant();
            } // else can't afford it.
        }

        private void BuyPlant()
        {
            generatorCount += 1;                                                   // Update PlantCount.

            float _invPlantCost = -_nextGeneratorCost;                              // Hold inverse of plant cost.

            _nextGeneratorCost = baseGeneratorCost *
                Mathf.Pow(powerMultiplier, generatorCount);                       // Update next store cost.

            GameManager.instance.AddToBalance(_invPlantCost);                   // Subtract cost.

            CheckAndApplyBuyBonus();
        }

        public void PowerOn()
        {
            if (!_startTimer && generatorCount > 0)
            {
                _startTimer = true;
            }
        }

        private void UpdateTimer()
        {
            if (_startTimer)
            {
                _currentTimer += Time.deltaTime;

                if (_currentTimer > timer)
                {
                    if (!managerUnlocked)
                    {
                        _startTimer = false;
                    }
                    _currentTimer = 0f;
                    GameManager.instance
                        .AddToBalance(baseGeneratorProfit * generatorCount);
                }
            }
        }


        // Applies a speed bonus when number of plants owned is divisible by
        // the Time Divisor.
        private void CheckAndApplyBuyBonus()
        {
            if (generatorCount % bonusDivisible == 0)
            {
                timer /= timeDivisor;
            }
        }
    }

}