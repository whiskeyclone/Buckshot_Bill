using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerTrans;

    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        transform.position = new Vector3(playerTrans.position.x, playerTrans.position.y, transform.position.z); // Set camera position to player position
    }

    void LateUpdate()
    {
        // Follow Player
        transform.position = new Vector3(playerTrans.position.x, playerTrans.position.y, transform.position.z);
    }
}
