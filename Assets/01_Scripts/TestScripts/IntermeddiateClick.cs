using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���콺 ��Ʈ Ŭ�� �̺�Ʈ�� ����
//Ŭ���� ��ü�� ���� �޼��� ȣ���Ͽ� �ش� ��ü�� ���� �������
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

            if (Vector3.Distance(mousePosition, screenCenter) < 1.0f)  // Ŭ�� ��ġ�� ȭ�� �߾��� �Ÿ��� ��
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

    //���콺 ��ġ���� ���̸� ���� � ��ü�� �ε������� �˻�
    //out RaycastHit hit �Ű������� ���� �浹 ���� ��ȯ
    private bool LookForGameObjcet(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    private void PressDownGameObject(GameObject targetObject)
    {
        Debug.Log("���� ��ü" + targetObject);
        
    }

    private void PressUpGameObject(GameObject targetObject)
    {
        Debug.Log("�� ��ü" + targetObject);
    }
}
