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

    private void OnMouseDown()
    {
        Clicked();
    }

    private void OnMouseUp()
    {
        transform.localScale += new Vector3(0.25f, 0.25f, 0);
    }

    private void Clicked()
    {
        transform.localScale -= new Vector3(0.25f, 0.25f, 0); // create animation

        if (ClickEvent != null)
            ClickEvent(cookiesPerClick);
        if (TotalClicks != null)
            TotalClicks.Invoke();
    }

    private void IncreaseCookiesPerClick(int amount)
    {
        cookiesPerClick += amount;
    }
}
