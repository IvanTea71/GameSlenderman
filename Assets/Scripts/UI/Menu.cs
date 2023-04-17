using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        Game.Load();
    }

    public void Exit()
    {
        Application.Quit();
    }
    /*
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
    */
}
