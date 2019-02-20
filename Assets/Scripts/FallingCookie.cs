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
    float fallThing;
    SpriteRenderer sr;

    [HideInInspector]
    public bool die = false;

    void Start()
    {
        Camera cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        sr = GetComponent<SpriteRenderer>();

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
        fallSpeed = Random.Range(minMaxFallSpeed.x, minMaxFallSpeed.y);
        fallThing = (fallSpeed - minMaxFallSpeed.x) / minMaxFallSpeed.y;
        float scale = minMaxScale.y * fallThing + minMaxScale.x;
        float alpha = (255 - minAlpha) * fallThing;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (minAlpha + alpha) / 255f);
        transform.position = new Vector2(Random.Range(-width, width), height);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }
}
