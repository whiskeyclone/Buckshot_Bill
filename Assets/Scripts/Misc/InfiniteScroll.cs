using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    float cameraXPos;
    float textureUnitWidth;

    // Start is called before the first frame update
    void Start()
    {
        cameraXPos = Camera.main.transform.position.x;

        // Get textureUnitWidth
        Sprite sprite = GetComponent<SpriteRenderer>().sprite; 
        Texture2D texture = sprite.texture;
        textureUnitWidth = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        transform.position = new Vector3(transform.position.x + scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        // Reposition object
        if (Mathf.Abs(cameraXPos - transform.position.x) >= textureUnitWidth)
        {
            float offset = (cameraXPos - transform.position.x) % textureUnitWidth;
            transform.position = new Vector3(cameraXPos + offset, transform.position.y, transform.position.z);
        }
    }
}
