using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided with player
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player.GetTotalAmmo() < 2)
            {
                player.AddRegularAmmo();
                AudioManager.instance.PlaySound("Reload");
                Destroy(gameObject);
            }
        }
    }
}
