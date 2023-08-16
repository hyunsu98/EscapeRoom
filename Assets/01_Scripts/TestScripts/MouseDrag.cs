using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    //�̻���
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    //�巡�� �Ǵ� ���� �ٸ� ���� ������ ���� �ʵ���
    private void OnMouseDrag()
    {
        //���콺 ��ġ
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z + transform.position.z);
        //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

        //��ü�� ���� �� �� ��ü�� z ���� ���� �ڷ���Ʈ�ϰų� ��鸮�� �ʵ��� �ϱ� ����
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //��ü�� ��ġ�� ���ο� ��ü�� ��ġ�� �̵�
        transform.position = objPosition;

        //�߷�
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }
}
