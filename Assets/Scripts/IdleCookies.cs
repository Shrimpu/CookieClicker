using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCookies : MonoBehaviour
{
    public event System.Action IdleCookieGained;

    public static int cookiesPerSecond = 0;

    void Start()
    {
        StartCoroutine(GiveIdleCookies());
    }

    IEnumerator GiveIdleCookies()
    {
        float cookiesOnHold = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            cookiesOnHold = cookiesOnHold + (cookiesPerSecond * 0.1f);

            if (cookiesOnHold >= 1f)
            {
                CookieHandler.cookies += (int)cookiesOnHold;
                cookiesOnHold = cookiesOnHold % 1f;

                if (IdleCookieGained != null)
                    IdleCookieGained.Invoke();
            }
        }
    }
}
