using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text cookieText;
    public Text CookiesPerClickText;
    public Text[] achievementText = new Text[2];

    Cookie cookieScript;

    private void Start()
    {
        FindObjectOfType<Achievements>().OnAchivementGet += AchievementTextDisplay;
        cookieScript = FindObjectOfType<Cookie>();
        cookieScript.TotalClicks += DisplayScoreText;
        UpgradeManager upgradeManager = FindObjectOfType<UpgradeManager>();

        upgradeManager.JustBoughtAThing += DisplayScoreText;
        upgradeManager.JustBoughtAThing += UpdateIdleCookies;

        IdleCookies idleCookies = FindObjectOfType<IdleCookies>();

        idleCookies.IdleCookieGained += DisplayScoreText;

        UpdateIdleCookies();
    }

    private void DisplayScoreText()
    {
        cookieText.text = CookieHandler.cookies.ToString();
    }

    private void AchievementTextDisplay(string _name, string description)
    {
        achievementText[0].text = _name;
        achievementText[1].text = description;
    }

    private void UpdateIdleCookies()
    {
        CookiesPerClickText.text = IdleCookies.cookiesPerSecond.ToString();
    }
}
