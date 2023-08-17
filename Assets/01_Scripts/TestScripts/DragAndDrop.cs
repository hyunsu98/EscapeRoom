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
            // ray �� ���� ���� ���̶��
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // ���ο� ��ġ ���� �� UI ����
                // -> UI ���� �� �ڵ����� ��ġ�� �����
                Vector3 pos = hitInfo.point;
                pos.y = 0f;
            }
        }
    }
}
