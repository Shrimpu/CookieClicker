using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeBuyIdea : MonoBehaviour
{
    public enum BuyAmounts { buy1, buy10, buy100 };
    BuyAmounts buyAmount = new BuyAmounts();

    // public Upgrade[] upgrades

    public void ChangeBuyAmount(BuyAmounts i) // basically an int
    {
        buyAmount = i;
    }

    public void Buy(int upgrade)
    {
        int upgradesToPurchase = (int)Mathf.Pow(10, (int)buyAmount); // 10^0 = 1, 10^1 = 10, 10^2 = 100.
        for (int i = 0; i < upgradesToPurchase; i++)
        {
            // purchase upgrade[i]
        }
    }
}
