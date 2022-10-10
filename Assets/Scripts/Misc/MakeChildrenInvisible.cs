using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildrenInvisible : MonoBehaviour
{
    [SerializeField] bool invisible = false;

    // Start is called before the first frame update
    void Start()
    {
        if (invisible == true)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
