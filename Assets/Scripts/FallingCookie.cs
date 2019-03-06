using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCookie : MonoBehaviour
{
    public Vector2 minMaxFallSpeed = new Vector2(1f, 2f);
    public Vector2 minMaxScale = new Vector2(0.5f, 0.8f);
    public float minAlpha = 50f;
    float fallSpeed;
    float height;
    float width;
    SpriteRenderer sr;
    SpriteRenderer[] childsr;

    [HideInInspector]
    public bool die = false;

    void Start()
    {
        Camera cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        sr = GetComponent<SpriteRenderer>();

        childsr = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childsr[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        SetNewPos();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime, Space.World);
    }

    void OnBecameInvisible()
    {
        if (die)
        {
            Destroy(gameObject);
        }
        fallSpeed = Random.Range(minMaxFallSpeed.x, minMaxFallSpeed.y);
        SetNewPos();
    }

    void SetNewPos()
    {
        float maxPercentage = Random.Range(0f, 1f);
        fallSpeed = Mathf.Abs(minMaxFallSpeed.x - minMaxFallSpeed.y) * maxPercentage + minMaxFallSpeed.x;
        float scale = Mathf.Abs(minMaxScale.x - minMaxScale.y) * maxPercentage + minMaxScale.x;
        float alpha = ((255 - minAlpha) * maxPercentage + minAlpha) / 255f;
        int layer = (int)(100f * maxPercentage) - 100 + -1;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        transform.position = new Vector2(Random.Range(-width, width), height + 0.5f);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        sr.sortingOrder = layer;

        for (int i = 0; i < childsr.Length; i++)
        {
            childsr[i].sortingOrder = layer;
        }
    }
}
