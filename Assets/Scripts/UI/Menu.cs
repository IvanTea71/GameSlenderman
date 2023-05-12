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
}
