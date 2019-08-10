using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class Plant : MonoBehaviour
    {

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

        public int PlantCount
        {
            get { return _plantCount; }
        }
        public float CurrentTimer
        {
            get { return _currentTimer; }
        }
        public float PlantTimer
        {
            get { return _plantTimer; }
        }
        public float BasePlantCost
        {
            get { return _basePlantCost; }
        }
        public float NextPlantCost
        {
            get { return _nextPlantCost; }
        }
        public bool PlantUnlocked
        {
            get { return _plantUnlocked; }
            set { _plantUnlocked = value; }
        }
        public bool ManagerUnlocked
        {
            get { return _managerUnlocked; }
        }


        // Start is called before the first frame update.
        void Start()
        {
            plantCountText.text = _plantCount.ToString();
            _nextPlantCost = _basePlantCost;
        }

        // Update is called once per frame.
        void Update()
        {
            if(_plantUnlocked)
            {
                UpdateTimer();
            }
            else
            {
                //CheckIfPlantLocked();
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
            _plantCount += 1;                                               // Update PlantCount.

            GameManager.instance.AddToBalance(-_nextPlantCost);             // Subtract cost.

            _nextPlantCost = (_basePlantCost *
                Mathf.Pow(_plantMultiplier, _plantCount));                   // Update next store cost.

            CheckAndApplyBuyBonus();

        }

        public void PowerPlant()
        {
            if (!_startTimer && _plantCount > 0)
            {
                _startTimer = true;
            }
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
                    GameManager.instance
                        .AddToBalance(_basePlantProfit * _plantCount);
                }
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