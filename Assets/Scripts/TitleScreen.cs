using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void RecreatedLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void InnovatedLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
