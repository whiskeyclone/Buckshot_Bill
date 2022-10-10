using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    Transform spriteTrans;

    [SerializeField] int shootDirection = 0; // 0 = right, 1 = up, 2 = left, 3 = down
    [SerializeField] float shootRate = 1f;
    [SerializeField] float bulletSpeed = 10f;

    Vector2 shootVector = Vector2.right;
    float shootCounter;

    const float bulletSpawnOffset = 0f;

    private void Start()
    {
        shootCounter = shootRate;
        spriteTrans = transform.GetChild(0);

        // Set rotation, position, and shootVector based on shootDirection
        spriteTrans.Rotate(new Vector3(0, 0, shootDirection * 90));

        if (shootDirection == 1)
        {
            spriteTrans.localPosition = new Vector3(0.017f, 0.019f, 0f);
            shootVector = Vector2.up;
        }
        else if (shootDirection == 2)
        {
            spriteTrans.localPosition = new Vector3(-0.032f, 0.015f, 0f);
            shootVector = Vector2.left;
        }
        else if (shootDirection == 3)
        {
            spriteTrans.localPosition = new Vector3(-0.013f, -0.035f, 0f);
            shootVector = Vector2.down;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        shootCounter += Time.deltaTime;

        if (shootCounter >= shootRate)
        {
            Shoot();
            shootCounter = 0;
        }
    }

    void Shoot()
    {
        Vector2 bulletSpawnPosition = new Vector2(transform.position.x, transform.position.y) + shootVector * bulletSpawnOffset;

        GameObject bulletInstance = Instantiate(bullet, bulletSpawnPosition, transform.rotation); // Create Bullet
        bulletInstance.GetComponent<Bullet>().SetDirection(shootVector); // Set bullet direction
        bulletInstance.GetComponent<Bullet>().SetSpeed(bulletSpeed); // Set bullet speed

        AudioManager.instance.PlaySound("Shoot 2");

        /*
        if (AudioManager.instance.IsSoundPlaying("Shoot 2") == false) // If shoot sound is not currently playing, play shoot sound
        {
            AudioManager.instance.PlaySound("Shoot 2");
        }
        */
    }
}
