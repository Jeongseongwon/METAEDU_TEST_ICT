using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message_anim_controller : MonoBehaviour
{
    private List<string> Animation_clip = new List<string>();
    private Animation Message_anim;
    public Text Message_text;
    //0 : On, 1 : Off

    public bool Intro = false;

    // Start is called before the first frame update
    void Start()
    {
        Message_anim = this.GetComponent<Animation>();
        Init_Animation();

        //if (Intro)
        //{
        //    StartCoroutine(Active_false_time(3f));
        //}
    }

    public void Animation_On()
    {
        Message_anim.Play(Animation_clip[0]);
    }
    public void Animation_Off()
    {
        Message_anim.Play(Animation_clip[1]);
        StartCoroutine(Active_false());
    }

    public void Animation_On_Off()
    {
        Message_anim.Play(Animation_clip[2]);
        //StartCoroutine(Active_false_time(5f,1f));
    }

    public void Change_text(string Field)
    {
        Message_text.text = Field;
    }
    void Init_Animation()
    {
        foreach(AnimationState state in Message_anim)
        {
            Animation_clip.Add(state.name);
        }
    }

    IEnumerator Active_false()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    IEnumerator Active_false_time(float timer_1)
    {
        yield return new WaitForSeconds(timer_1);
        this.gameObject.SetActive(false);
    }

    //IEnumerator Active_false_time(float timer_1, float timer_2)
    //{
    //    yield return new WaitForSeconds(timer_1);
    //    Message_anim.Play(Animation_clip[1]);
    //    yield return new WaitForSeconds(timer_2);
    //    this.gameObject.SetActive(false);
    //}
}
