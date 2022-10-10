using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided with player
        if (collision.tag == "Player")
        {
            if (Player.instance.frozen == false)
            {
                Player.instance.Die();
            }
        }
    }
}
