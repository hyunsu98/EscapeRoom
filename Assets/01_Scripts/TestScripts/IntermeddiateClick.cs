using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마우스 버트 클릭 이벤트를 감지
//클릭한 객체에 따라 메서드 호출하여 해당 객체에 대한 정보얻기
public class IntermeddiateClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LookForGameObjcet(out RaycastHit hit))
            {
                PressDownGameObject(hit.collider.gameObject);
            }

            /*Vector3 mousePosition = Input.mousePosition;
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, mousePosition.z);

            if (Vector3.Distance(mousePosition, screenCenter) < 1.0f)  // 클릭 위치와 화면 중앙의 거리를 비교
            {
                if (LookForGameObjcet(out RaycastHit hit))
                {
                    PressDownGameObject(hit.collider.gameObject);
                }
            }*/
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (LookForGameObjcet(out RaycastHit hit))
            {
                PressUpGameObject(hit.collider.gameObject);
            }
        }
    }

    //마우스 위치에서 레이를 쏴서 어떤 객체에 부딪히는지 검사
    //out RaycastHit hit 매개변수를 통해 충돌 정보 반환
    private bool LookForGameObjcet(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    private void PressDownGameObject(GameObject targetObject)
    {
        Debug.Log("닿은 물체" + targetObject);
        
    }

    private void PressUpGameObject(GameObject targetObject)
    {
        Debug.Log("뗀 물체" + targetObject);
    }
}
