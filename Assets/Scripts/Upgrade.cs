using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public event System.Action JustBoughtAThingAndNowToEnterAStateOfDepression;

    public int cost = 10;
    public int cookiesPerClickIncrease = 1;

    public Text costText;
    public Text benefitText;

    private float scaling = 1.05f;

    Cookie cookie;

    private void Start()
    {
        cookie = FindObjectOfType<Cookie>();
        UpdateText();
    }

    private void OnMouseDown()
    {
        if (CookieHandler.cookies >= cost)
        {
            CookieHandler.cookies -= cost;
            cookie.cookiesPerClick += cookiesPerClickIncrease;
            int prevCost = cost;
            cost = (int)Mathf.Pow(cost, scaling);
            if (cost == prevCost)
                cost++;

            if (JustBoughtAThingAndNowToEnterAStateOfDepression != null)
                JustBoughtAThingAndNowToEnterAStateOfDepression.Invoke();

            UpdateText();
        }
    }

    private void UpdateText()
    {
        costText.text = "Cost " + cost;
        benefitText.text = "+" + cookiesPerClickIncrease;
    }
}
