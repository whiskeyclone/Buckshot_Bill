using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
