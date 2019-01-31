using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    bool isSpinning;

    void Start()
    {
        SpaceClicker spico = GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceClicker>();
        spico.LektionStart += () => { isSpinning = true; }; spico.LektionSlut += () => { isSpinning = false; };
    }

    private void Update()
    {
        if (isSpinning)
            transform.Rotate(0, 0, 180f * Time.deltaTime);
    }
}
