using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{
    public class LoadGameData : MonoBehaviour
    {
        public delegate void LoadDataComplete();
        public static event LoadDataComplete OnLoadDataComplete;

        [SerializeField]
        TextAsset gameData;
        [SerializeField]
        GameObject plantPrefab, plantPanel;

        void Start()
        {
            LoadData();

            if(OnLoadDataComplete != null)
            {
                OnLoadDataComplete();
            }
        }

        void LoadData()
        {
            XmlDocument xmlDoc = new XmlDocument();                             // Load plant data from XML doc.
            xmlDoc.LoadXml(gameData.text);

            LoadCompanyData(xmlDoc);
            LoadPlantData(xmlDoc);
            
        }

        void LoadCompanyData(XmlDocument _xmlDoc)
        {
            // Load Company Data
            XmlNodeList _startingBalanceNode = _xmlDoc.GetElementsByTagName("StartingBalance");
            float _startingBalance = float.Parse(_startingBalanceNode[0].InnerText);
            GameManager.instance.AddToBalance(_startingBalance);

            XmlNodeList _companyNameNode = _xmlDoc.GetElementsByTagName("CompanyName");
            string _companyName = _companyNameNode[0].InnerText;
            GameManager.instance.CompanyName = _companyName;
        }

        void LoadPlantData(XmlDocument _xmlDoc)
        {
            XmlNodeList plantList = _xmlDoc.GetElementsByTagName("Plant");

            foreach (XmlNode plantNode in plantList)
            {
                LoadPlantNodes(plantNode);
            }
        }

        void LoadPlantNodes(XmlNode _plantNode)
        {

            GameObject newPlant = (GameObject)Instantiate(plantPrefab);     // References Plant component from Plant prefab.
            Plant plantObj = newPlant.GetComponent<Plant>();

            XmlNodeList infoList = _plantNode.ChildNodes;

            foreach (XmlNode plantInfo in infoList)
            {
                SetPlantObj(plantObj, plantInfo, newPlant);
            }

            plantObj.NextGeneratorCost = plantObj.BaseGeneratorCost;        // Set cost of initial next generator to base cost.
            newPlant.transform.SetParent(plantPanel.transform);
        }
        
        void SetPlantObj(Plant _plantObj, XmlNode _plantInfo, GameObject _newPlant)
        {
                if (_plantInfo.Name == "Name")
                {
                    Text PlantText = _newPlant.transform.Find("PlantNameText").GetComponent<Text>();
                    PlantText.text = _plantInfo.InnerText;

                }
                else if (_plantInfo.Name == "DisplayImage")
                {
                    Sprite displayImageSprite = Resources.Load<Sprite>(_plantInfo.InnerText);
                    Image displayImage = _newPlant.transform.Find("DisplayImage").GetComponent<Image>();
                    displayImage.sprite = displayImageSprite;
                }
                else if (_plantInfo.Name == "BaseGeneratorProfit")
                    _plantObj.BaseGeneratorProfit = float.Parse(_plantInfo.InnerText);
                else if (_plantInfo.Name == "BaseGeneratorCost")
                    _plantObj.BaseGeneratorCost = float.Parse(_plantInfo.InnerText);
                else if (_plantInfo.Name == "Timer")
                    _plantObj.Timer = float.Parse(_plantInfo.InnerText);
                else if (_plantInfo.Name == "TimeDivisor")
                    _plantObj.TimeDivisor = float.Parse(_plantInfo.InnerText);
                else if (_plantInfo.Name == "PowerMultiplier")
                    _plantObj.PowerMultiplier = float.Parse(_plantInfo.InnerText);
                else if (_plantInfo.Name == "GeneratorCount")
                    _plantObj.GeneratorCount = int.Parse(_plantInfo.InnerText);
                else
                    Debug.Log("Unrecognized GameData.");
           
        }


    }
}