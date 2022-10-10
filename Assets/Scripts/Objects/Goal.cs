using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    bool goalTouched = false;
    const float pauseTime = 0.5f;
    float pauseCounter = 0f;

    private void Update()
    {
        if (goalTouched == true)
        {
            // After goal is touched by player, pause for a time before changing scenes
            pauseCounter += Time.deltaTime;

            if (pauseCounter >= pauseTime)
            {
                SceneChanger.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1); // Go to next level
                goalTouched = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided with player
        if (collision.tag == "Player")
        {
            goalTouched = true;
            Player.instance.frozen = true;
            AudioManager.instance.PlaySound("Cash Register");
        }
    }
}
