using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
 public class LoadGameData : MonoBehaviour
    {
        [SerializeField]
        TextAsset gameData;
        [SerializeField]
        GameObject plantPrefab, plantPanel;

        void Start()
        {
            LoadData();
        }

        void LoadData()
        {
            XmlDocument xmlDoc = new XmlDocument();                             // Load plant data from XML doc.
            xmlDoc.LoadXml(gameData.text);
            XmlNodeList PlantList = xmlDoc.GetElementsByTagName("Plant");

            foreach (XmlNode PlantNode in PlantList)
            {

                GameObject newPlant = (GameObject)Instantiate(plantPrefab);     // References Plant component from Plant prefab.
                Plant plantObj = newPlant.GetComponent<Plant>();

                XmlNodeList InfoList = PlantNode.ChildNodes;

                foreach (XmlNode PlantInfo in InfoList)
                {
                    if (PlantInfo.Name == "Name")
                    {
                        Text PlantText = newPlant.transform.Find("PlantNameText").GetComponent<Text>();
                        PlantText.text = PlantInfo.InnerText;

                    }
                    else if (PlantInfo.Name == "DisplayImage")
                    {
                        Sprite displayImageSprite = Resources.Load<Sprite>(PlantInfo.InnerText);
                        Image displayImage = newPlant.transform.Find("DisplayImage").GetComponent<Image>();
                        displayImage.sprite = displayImageSprite;
                    }
                    else if (PlantInfo.Name == "BaseGeneratorProfit")
                        plantObj.BaseGeneratorProfit = float.Parse(PlantInfo.InnerText);
                    else if (PlantInfo.Name == "BaseGeneratorCost")
                        plantObj.BaseGeneratorCost = float.Parse(PlantInfo.InnerText);
                    else if (PlantInfo.Name == "Timer")
                        plantObj.Timer = float.Parse(PlantInfo.InnerText);
                    else if (PlantInfo.Name == "TimeDivisor")
                        plantObj.TimeDivisor = float.Parse(PlantInfo.InnerText);
                    else if (PlantInfo.Name == "PowerMultiplier")
                        plantObj.PowerMultiplier = float.Parse(PlantInfo.InnerText);
                    else if (PlantInfo.Name == "GeneratorCount")
                        plantObj.GeneratorCount = int.Parse(PlantInfo.InnerText);
                    else
                        Debug.Log("Unrecognized GameData.");
                }

                newPlant.transform.SetParent(plantPanel.transform);
            }
        }


    }
}