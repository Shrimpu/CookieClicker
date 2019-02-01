using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerBehaviour : MonoBehaviour
{
    [HideInInspector]
    public float offset;
    public float intensity = 2.5f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = transform.up * -(Mathf.Abs(Mathf.Sin(Time.time + offset)) * intensity) + startPos;
    }
}
