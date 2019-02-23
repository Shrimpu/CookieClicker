using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public static event Action TotalClicks;
    public delegate void ClickEventDelegate(ulong cookiesPerClick);
    public static ClickEventDelegate ClickEvent;

    public string[] Inputs;
    private KeyCode kc;

    Animator animator;

    public static ulong cookiesPerClick = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        for (int i = 0; i < Inputs.Length; i++)
        {
            kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), Inputs[i]);
            if (Input.GetKeyDown(kc))
                Clicked();
        }
    }

    private void OnMouseDown()
    {
        Clicked();
    }

    private void Clicked()
    {
        animator.SetTrigger("Clicked");

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
