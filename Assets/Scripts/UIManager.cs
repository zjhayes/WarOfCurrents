using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarOfCurrents
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public Text currentBalanceText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void UpdateBalanceText()
        {
            currentBalanceText.text = GameManager.instance.GetCurrentBalance().ToString("c2");
        }
    }
}
