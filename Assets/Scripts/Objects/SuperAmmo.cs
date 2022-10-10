using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided with player
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player.GetSuperAmmo() < 2) // Increase super ammo
            {
                player.AddSuperAmmo();
            }

            AudioManager.instance.PlaySound("Reload");
            Destroy(gameObject);
        }
    }
}
