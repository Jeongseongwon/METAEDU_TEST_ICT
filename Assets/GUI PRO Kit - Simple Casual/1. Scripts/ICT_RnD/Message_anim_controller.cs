using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message_anim_controller : MonoBehaviour
{
    private List<string> Animation_clip = new List<string>();
    private Animation Message_anim;
    //0 : On, 1 : Off

    // Start is called before the first frame update
    void Start()
    {
        Message_anim = this.GetComponent<Animation>();
        Init_Animation();
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
}
