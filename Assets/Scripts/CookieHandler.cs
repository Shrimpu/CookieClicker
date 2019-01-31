using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieHandler : MonoBehaviour
{
    public static int cookies;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Cookie").GetComponent<Cookie>().ClickEvent += AddCookies;
    }

    void AddCookies(int cookiesToAdd)
    {
        cookies += cookiesToAdd;
    }
}
