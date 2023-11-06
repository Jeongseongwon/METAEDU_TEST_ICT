using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameLauncher_ICT : MonoBehaviour
{
    public GameObject ICT_RnD_UI;
    private GameObject Loading;
    private GameObject Home;
    private GameObject Setting;
    private GameObject Tool;
    private GameObject Result;
    private GameObject Contents;
    private GameObject Monitoring_C1;
    private GameObject Monitoring_C2;
    private GameObject Monitoring_C3;
    private GameObject Monitoring_C4;

    public GameObject Message_UI;
    private GameObject Message_Tool;
    private GameObject Message_Intro;
    private Message_anim_controller MAC;


    //
    private bool Is_saved = false;


    // Start is called before the first frame update
    [Header("LOADING PAGE COMPONENT")]
    [SerializeField]
    public Slider progressBar;
    public Text loadingPercent;
    public Image loadingIcon;

    private bool loadingCompleted;
    private int nextScene;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadScene());
        StartCoroutine(RotateIcon());

        loadingCompleted = false;
        nextScene = 0;
        Init_page();
    }

    IEnumerator LoadScene()
    {
        //yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        //while (!op.isDone)
        while (true)
        {
            //yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                loadingPercent.text = "progressBar.value";

                if (progressBar.value == 1.0f)
                    op.allowSceneActivation = true;
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;

                    //End of scene index
                    if (nextScene == 2 && loadingCompleted)
                    {
                        StopAllCoroutines();
                    }
                }
            }
        }
    }

    IEnumerator RotateIcon()
    {
        float timer = 0f;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;

            //Debug.Log(progressBar.value);
            //Debug.Log("check");
            if (progressBar.value < 100f)
            {
                progressBar.value = Mathf.RoundToInt(Mathf.Lerp(progressBar.value, 100f, timer / 8));
                loadingIcon.rectTransform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
                loadingPercent.text = progressBar.value.ToString();
            }
            else
            {
                StopAllCoroutines();
                //Debug.Log("100%");

                Loading.SetActive(false);
                //Mode.SetActive(true);
                Home.SetActive(true);
            }
        }
    }

    public void Button_Save()
    {

        //���� ����
        Is_saved = true;

        Tool.SetActive(false);
        Home.SetActive(true);
    }
    public void Button_Back_ToHome()
    {
        //���� ȭ�� ��Ȱ��ȭ
        Tool.SetActive(false);
        Result.SetActive(false);
        Contents.SetActive(false);

        Home.SetActive(true);
    }
    public void Button_Back_ToContent()
    {
        //���� ȭ�� ��Ȱ��ȭ
        Monitoring_C1.SetActive(false);
        Monitoring_C2.SetActive(false);
        Monitoring_C3.SetActive(false);
        Monitoring_C4.SetActive(false);

        Contents.SetActive(true);
    }
    public void Button_Setting()
    {
        Setting.SetActive(true);
    }

    public void Button_Close()
    {
        Setting.SetActive(false);
    }

    public void Button_Home()
    {
        //���� ȭ�� ��Ȱ��ȭ
        Tool.SetActive(false);
        Result.SetActive(false);
        Contents.SetActive(false);

        //�ش� ������ ���� ���� �ʿ�
        Monitoring_C1.SetActive(false);
        Monitoring_C2.SetActive(false);
        Monitoring_C3.SetActive(false);
        Monitoring_C4.SetActive(false);

        Home.SetActive(true);
    }
    public void Button_Tool()
    {
        Home.SetActive(false);
        Tool.SetActive(true);
    }
    public void Button_Result()
    {
        Home.SetActive(false);
        Result.SetActive(true);
    }
    public void Button_Contents()
    {
        Home.SetActive(false);
        Contents.SetActive(true);
    }
    public void Button_START()
    {
        //���� ȭ�� ��Ȱ��ȭ
        Tool.SetActive(false);

        //���ӽ���
        Monitoring_C1.SetActive(true);
    }

    public void Run_Contents(int contentname)
    {
        //���� ��ȯ
        Is_saved = false;

        Contents.SetActive(false);

        //�ش� ������ ���� ���� ��� ����
        Dummy_setting_content();

        //Message_Intro setting
        Message_Intro.SetActive(true);

        if (contentname == 0)
        {
            Monitoring_C1.SetActive(true);
            MAC.Change_text("ģ���� �ɺ��̿� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 1)
        {
            Monitoring_C2.SetActive(true);
            MAC.Change_text("ģ���� ��ٿ� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 2)
        {
            Monitoring_C3.SetActive(true);
            MAC.Change_text("ģ���� �˷ο��� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 3)
        {
            Monitoring_C4.SetActive(true);
            MAC.Change_text("ģ���� �������� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        //SceneManager.LoadSceneAsync(1);
    }
    public void Run_Contents_Func(int contentname)
    {
        //�ٸ� ������ ���α�� ���� ���ΰ� ��Ȱ��ȭ

        //������ ���� �� ��ɿ� ���� ����
        Dummy_setting_content_Func();


        //���� ������ ���α�ɰ��� UI ����
        Message_Intro.SetActive(true);

        if (contentname == 0)
        {
            Monitoring_C1.SetActive(true);
            MAC.Change_text("������� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 1)
        {
            Monitoring_C2.SetActive(true);
            MAC.Change_text("�˰��� ���������?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 2)
        {
            Monitoring_C3.SetActive(true);
            MAC.Change_text("���̿� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 3)
        {
            Monitoring_C4.SetActive(true);
            MAC.Change_text("ģ���� �������� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 4)
        {
            Monitoring_C2.SetActive(true);
            MAC.Change_text("ģ���� ��ٿ� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 5)
        {
            Monitoring_C3.SetActive(true);
            MAC.Change_text("ģ���� �˷ο��� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        else if (contentname == 6)
        {
            Monitoring_C4.SetActive(true);
            MAC.Change_text("ģ���� �������� ���� �˾ƺ����?");
            MAC.Animation_On_Off(); ;
        }
        //SceneManager.LoadSceneAsync(1);
    }

    public void Button_Message_Contents()
    {
        if (Is_saved)
        {
            Button_Contents();
        }
        else
        {
            Message_Tool.SetActive(true);
        }

    }

    void Dummy_setting_content()
    {
        //������ ����
    }
    void Dummy_setting_content_Func()
    {
        //������ ���� ��� ����
    }

    void Init_page()
    {
        Loading = ICT_RnD_UI.transform.GetChild(0).gameObject;
        Home = ICT_RnD_UI.transform.GetChild(1).gameObject;
        Tool = ICT_RnD_UI.transform.GetChild(2).gameObject;
        Result = ICT_RnD_UI.transform.GetChild(3).gameObject;
        Contents = ICT_RnD_UI.transform.GetChild(4).gameObject;
        Monitoring_C1 = ICT_RnD_UI.transform.GetChild(5).gameObject;
        Monitoring_C2 = ICT_RnD_UI.transform.GetChild(6).gameObject;
        Monitoring_C3 = ICT_RnD_UI.transform.GetChild(7).gameObject;
        Monitoring_C4 = ICT_RnD_UI.transform.GetChild(8).gameObject;
        Setting = ICT_RnD_UI.transform.GetChild(9).gameObject;

        Message_Tool = Message_UI.transform.GetChild(0).gameObject;
        Message_Intro = Message_UI.transform.GetChild(1).gameObject;

        //Message_Intro setting, text�ܰ迡�� scale 0,0,0���� ����
        Message_Intro.SetActive(true);
        MAC = Message_Intro.GetComponent<Message_anim_controller>();
    }
}
