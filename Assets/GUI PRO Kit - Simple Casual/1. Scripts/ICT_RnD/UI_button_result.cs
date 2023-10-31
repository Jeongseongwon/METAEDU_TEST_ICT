using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_button_result : MonoBehaviour, IPointerClickHandler
{
    public int Result_num=-1;
    // Start is called before the first frame update
   

    //부모 오브젝트로 부터 몇번째 자식인지 체크
    //해당 순서의 데이터 각각의 데이터 오브젝트에 저장
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Result_num != -1)
            {
                Manager_data.instance.Change_result(Result_num);
                
            }
        }
    }
}
