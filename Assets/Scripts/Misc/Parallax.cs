using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxMult = 0.5f;
    [SerializeField] float startPosYOffset = 0f;
    Transform cameraTrans;
    Vector3 lastCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Player.instance.transform.position + new Vector3(0, startPosYOffset, 0); // Set position to player position + offset

        cameraTrans = Camera.main.transform;
        lastCameraPos = cameraTrans.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Get difference between current camera position and last camera position
        Vector3 movement = cameraTrans.position - lastCameraPos;
        movement.z = 0;

        // Move background
        transform.position += movement * parallaxMult;

        // Update last camera position
        lastCameraPos = cameraTrans.position;
    }
}
