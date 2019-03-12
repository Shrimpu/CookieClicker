using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static event System.Action JustBoughtAThing;

    public GameObject autoClicker;
    public Transform autoClickerHolder;

    [System.Serializable]
    public class UpgradeData
    {
        public UpgradeEffect effect;
        [Tooltip("Requires two textobjects as children")]
        public Transform upgrade;
        public string upgradeName;
        public ulong startCost;
        [Tooltip("CPS = Cookies per Second. CPC = Cookies per Click")]
        public ulong increase;
        [HideInInspector]
        public ulong upgradesOfTypeBought;

        [HideInInspector]
        public Text _name;
        [HideInInspector]
        public Text cost;
        [HideInInspector]
        public Text amount;
        [HideInInspector]
        public ulong updatedCost;
    }

    public UpgradeData[] upgradeData;
    public ulong minimumCostIncrease = 5;
    public float costScaling = 0.05f;

    // bools for buying more
    public bool buy10;
    public bool buy100;

    // Autoclicker fields
    [Range(1, 100)]
    public float autoClickersPerLevel = 20;
    [Range(1, 10)]
    public float activeClickersPerLevel = 3f;
    private ulong autoClickerCounter = 0;
    public float radius = 2.15f;
    public float radiusIncrease = 0.3f;
    private float currentRadius;
    private float currentAngle = 0;
    private float autoClickerOffsetAmount;
    private float autoClickerOffset = 0f;
    private float autoClickerClickOffset;

    //Cookie cookie;

    private void Start()
    {
        costScaling += 1f; // makes the scaling positive even if it is set below 1 in the inspector
        currentRadius = radius;
        autoClickerOffsetAmount = (360 / autoClickersPerLevel) / 2;

        //cookie = FindObjectOfType<Cookie>();
        for (int i = 0; i < upgradeData.Length; i++)
        {
            upgradeData[i]._name = upgradeData[i].upgrade.transform.GetChild(0).GetComponent<Text>();
            upgradeData[i].cost = upgradeData[i].upgrade.transform.GetChild(1).GetComponent<Text>();
            upgradeData[i].amount = upgradeData[i].upgrade.transform.GetChild(2).GetComponent<Text>();
            upgradeData[i].updatedCost = upgradeData[i].startCost;

            if (upgradeData[i].upgradeName != "")
                upgradeData[i]._name.text = upgradeData[i].upgradeName;
            else
                upgradeData[i]._name.text = "Name Missing";

            upgradeData[i].cost.text = upgradeData[i].updatedCost.ToString();
        }
    }

    // this doesn't really belong in this script
    #region changing buyAmount variables

    public void DisableMultiBuy()
    {
        buy10 = false;
        buy100 = false;
    }

    public void EnableBuy10()
    {
        buy10 = true;
        buy100 = false;
    }

    public void EnableBuy100()
    {
        buy10 = false;
        buy100 = true;
    }

    #endregion  

    public void UpgradeButton(int index)
    {
        if (index < upgradeData.Length)
        {

            bool[] afforded = new bool[100];
            afforded = BuyUpgrades((ulong)index);
            if (index == 0)
            {
                for (int i = 0; i < afforded.Length; i++)
                {
                    if (afforded[i])
                    {
                        SpawnAutoClicker();
                    }
                    else
                        break;
                }
            }
        }
        else print("index to large, button doesn't exists");
    }

    private bool BuyUpgrade(ulong upgradeID, bool free = false)
    {
        if (upgradeData[upgradeID] != null)
        {
            if (CookieHandler.cookies >= upgradeData[upgradeID].updatedCost || free)
            {
                if (!free)
                    CookieHandler.cookies -= upgradeData[upgradeID].updatedCost;

                if ((ulong)(upgradeData[upgradeID].updatedCost * costScaling) <= upgradeData[upgradeID].updatedCost + minimumCostIncrease)
                    upgradeData[upgradeID].updatedCost += minimumCostIncrease;
                else
                    upgradeData[upgradeID].updatedCost = (ulong)(upgradeData[upgradeID].updatedCost * costScaling);

                upgradeData[upgradeID].cost.text = upgradeData[upgradeID].updatedCost.ToString();

                upgradeData[upgradeID].effect.Do(upgradeData[upgradeID].increase);

                if (!free)
                    if (JustBoughtAThing != null)
                        JustBoughtAThing.Invoke();

                upgradeData[upgradeID].upgradesOfTypeBought++;
                upgradeData[upgradeID].amount.text = upgradeData[upgradeID].upgradesOfTypeBought.ToString();

                return true;
            }
        }
        return false;
    }

    public bool[] BuyUpgrades(ulong functionID)
    {
        bool[] afforded = new bool[100];

        if (buy10)
        {
            for (ulong i = 0; i < 10; i++)
            {
                afforded[i] = BuyUpgrade(functionID);
            }
        }
        else if (buy100)
        {
            for (ulong i = 0; i < 100; i++)
            {
                afforded[i] = BuyUpgrade(functionID);
            }
        }
        else
        {
            afforded[0] = BuyUpgrade(functionID);
        }

        return afforded;
    }

    public void buyUpgradesOnLoad(ulong[] upgradeID, ulong[] amount)
    {
        ResetAll();

        for (int i = 0; i < upgradeID.Length; i++)
        {
            if (amount != null)
            {
                for (int j = 0; j < (int)amount[i]; j++)
                {
                    BuyUpgrade(upgradeID[i], true);
                    if (i == 0)
                        SpawnAutoClicker();
                }
            }
            else break;
        }
    }

    float retardRatio;
    public void SpawnAutoClicker()
    {
        GameObject spawnedClicker = Instantiate(autoClicker,
            autoClickerHolder.position - new Vector3(Mathf.Cos((currentAngle + 90f) * Mathf.Deg2Rad),
            Mathf.Sin((currentAngle + 90f) * Mathf.Deg2Rad)) * currentRadius, Quaternion.Euler(0, 0, currentAngle));
        currentAngle += 360 / autoClickersPerLevel;

        spawnedClicker.transform.SetParent(autoClickerHolder);
        autoClickerClickOffset = (autoClickerCounter / autoClickersPerLevel) * (2 * activeClickersPerLevel * Mathf.PI); // multipling with PI because Unity uses radians and the amount is pretty much active clickers * 2
        spawnedClicker.GetComponent<ClickerBehaviour>().offset = autoClickerClickOffset;

        autoClickerCounter++;
        if (autoClickerCounter >= autoClickersPerLevel)
        {
            autoClickerCounter = 0;
            autoClickerOffset += autoClickerOffsetAmount;
            currentRadius += radiusIncrease;
            retardRatio += 1.618f;
            currentAngle = 0f;
            currentAngle += retardRatio;
        }
    }

    private void ResetAll()
    {
        ClickerBehaviour[] clickers = FindObjectsOfType<ClickerBehaviour>();
        for (int i = 0; i < clickers.Length; i++)
        {
            Destroy(clickers[i].gameObject);
        }

        for (int i = 0; i < upgradeData.Length; i++)
        {
            upgradeData[i].updatedCost = upgradeData[i].startCost;

            autoClickerOffset = 0f;
            autoClickerCounter = 0;
            currentAngle = 0f;
            currentRadius = radius;

            upgradeData[i].upgradesOfTypeBought = 0;

            if (JustBoughtAThing != null)
                JustBoughtAThing.Invoke();
        }
    }
}