using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text cookieText;
    public Text CookiesPerClickText;
    public Text[] achievementText = new Text[2];
    public float fadeInIime = 0.3f;
    public float fadeOutTime = 0.5f;
    public float screenTime = 1.2f;

    bool displayingAchievement;
    List<achievementInfo> achievementQueue = new List<achievementInfo>();
    Cookie cookieScript;

    private void Start()
    {
        FindObjectOfType<Achievements>().OnAchivementGet += AchievementTextDisplay;
        Cookie.TotalClicks += DisplayScoreText;

        UpgradeManager.JustBoughtAThing += DisplayScoreText;

        IdleCookies.IdleCookieGained += DisplayScoreText;
        IdleCookies.CpsChanged += UpdateIdleCookies;

        UpdateIdleCookies();
    }

    private void DisplayScoreText()
    {
        cookieText.text = CookieHandler.cookies.ToString();
    }

    private void AchievementTextDisplay(string text1, string text2)
    {
        achievementQueue.Add(new achievementInfo()
        {
            Name = text1,
            Description = text2
        });

        if (!displayingAchievement)
            StartCoroutine(DisplayAchievement());
    }

    IEnumerator DisplayAchievement()
    {
        displayingAchievement = true;
        float a = 0f;

        achievementText[0].text = achievementQueue[0].Name;
        achievementText[1].text = achievementQueue[0].Description;

        while (a < 1f)
        {
            a += Time.deltaTime / fadeInIime;
            achievementText[0].color = new Color(achievementText[0].color.r, achievementText[0].color.g, achievementText[0].color.b, a > 0 ? a : 0);
            achievementText[1].color = new Color(achievementText[1].color.r, achievementText[1].color.g, achievementText[1].color.b, a > 0 ? a : 0);
            yield return null;
        }
        yield return new WaitForSeconds(screenTime);
        while (a > 0f)
        {
            a -= Time.deltaTime / fadeOutTime;
            achievementText[0].color = new Color(achievementText[0].color.r, achievementText[0].color.g, achievementText[0].color.b, a < 1 ? a : 1);
            achievementText[1].color = new Color(achievementText[1].color.r, achievementText[1].color.g, achievementText[1].color.b, a < 1 ? a : 1);
            yield return null;
        }

        achievementQueue.RemoveAt(0);
        if (achievementQueue.Count > 0)
            StartCoroutine(DisplayAchievement());
        else
            displayingAchievement = false;
    }

    private void UpdateIdleCookies()
    {
        CookiesPerClickText.text = IdleCookies.cookiesPerSecond.ToString();
    }

    class achievementInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
