using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class UIPlant : MonoBehaviour
    {
        [SerializeField]
        Slider progressSlider;
        [SerializeField]
        Button buyButton;
        [SerializeField]
        Text buyButtonText, generatorCountText;

        public Plant plant;

        void Awake()
        {
            plant = transform.GetComponent<Plant>();
        }

        void Start()
        {
            //UpdateUI();
            UpdatePlantCountText();
        }

        void Update()
        {
            progressSlider.value = plant.CurrentTimer / plant.Timer;       // Update slider.
        }

        void OnEnable()
        {
            GameManager.OnUpdateBalance += UpdateUI;
            LoadGameData.OnLoadDataComplete += UpdateUI;
        }

        void OnDisable()
        {
            GameManager.OnUpdateBalance -= UpdateUI;
            LoadGameData.OnLoadDataComplete -= UpdateUI;
        }

        private void UpdateUI()
        {
            SetPlantAvailability();
            SetBuyButtonInteractability();
            UpdateBuyButtonText();
        }

        // Unlock plant when affordable.
        private void SetPlantAvailability()
        {
            CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();

            if (!plant.PlantUnlocked &&
                !GameManager.instance.CanBuy(plant.BaseGeneratorCost))
            {
                cg.interactable = false;
                cg.alpha = 0;
            }
            else // Show plant.
            {
                cg.interactable = true;
                cg.alpha = 1;
                plant.PlantUnlocked = true;
            }
        }

        // Enables or disables buy button based on current game state.
        private void SetBuyButtonInteractability()
        {
            if (GameManager.instance.CanBuy(plant.NextGeneratorCost) &&
                (plant.CurrentTimer <= 0f || plant.ManagerUnlocked))
            {
                ToggleBuyButton(true);
            }
            else
            {
                ToggleBuyButton(false);
            }
        }

        // Show or hide buy button depending on bool.
        private void ToggleBuyButton(bool showButton)
        {
            buyButton.interactable = showButton;
        }

        // Update text on buy button to reflect cost of next plant.
        private void UpdateBuyButtonText()
        {
            buyButtonText.text = "Buy " + plant.NextGeneratorCost.ToString("c2");
        }

        // Update number of plants owned in UI.
        private void UpdatePlantCountText()
        {
            generatorCountText.text = plant.GeneratorCount.ToString();
        }

        public void BuyPlantOnClick()
        {
            plant.BuyPlantIfAffordable();
            UpdatePlantCountText();
        }

        public void PowerOnClick()
        {
            plant.PowerOn();
        }
    }
}
