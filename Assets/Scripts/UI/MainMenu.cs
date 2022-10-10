using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.instance.PlaySound("Shoot"); // Play shoot sound
        SceneChanger.instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1); // Move to next room
    }
}
