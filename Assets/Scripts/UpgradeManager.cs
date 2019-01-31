using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public event System.Action JustBoughtAThing;

    [System.Serializable]
    public class UpgradeData
    {
        [Tooltip("Requires two textobjects as children")]
        public Transform upgrade;
        public string upgradeName;
        public int startCost;
        [Tooltip("CPS = Cookies per Second. CPC = Cookies per Click")]
        public int CPSorCPCIncrease;

        [HideInInspector]
        public Text _name;
        [HideInInspector]
        public Text cost;
        [HideInInspector]
        public int updatedCost;
    }

    public UpgradeData[] upgradeData;
    public int minimumCostIncrease = 5;
    public float costScaling = 0.05f;

    //Cookie cookie;

    private void Start()
    {
        costScaling += 1f; // makes the scaling positive even if it is set below 1 in the inspector

        //cookie = FindObjectOfType<Cookie>();
        for (int i = 0; i < upgradeData.Length; i++)
        {
            upgradeData[i]._name = upgradeData[i].upgrade.transform.GetChild(0).GetComponent<Text>();
            upgradeData[i].cost = upgradeData[i].upgrade.transform.GetChild(1).GetComponent<Text>();
            upgradeData[i].updatedCost = upgradeData[i].startCost;

            if (upgradeData[i].upgradeName != "")
                upgradeData[i]._name.text = upgradeData[i].upgradeName;
            else
                upgradeData[i]._name.text = "Name Missing";

            upgradeData[i].cost.text = upgradeData[i].updatedCost.ToString();
        }
    }

    #region Upgrades

    public void BuyUpgrade0()
    {
        BuyUpgrade(0);
    }

    public void BuyUpgrade1()
    {
        BuyUpgrade(1);
    }

    public void BuyUpgrade2()
    {
        BuyUpgrade(2);
    }

    public void BuyUpgrade3()
    {
        BuyUpgrade(3);
    }

    public void BuyUpgrade4()
    {
        BuyUpgrade(4);
    }

    #endregion

    private void BuyUpgrade(int upgradeID)
    {
        if (upgradeData[upgradeID] != null)
        {
            if (CookieHandler.cookies >= upgradeData[upgradeID].updatedCost)
            {
                CookieHandler.cookies -= upgradeData[upgradeID].updatedCost;

                if ((int)(upgradeData[upgradeID].updatedCost * costScaling) <= upgradeData[upgradeID].updatedCost + minimumCostIncrease)
                    upgradeData[upgradeID].updatedCost += minimumCostIncrease;
                else
                    upgradeData[upgradeID].updatedCost = (int)(upgradeData[upgradeID].updatedCost * costScaling);

                upgradeData[upgradeID].cost.text = upgradeData[upgradeID].updatedCost.ToString();

                IdleCookies.cookiesPerSecond += upgradeData[upgradeID].CPSorCPCIncrease;

                if (JustBoughtAThing != null)
                    JustBoughtAThing.Invoke();
            }
        }
    }
}
