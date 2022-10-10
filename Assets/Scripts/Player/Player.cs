using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    PlayerController controller;
    GunAim gunAim;
    AmmoUI ammoUI;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Animator animator;
    [SerializeField] Animator gunAnimator;

    Vector2 velocity = Vector2.zero;
    const float gravity = -20f;
    const float maxStrafeVelocity = 15f;
    const float strafeAccel = 50f;
    const float shootVelocity = 20f;
    const float trampBounceMult = 0.25f;
    const float friction = 100f;
    const float wallBounceMult = 0.5f;

    const float superAmmoVelocity = 30f;

    const float shootStrafeLockTime = 0.25f;
    const float bounceStrafeLockTime = 0.25f;
    float strafeLockCounter = 0;

    const float shootLockTime = 0.05f;
    float shootLockCounter = 0f;

    [System.NonSerialized] public char[] ammo = { 'R', 'R' }; // R = regular ammo, S = super ammo, N = none
    [System.NonSerialized] public bool frozen = true;
    [System.NonSerialized] public bool playerShotOnce = false;

    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        controller = GetComponent<PlayerController>();
        gunAim = GetComponentInChildren<GunAim>();
        ammoUI = GameObject.Find("Ammo UI").GetComponent<AmmoUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen == false)
        {
            if (Input.GetKeyDown(KeyCode.R)) // Reload scene when R key is pressed
            {
                SceneChanger.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Bounce when hitting a wall
            if ((controller.collisionInfo.left == true) || (controller.collisionInfo.right == true))
            {
                strafeLockCounter = bounceStrafeLockTime; // Prevent strafing for a time
                velocity.x = velocity.x * -1 * wallBounceMult; // Set velocity
                AudioManager.instance.PlaySound("Impact"); // Play impact sound
            }

            // Get strafe input
            float strafeInput = Input.GetAxisRaw("Horizontal");

            // Reset vertical velocity and ammo and apply friction
            if (controller.collisionInfo.below == true)
            {
                velocity.y = 0; // Reset vertical velocity

                // Apply friction
                if (velocity.x != 0)
                {
                    if (velocity.x > 0)
                    {
                        velocity.x = Mathf.Clamp(velocity.x - friction * Time.deltaTime, 0, int.MaxValue);
                    }
                    else
                    {
                        velocity.x = Mathf.Clamp(velocity.x + friction * Time.deltaTime, int.MinValue, 0);
                    }
                }
            }

            // Shooting
            shootLockCounter -= Time.deltaTime;

            if ((Input.GetMouseButtonDown(0)) && (GetTotalAmmo() > 0) && (shootLockCounter <= 0))
            {
                Shoot();
            }

            // Add strafe velocity to velocity
            strafeLockCounter -= Time.deltaTime;

            if ((controller.collisionInfo.below == false) && (strafeLockCounter <= 0) && (strafeInput != 0))
            {
                if (strafeInput == Mathf.Sign(velocity.x)) // If player input direction is the same as horizontal direction, prevent player moving at strafeVelocity if they are moving faster than strafeVelocity
                {
                    if (Mathf.Abs(velocity.x) < maxStrafeVelocity)
                    {
                        velocity.x = Mathf.Clamp(velocity.x + strafeAccel * strafeInput * Time.deltaTime, -maxStrafeVelocity, maxStrafeVelocity);
                    }
                }
                else
                {
                    velocity.x = Mathf.Clamp(velocity.x + strafeAccel * strafeInput * Time.deltaTime, -maxStrafeVelocity, maxStrafeVelocity);
                }
            }

            // Bounce when hitting a ceiling
            if (controller.collisionInfo.above == true)
            {
                strafeLockCounter = bounceStrafeLockTime; // Prevent strafing for a time
                velocity.y = velocity.y * -1 * wallBounceMult; // Set velocity
                AudioManager.instance.PlaySound("Impact"); // Play impact sound
            }

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;

            // Move
            controller.Move(velocity * Time.deltaTime);
        }
        else if (dead == true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("bill_death_anim") && (stateInfo.normalizedTime >= 1)) // If death anim is over
            {
                transform.localScale = new Vector3(0, 0, 0); // Make player disappear
                SceneChanger.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
                dead = false;
            }
        }
    }

    void Shoot()
    {
        if (playerShotOnce == false)
        {
            playerShotOnce = true;
        }

        strafeLockCounter = shootStrafeLockTime; // Prevent strafing for a time
        shootLockCounter = shootLockTime; // Prevent shooting for a time

        // Set Velocity
        if (GetCurrentAmmoType() == 'R')
        {
            velocity = gunAim.aimDir * shootVelocity * -1;
        }
        else if (GetCurrentAmmoType() == 'S')
        {
            velocity = gunAim.aimDir * superAmmoVelocity * -1;
        }

        RemoveAmmo();

        // Flip Sprite
        if ((gunAim.angle < 90) && (gunAim.angle > -90))
        {
            if (spriteRend.flipX == false)
            {
                spriteRend.flipX = true;
            }
        }
        else if (spriteRend.flipX == true)
        {
            spriteRend.flipX = false;
        }

        gunAnimator.SetTrigger("Shoot"); // Play shoot animation
        AudioManager.instance.PlaySound("Shoot"); // Play shoot sound
    }

    public void AddRegularAmmo()
    {
        // Add ammo to empty spot
        if (ammo[0] == 'N')
        {
            ammo[0] = 'R';
        }
        else if (ammo[1] == 'N')
        {
            ammo[1] = 'R';
        }

        ammoUI.UpdateAmmoUI();
    }

    public void AddSuperAmmo()
    {
        if (GetTotalAmmo() < 2)
        {
            // Add ammo to empty spot
            if (ammo[0] == 'N')
            {
                ammo[0] = 'S';
            }
            else if (ammo[1] == 'N')
            {
                ammo[1] = 'S';
            }
        }
        else
        {
            // Replace regular ammo
            if (ammo[0] != 'S')
            {
                ammo[0] = 'S';
            }
            else if (ammo[1] != 'S')
            {
                ammo[1] = 'S';
            }
        }

        ammoUI.UpdateAmmoUI();
    }

    public void RemoveAmmo()
    {
        if (ammo[0] != 'N')
        {
            ammo[0] = 'N';
        }
        else if (ammo[1] != 'N')
        {
            ammo[1] = 'N';
        }

        ammoUI.UpdateAmmoUI();
    }

    public int GetRegularAmmo()
    {
        int ammoCount = 0;

        if (ammo[0] == 'R')
        {
            ammoCount++;
        }
        
        if (ammo[1] == 'R')
        {
            ammoCount++;
        }

        return (ammoCount);
    }

    public int GetSuperAmmo()
    {
        int ammoCount = 0;

        if (ammo[0] == 'S')
        {
            ammoCount++;
        }

        if (ammo[1] == 'S')
        {
            ammoCount++;
        }

        return (ammoCount);
    }

    public int GetTotalAmmo()
    {
        int ammoCount = 0;

        if (ammo[0] != 'N')
        {
            ammoCount++;
        }

        if (ammo[1] != 'N')
        {
            ammoCount++;
        }

        return (ammoCount);
    }

    public char GetCurrentAmmoType()
    {
        if (ammo[0] != 'N')
        {
            return (ammo[0]);
        }
        else if (ammo[1] != 'N')
        {
            return (ammo[1]);
        }

        return ('N');
    }

    public void Die()
    {
        if (dead == false)
        {
            frozen = true;
            dead = true;

            spriteRend.flipX = false; // Unflip sprite
            animator.SetTrigger("Death"); // Play death animation
            AudioManager.instance.PlaySound("Splat"); // Play death sound
            Destroy(gunAim.gameObject); // Destroy gun
        }
    }

    public void TrampolineBounce()
    {
        strafeLockCounter = shootStrafeLockTime; // Prevent strafing for a time
        shootLockCounter = shootLockTime; // Prevent shooting for a time

        velocity.x = -1 * (velocity.x + velocity.x * trampBounceMult); // Set velocity
    }
}
