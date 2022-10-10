using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
