using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPlatform : MonoBehaviour
{
    BoxCollider2D col;
    FadeOut fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        fadeOut = GetComponent<FadeOut>();
    }

    // Update is called once per frame
    void Update()
    {
        // Upon player shooting for the first time, destroys collider and fades sprite out
        if (fadeOut.beganFading == false)
        {
            if (Player.instance.playerShotOnce == true)
            {
                fadeOut.beganFading = true;
                Destroy(col);
            }
        }
        else if (fadeOut.doneFading == true)
        {
            Destroy(gameObject); // Destroy this object when done fading
        }
    }
}
