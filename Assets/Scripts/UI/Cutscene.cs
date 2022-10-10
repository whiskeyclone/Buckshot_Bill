using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] Animator animator;
    int slide = 1;
    [SerializeField] int slides = 0;

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            animator.SetTrigger("Next Anim");
            slide++;
            AudioManager.instance.PlaySound("Menu Click");

            if (slide > slides)
            {
                SceneChanger.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1); // Move to next room
            }
        }
    }
}
