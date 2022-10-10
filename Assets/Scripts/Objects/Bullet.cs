using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    FadeOut fadeOut;
    float moveSpeed = 0f;
    const float invulnTime = 0.2f; // Time period after bullet is created that bullet can't be destroyed by walls/spikes
    float invulnCounter = 0f;
    Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        fadeOut = GetComponent<FadeOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut.beganFading == false)
        {
            invulnCounter += Time.deltaTime;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // Move
        }
        else if (fadeOut.doneFading == true)
        {
            Destroy(gameObject); // Destroy object when done fading
        }
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // Destroy when touching wall or hazard
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invulnCounter >= invulnTime)
        {
            if ((collision.tag == "Solid") || (collision.tag == "Hazard"))
            {
                Destroy(gameObject.GetComponent<KillPlayer>()); // Make bullet non-lethal
                fadeOut.beganFading = true; // Begin fading
            }
        }
    }
}
