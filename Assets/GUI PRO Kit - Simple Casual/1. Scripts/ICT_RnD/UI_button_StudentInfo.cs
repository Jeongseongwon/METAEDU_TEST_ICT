using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_button_StudentInfo : MonoBehaviour, IPointerClickHandler
{
    private Text Text_Student_Name;
    private Text Text_Student_ID;
    private Text Text_Student_BirthDate;
    public string Student_Name;
    public string Student_ID;
    public string Student_BirthDate;


    public int Result_num = -1;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //클릭할 경우 해당 데이터 로그인 정보로 저장
            //최종 선택 버튼 클릭시 해당 데이터 저장해서 로그인 매니저로 전달
        }
    }

    void Start()
    {
        Text_Student_Name = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        Text_Student_ID = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        Text_Student_BirthDate = this.transform.GetChild(2).gameObject.GetComponent<Text>();

        Text_Student_Name.text = Student_Name;
        Text_Student_ID.text = "#" + Student_ID;
        Text_Student_BirthDate.text = Student_BirthDate;
    }
}
