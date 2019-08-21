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
                    if(PlantInfo.Name == "PlantName")
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
                    else if (PlantInfo.Name == "BasePlantProfit")
                        plantObj.BasePlantProfit = float.Parse(PlantInfo.InnerText);
                    else if(PlantInfo.Name == "BasePlantCost")
                        plantObj.BasePlantCost = float.Parse(PlantInfo.InnerText);
                    else if(PlantInfo.Name == "PlantTimer")
                        plantObj.PlantTimer = float.Parse(PlantInfo.InnerText);
                }

                newPlant.transform.SetParent(plantPanel.transform);
            }
        }


    }
}