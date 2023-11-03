using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLauncher_ICT : MonoBehaviour
{
    public GameObject Loading;
    public GameObject Home;
    public GameObject Setting;
    public GameObject Tool;
    public GameObject Result;
    public GameObject Monitoring_C1;
    public GameObject Monitoring_C2;

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

    public void Button_Gamestart()
    {
        //이전 화면 비활성화
        Monitoring_C1.SetActive(true);
    }
    public void Button_Contents(int contentname)
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Button_Back_ToHome()
    {
        //이전 화면 비활성화
        Tool.SetActive(false);
        Result.SetActive(false);

        Home.SetActive(true);
    }

    public void Button_Back_ToTool()
    {
        //이전 화면 비활성화
        Monitoring_C1.SetActive(false);
        Monitoring_C2.SetActive(false);

        Tool.SetActive(true);
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

    public void Button_Home()
    {
        //이전 화면 비활성화
        Tool.SetActive(false);
        Result.SetActive(false);
        Monitoring_C1.SetActive(false);
        Monitoring_C2.SetActive(false);

        Home.SetActive(true);
    }
    public void Button_START()
    {
        //이전 화면 비활성화
        Tool.SetActive(false);

        //게임시작
        Monitoring_C1.SetActive(true);
    }
    public void Button_Teacher_UI(int UIindex)
    {
        Home.SetActive(false);
        if (UIindex == 0)
        {
            Tool.SetActive(true);
        }
        else if (UIindex == 1)
        {
            Monitoring_C1.SetActive(true);
        }
        else if (UIindex == 2)
        {
            Result.SetActive(true);
        }
    }
}
