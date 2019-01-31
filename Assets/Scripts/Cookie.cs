using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public event Action TotalClicks;
    public delegate void ClickEventDelegate(int cookiesPoesClick);
    public ClickEventDelegate ClickEvent;

    public int cookiesPerClick;

    #region MouseDetection

    private void OnMouseDown()
    {
        transform.localScale -= new Vector3(0.25f, 0.25f, 0);

        if (ClickEvent != null)
            ClickEvent(cookiesPerClick);
        if (TotalClicks != null)
            TotalClicks.Invoke();

        print("Got through");
    }

    private void OnMouseUp()
    {
        transform.localScale += new Vector3(0.25f, 0.25f, 0);
    }

    #endregion

    private void IncreaseCookiesPerClick(int amount)
    {
        cookiesPerClick += amount;
    }
}
