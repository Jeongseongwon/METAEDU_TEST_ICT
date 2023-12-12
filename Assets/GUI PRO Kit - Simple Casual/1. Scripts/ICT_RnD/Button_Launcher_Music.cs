using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Launcher_Music : MonoBehaviour, IPointerClickHandler
{
    private GameLauncher_ICT Launcher;

    public bool Play = false;
    public bool Replay = false;
    public bool Stop = false;
    public bool Analysis = false;
    public bool Listening = false;

    void Start()
    {
        Launcher = GameObject.Find("Launcher").GetComponent<GameLauncher_ICT>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        //Music content
        if (Play)
            Launcher.Button_Music_Play();

        if (Replay)
            Launcher.Button_Music_Replay();

        if (Stop)
            Launcher.Button_Music_Stop();

        if (Analysis)
            Launcher.Button_Music_Analysis();

        if (Listening)
            Launcher.Button_Music_Listening();
    }
}
