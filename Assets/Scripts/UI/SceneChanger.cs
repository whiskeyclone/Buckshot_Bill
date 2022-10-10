using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    [SerializeField] Animator animator;
    bool sceneEnding = false;
    bool sceneStarting = true;
    AnimatorStateInfo stateInfo;
    int sceneToChangeToIndex;

    private void Start()
    {
        instance = this;

        // Play music if needed at start of level
        string currentSceneName = SceneManager.GetActiveScene().name; // get current scene name

        if ((currentSceneName == "main_menu") || (currentSceneName == "intro") || (currentSceneName == "outro")) // if in main menu or cutscene
        {
            AudioManager.instance.StopSound("Shekels"); // stop playing Shekels

            if (AudioManager.instance.IsSoundPlaying("Doubloons") == false) // if Doubloons isn't playing
            {
                AudioManager.instance.PlaySound("Doubloons"); // play Doubloons
            }
        }
        else
        {
            AudioManager.instance.StopSound("Doubloons"); // stop playing Doubloons

            if (AudioManager.instance.IsSoundPlaying("Shekels") == false) // if Shekels isn't playing
            {
                AudioManager.instance.PlaySound("Shekels"); // play Shekels
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStarting == true)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Get current state info

            if ((stateInfo.IsName("wipe_end_anim")) && (stateInfo.normalizedTime >= 1)) // If wipe end anim is over (plays during start of stage)
            {
                if (GameObject.Find("Player") != null) // If player exists, unfreeze player
                {
                    Player.instance.frozen = false;
                }

                sceneStarting = false;
            }
        }
        else if (sceneEnding == true)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Get current state info

            if ((stateInfo.IsName("wipe_start_anim")) && (stateInfo.normalizedTime >= 1)) // If wipe start anim is over (plays during end of stage)
            {
                SceneManager.LoadScene(sceneToChangeToIndex); // Change scenes
                sceneEnding = false;
            }
        }
        
    }

    public void ChangeScene(int sceneIndex)
    {
        sceneToChangeToIndex = sceneIndex;
        animator.SetTrigger("Wipe Start");
        sceneEnding = true;
    }
}
