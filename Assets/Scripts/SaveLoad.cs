using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoad : MonoBehaviour
{
    [HideInInspector]
    static string path;
    public bool autoLoad = true;

    SaveData saveData = new SaveData();

    void Awake()
    {
        saveData.achievements = FindObjectOfType<Achievements>();
        saveData.cookieHandler = FindObjectOfType<CookieHandler>();
        saveData.upgradeManager = FindObjectOfType<UpgradeManager>();

        if (autoLoad)
            Save();
    }


    public void Save()
    {
        saveData = SaveValues();

        path = Application.persistentDataPath + "/big.stegosaurus";

        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string data;
                    data = JsonUtility.ToJson(saveData);
                }
            }
        }
        catch (System.Exception) { throw; }
    }

    public void Load()
    {
        path = Application.persistentDataPath + "/big.stegosaurus";

        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                string data = sr.ReadToEnd();
                JsonUtility.FromJsonOverwrite(data, saveData);
                LoadValues(saveData);
            }
        }
    }

    private SaveData SaveValues()
    {
        SaveData data = new SaveData
        {
            cookiesOwned = CookieHandler.cookies,
            totalCookiesBaked = CookieHandler.totalCookies,

            clickTotal = Achievements.clickTotal,
            upgradeAmounts = saveData.SetUpgradeAmounts(saveData.upgradeManager.upgradeData.Length),

            achievements = saveData.achievements,
            cookieHandler = saveData.cookieHandler,
            upgradeManager = saveData.upgradeManager
        };
        return data;
    }

    private void LoadValues(SaveData data)
    {
        saveData.achievements = FindObjectOfType<Achievements>(); // reassigns these because i cant bother troubleshooting and if it breakes then so be it. me tired
        saveData.cookieHandler = FindObjectOfType<CookieHandler>();
        saveData.upgradeManager = FindObjectOfType<UpgradeManager>();

        CookieHandler.cookies = data.cookiesOwned;
        CookieHandler.totalCookies = data.totalCookiesBaked;

        IdleCookies.ChangeCpsMult(0);
        IdleCookies.cookieBoost = 1;

        Achievements.clickTotal = data.clickTotal;
        Achievements.upgradesBought = 0;
        FindObjectOfType<Achievements>().ResetAchievements();
        ulong[] upgradeIDs = new ulong[saveData.upgradeManager.upgradeData.Length];
        for (ulong i = 0; i < (ulong)upgradeIDs.Length; i++)
        {
            upgradeIDs[i] = i;
        }
        saveData.upgradeAmounts = data.upgradeAmounts;
        saveData.upgradeManager.buyUpgradesOnLoad(upgradeIDs, data.upgradeAmounts); // re-purchases all upgrades
    }

    class SaveData
    {
        public ulong cookiesOwned;
        public ulong totalCookiesBaked;
        public ulong clickTotal;
        public ulong[] upgradeAmounts;

        public Achievements achievements;
        public CookieHandler cookieHandler;
        public UpgradeManager upgradeManager;

        public ulong[] SetUpgradeAmounts(int arrayLength)
        {
            ulong[] arr = new ulong[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                arr[i] = upgradeManager.upgradeData[i].upgradesOfTypeBought;
            }
            return arr;
        }
    }
}
