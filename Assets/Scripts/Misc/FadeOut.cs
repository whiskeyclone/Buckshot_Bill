using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer; // renderer of sprite to fade out
    [System.NonSerialized] public bool beganFading = false;
    [SerializeField] const float fadeSpeed = 5f;
    [System.NonSerialized] public bool doneFading = false;

    // Update is called once per frame
    void Update()
    {
        if ((beganFading == true) && (doneFading == false))
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - fadeSpeed * Time.deltaTime); // Fade out

            if (spriteRenderer.color.a <= 0)
            {
                doneFading = true;
            }
        }
    }
}
