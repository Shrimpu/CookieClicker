using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public event Action TotalClicks;
    public delegate void ClickEventDelegate(ulong cookiesPerClick);
    public ClickEventDelegate ClickEvent;

    public static ulong cookiesPerClick = 1;

    private void OnMouseDown()
    {
        Clicked();
    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Clicked()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 1f);

        if (ClickEvent != null)
        {
            ClickEvent(cookiesPerClick);
        }

        if (TotalClicks != null)
        {
            TotalClicks.Invoke();
        }
    }
}
