using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Tool : MonoBehaviour
{
    public static Manager_Tool instance = null;

    public GameObject Tool_BGM_UI;
    public GameObject Tool_Inst_UI;

    private bool Is_BGM_Selected = false;
    private bool Is_Inst_Selected = false;


    private GameObject Inst_1;
    private GameObject Inst_2;
    private GameObject Inst_3;
    //�켱�� Ŭ���ϸ� �ش� ��ȣ�� ���������� ��µǰ� ����

    //�ش� �ϴ� ������ �ٲٰ�
    //�ش� �ϴ� �� �����س��� �־����


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }


    public void Active_Inst()
    {
        Tool_Inst_UI.SetActive(true);
        //BGM �����ϰ� �ִ��� ���� �Ǵ� �ʱ�ȭ
        Is_Inst_Selected = true;
        Is_BGM_Selected = false;
    }
    //�Ǳ� �����ϰ� �ִ��� ���� �Ǵ� �ʱ�ȭ �ʿ�

    public void Change_Inst(int selected)
    {
        //�켱 Inst�� Ȱ��ȭ �Ǿ��ִ��� Ȯ��

        if (Is_Inst_Selected)
        {
            if(selected == 0)
            {
                //�ش� �ϴ� ��ġ �ִϸ��̼� �� ����
            }
            else if(selected == 1)
            {

            }
            else if (selected == 2)
            {

            }
            else if (selected == 3)
            {

            }
            else if (selected == 4)
            {

            }
            else if (selected == 5)
            {

            }
        }
    }
    public void Active_BGM()
    {
        Tool_BGM_UI.SetActive(true);
        //�Ǳ� �����ϰ� �ִ��� ���� �Ǵ� �ʱ�ȭ �ʿ�

        Is_Inst_Selected = false;
        Is_BGM_Selected = true;
    }
    public void Change_BGM()
    {
        Tool_BGM_UI.SetActive(true);
        //�Ǳ� �����ϰ� �ִ��� ���� �Ǵ� �ʱ�ȭ �ʿ�
    }
}
