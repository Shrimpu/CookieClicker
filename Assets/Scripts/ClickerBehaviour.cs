using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerBehaviour : MonoBehaviour
{
    [HideInInspector]
    public float offset;
    public float intensity = 2.5f;

    private Vector3 startPos;

    float Curve
    {
        get
        {
            float value = Mathf.Sin(Time.time + offset);
            if (value < 0.995f)
                value = 0f;
            else
                value = 1f;
            return value;
        }
        set { Curve = value; }
    }

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = transform.up * Curve * intensity + startPos;
    }
}
