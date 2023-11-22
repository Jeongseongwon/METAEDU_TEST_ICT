using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Message : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update

    private GameObject Launcher;
    public bool Contents = false;
    void Start()
    {
        Launcher = GameObject.Find("Launcher");
    }

    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Contents)
            Launcher.GetComponent<GameLauncher_ICT>().Button_Contents();
        
    }
}
