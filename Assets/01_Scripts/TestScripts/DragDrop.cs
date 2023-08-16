using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오류 : 카메라의 기준에서 자꾸 이동하게 됨.

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    //드롭이 완료된 경우 해당 물체가 이동할 목적지 태그
    [SerializeField] string destinationTag = "DropArea";

    //마우스 버튼이 눌렸을때 호출되는 함수
    private void OnMouseDown()
    {
        //드래그 중인 물체의 원래 위치와 마우스 위치 간의 차이를 저장하기 위해
        offset = transform.position - MouseWorldPosition();
        //물체의 콜라이더 비활성화 -> 잡고 있는 동안 다른 물체와 충돌하지 않도록 하기 위해
        transform.GetComponent<Collider>().enabled = false;
    }

    //마우스 드래그 중
    private void OnMouseDrag()
    {
        //물체를 드래그하면서 이동
        transform.position = MouseWorldPosition() + offset;
    }
    
    //마우스 버튼 놓여졌을 때
    //카메라에서 마우스 위치까지 Raycast를 발사하여 Raycast가 충돌한 객체 정보를 확인
    private void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            //충돌한 객체의 목적지의 태그와 같은지 검사
            if(hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
            }
        }
        transform.GetComponent<Collider>().enabled = true;
    }

    //마우스 스크린 위치를 월드 위치로 변환하는 함수
    Vector3 MouseWorldPosition()
    {
        //현재 마우스 스크린 위치에 해당 물체의 z월드 위치를 더하여 마우스 월드 위치를 계산
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
