using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void Update()
    {
        MoveEtcObject();
    }

    private void MoveEtcObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            // ray 가 닿은 곳이 땅이라면
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // 새로운 위치 설정 및 UI 변경
                // -> UI 변경 시 자동으로 위치가 변경됨
                Vector3 pos = hitInfo.point;
                pos.y = 0f;
            }
        }
    }
}
