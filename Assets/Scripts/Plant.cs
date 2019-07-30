using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    int PlantCount;
    public Text PlantCountText;
    float BasePlantCost;

    //Game
    float CurrentBalance;
    public Text CurrentBalanceText;

    // Start is called before the first frame update.
    void Start()
    {
        PlantCount = 0;
        CurrentBalance = 2;
        BasePlantCost = 1.50f;
        CurrentBalanceText.text = CurrentBalance.ToString("c2");
    }

    // Update is called once per frame.
    void Update()
    {
        
    }

    public void BuyPlantOnClick()
    {
        if (BasePlantCost > CurrentBalance)
        {
            return; // Can't afford it.
        }

        PlantCount += 1;                                // Update PlantCount.
        PlantCountText.text = PlantCount.ToString();
        Debug.Log(PlantCount);

        CurrentBalance -= BasePlantCost;                // Update Balance.
        CurrentBalanceText.text = CurrentBalance.ToString("c2");
        Debug.Log(CurrentBalance);
    }
}
