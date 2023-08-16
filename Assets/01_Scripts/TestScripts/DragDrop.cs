using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� : ī�޶��� ���ؿ��� �ڲ� �̵��ϰ� ��.

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    //����� �Ϸ�� ��� �ش� ��ü�� �̵��� ������ �±�
    [SerializeField] string destinationTag = "DropArea";

    //���콺 ��ư�� �������� ȣ��Ǵ� �Լ�
    private void OnMouseDown()
    {
        //�巡�� ���� ��ü�� ���� ��ġ�� ���콺 ��ġ ���� ���̸� �����ϱ� ����
        offset = transform.position - MouseWorldPosition();
        //��ü�� �ݶ��̴� ��Ȱ��ȭ -> ��� �ִ� ���� �ٸ� ��ü�� �浹���� �ʵ��� �ϱ� ����
        transform.GetComponent<Collider>().enabled = false;
    }

    //���콺 �巡�� ��
    private void OnMouseDrag()
    {
        //��ü�� �巡���ϸ鼭 �̵�
        transform.position = MouseWorldPosition() + offset;
    }
    
    //���콺 ��ư �������� ��
    //ī�޶󿡼� ���콺 ��ġ���� Raycast�� �߻��Ͽ� Raycast�� �浹�� ��ü ������ Ȯ��
    private void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            //�浹�� ��ü�� �������� �±׿� ������ �˻�
            if(hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
            }
        }
        transform.GetComponent<Collider>().enabled = true;
    }

    //���콺 ��ũ�� ��ġ�� ���� ��ġ�� ��ȯ�ϴ� �Լ�
    Vector3 MouseWorldPosition()
    {
        //���� ���콺 ��ũ�� ��ġ�� �ش� ��ü�� z���� ��ġ�� ���Ͽ� ���콺 ���� ��ġ�� ���
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
