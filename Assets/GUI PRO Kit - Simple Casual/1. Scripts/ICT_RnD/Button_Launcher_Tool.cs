using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Launcher_Tool : MonoBehaviour, IPointerClickHandler
{
    public bool Inst = false;
    public bool Inst_Save = false;

    public bool BGM = false;
    public int Select = -1;

    private GameLauncher_ICT Launcher;
    void Start()
    {
        Launcher = GameObject.Find("Launcher").GetComponent<GameLauncher_ICT>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inst)
            Manager_Tool.instance.Active_Inst();

        if (Inst_Save)
            Manager_Tool.instance.Active_Inst();

        if (BGM)
            Manager_Tool.instance.Active_BGM();


        if (Select != -1)
            Manager_Tool.instance.Change_Inst(Select);
    }
}
