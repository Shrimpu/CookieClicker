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
    public bool die = false; // one-time cookie

    void Start()
    {
        Camera cam = Camera.main;
        height = cam.orthographicSize; // some fancy stuff i found on the web
        width = height * cam.aspect; // thank you random user
        sr = GetComponent<SpriteRenderer>();

        childsr = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) // yeah, my cookie sucks and is built with unity-sprites so there's a lot.
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
        SetNewPos();
    }

    void SetNewPos()
    {
        // calculate values
        float maxPercentage = Random.Range(0f, 1f); // the big boi that controlls all other values.
        fallSpeed = Mathf.Abs(minMaxFallSpeed.x - minMaxFallSpeed.y) * maxPercentage + minMaxFallSpeed.x; // same
        float scale = Mathf.Abs(minMaxScale.x - minMaxScale.y) * maxPercentage + minMaxScale.x;           // thing
        float alpha = ((255 - minAlpha) * maxPercentage + minAlpha) / 255f;                               // different number
        int layer = (int)(100f * maxPercentage) - 100 + -1;     // its always atleast -1

        // assign all values
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
