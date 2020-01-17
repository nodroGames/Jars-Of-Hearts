using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialogue : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseDialogueUI;
    public Animator uIAnim; 


    public void Nevermind()
    {
        uIAnim.SetTrigger("hidetext");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        uIAnim.SetTrigger("displaytext");
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

}
