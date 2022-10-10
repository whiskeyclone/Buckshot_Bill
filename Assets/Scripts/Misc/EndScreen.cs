using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] float changeTime;
    [SerializeField] Animator messageAnimator;
    [SerializeField] RectTransform clickIconRectTransform;

    float timeCounter = 0f;
    bool changed = false;

    private void Start()
    {
        Cursor.visible = false;    
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if ((timeCounter >= changeTime) && (changed == false)) // Change end message and display click icon
        {
            changed = true;
            messageAnimator.SetTrigger("Next Anim");
            clickIconRectTransform.localScale = new Vector3(1, 1, 1);
        }

        if ((changed == true) && (Input.GetMouseButtonDown(0))) // If end messages are done displaying, return to main menu when left mouse button is pressed
        {
            SceneChanger.instance.ChangeScene(0);
        }
    }
}
