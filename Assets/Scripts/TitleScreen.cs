using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script for the title screen. Has methods for the buttons on the sceen
public class TitleScreen : MonoBehaviour
{
    //Load recreated level
    public void RecreatedLevel()
    {
        SceneManager.LoadScene(1);
    }

    //Load innovated level
    public void InnovatedLevel()
    {
        SceneManager.LoadScene(2);
    }

    //Quit game
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
