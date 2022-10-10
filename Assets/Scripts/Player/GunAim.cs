using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    private Camera cam;
    Transform crosshair;
    public Vector3 aimDir;
    public float angle;

    bool flipped = false;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get direction to crosshair
        Vector3 crosshairWorldPos = cam.ScreenToWorldPoint(crosshair.position);
        crosshairWorldPos.z = 0;

        aimDir = (crosshairWorldPos - transform.position).normalized;

        // Rotate to point towards crosshair
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Flip gun
        if ((angle > 90) || (angle < -90))
        {
            if (flipped == false)
            {
                flipped = true;
                transform.localScale = new Vector3(1, -1, 1);
            }
        }
        else if (flipped == true)
        {
            flipped = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
