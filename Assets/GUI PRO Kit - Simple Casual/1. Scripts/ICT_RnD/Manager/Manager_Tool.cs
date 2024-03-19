using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Tool : MonoBehaviour
{
    public static Manager_Tool instance = null;

    public GameObject Tool_BGM_UI;
    public GameObject Tool_Inst_UI;

    private int[] Inst_num = new int[3] { 0, 0, 0 };
    public GameObject[] Inst_group = new GameObject[3];
    public GameObject Tool_TestObject;
    public Text Text_Music;

    private int BGM_num;
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

    void Init()
    {
        //BGM ���� �ʱ� ���� �κ�, �ʱ� ������ BGM�� 0�� ��ġ
        BGM_num = 0;

        ///�Ǳ� ���� ȭ�� �ʱ� ���� �κ�
        Inst_num[0] = 0;
        Inst_num[1] = 1;
        Inst_num[2] = 0;
        Active_SelectedInst(0, 0);
        Active_SelectedInst(1, 1);
        Active_SelectedInst(2, 0);
    }

    public void Active_Inst()
    {
        Tool_Inst_UI.SetActive(true);
        Tool_BGM_UI.SetActive(false);

        Tool_TestObject.SetActive(true);
    }
    public void Save_Inst_Next()
    {
        //�޽��� �˾�
        //�� ��ȣ ���� �� ��ȣ �ʱ�ȭ
        Init();
    }

    public void Change_Inst_Next(int selected)
    {
        Inst_num[selected] += 1;
        if (Inst_num[selected] > 3)
            Inst_num[selected] = 0;
        Active_SelectedInst(selected, Inst_num[selected]);
    }
    public void Change_Inst_Prev(int selected)
    {

        Inst_num[selected] -= 1;
        if (Inst_num[selected] < 0)
            Inst_num[selected] = 3;
        Active_SelectedInst(selected, Inst_num[selected]);
    }
    void Active_SelectedInst(int selected, int num)
    {
        GameObject SelectedGroup = Inst_group[selected];
        GameObject SelectedInst;

        //���� ��Ȱ��ȭ �� �ش� ��ȣ ������Ʈ�� �ٽ� Ȱ��ȭ
        for (int i = 0; i < SelectedGroup.transform.childCount; i++)
        {
            SelectedInst = SelectedGroup.transform.GetChild(i).gameObject;
            SelectedInst.SetActive(false);
        }
        SelectedGroup.transform.GetChild(num).gameObject.SetActive(true);
    }
    public void Inactive_BGM()
    {
        Tool_BGM_UI.SetActive(false);
    }
    public void Active_BGM()
    {
        Tool_BGM_UI.SetActive(true);
        Tool_Inst_UI.SetActive(false);

        Tool_TestObject.SetActive(false);
        Init();
    }
    public void Change_BGM_Next()
    {
        BGM_num += 1;
        if (BGM_num > 3)
            BGM_num = 0;
        Change_Text_Music();
    }
    public void Change_BGM_Prev()
    {
        BGM_num -= 1;
        if (BGM_num < 0)
            BGM_num = 3;
        Change_Text_Music();
    }
    void Change_Text_Music()
    {
        if (BGM_num == 0)
        {
            Text_Music.text = "Ȱ���� ��Ÿ��";
        }else if (BGM_num == 1)
        {
            Text_Music.text = "���� 0";
        }
        else if (BGM_num == 2)
        {
            Text_Music.text = "���� 1";
        }
        else if (BGM_num == 3)
        {
            Text_Music.text = "���� 2";
        }
    }
    public void Play_BGM()
    {
        Manager_audio.instance.Set_BGM_Audiosource(BGM_num);
    }
}
