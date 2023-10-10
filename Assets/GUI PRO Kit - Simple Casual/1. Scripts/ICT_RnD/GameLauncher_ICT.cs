using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLauncher_ICT : MonoBehaviour
{
    public GameObject Loading;
    public GameObject Mode;
    public GameObject Lobby_teacher;
    public GameObject Lobby_student;
    public GameObject Setting;
    public GameObject Tool;
    public GameObject Monitoring;
    public GameObject Result;
    public GameObject Contents_Student_normal;

    // Start is called before the first frame update
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
                Lobby_teacher.SetActive(true);
            }
        }
    }

    public void Button_Gamestart()
    {
        //Main.SetActive(false);
        //Menu.SetActive(true);
    }
    public void Button_Modes(int contentindex)
    {

        Debug.Log("MODE Check"+ contentindex);
        Mode.SetActive(false);
        if (contentindex == 0)
        {
            Lobby_teacher.SetActive(true);
        }
        else if(contentindex == 1)
        {
            Lobby_student.SetActive(true);
        }
    }
    public void Button_Contents(int contentname)
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Button_Back_ToMode()
    {
        Lobby_teacher.SetActive(false);
        Lobby_student.SetActive(false);
        Mode.SetActive(true);
    }

    public void Button_Back_ToTeacher()
    {
        Lobby_teacher.SetActive(true);
        Tool.SetActive(false);
        Monitoring.SetActive(false);
        Result.SetActive(false);
    }
    public void Button_Back_ToStudent()
    {
        Lobby_student.SetActive(true);
        Contents_Student_normal.SetActive(false);
    }

    public void Button_Setting()
    {
        Setting.SetActive(true);
        //일시정지 기능
    }

    public void Button_Close()
    {
        Setting.SetActive(false);
        //일시정지 해제 기능 추가
    }

    public void Button_Home_Teacher()
    {
        Lobby_teacher.SetActive(true);
        Tool.SetActive(false);
        Monitoring.SetActive(false);
        Result.SetActive(false);
        //지금 UI 비활성화 하는 기능 추가 필요
    }
    public void Button_Home_Student()
    {
        Lobby_student.SetActive(true);
        Contents_Student_normal.SetActive(false);
        //이전 꺼 비활성화하는 기능 추가 필요

        //지금 활성화 되어있는 오브젝트를 찾고
        //그 오브젝트 비활성화 하면 되잖아
    }
    public void Button_Teacher_UI(int UIindex)
    {
        Lobby_teacher.SetActive(false);
        if (UIindex == 0)
        {
            Tool.SetActive(true);
        }
        else if (UIindex == 1)
        {
            Monitoring.SetActive(true);
        }
        else if (UIindex == 2)
        {
            Result.SetActive(true);
        }
    }
    public void Button_Student_UI(int UIindex)
    {
        Debug.Log("Check");
        Lobby_student.SetActive(false);
        if (UIindex == 0)
        {
            Contents_Student_normal.SetActive(true);
        }
        else if (UIindex == 1)
        {
            Monitoring.SetActive(true);
        }
    }
}
