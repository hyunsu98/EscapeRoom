using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�θ������Ʈ�� ������ �� ȸ���ϸ鼭 ���� ũ��� �ǰ�ʹ�
public class UI_LobbyButton : MonoBehaviour
{
    Vector3 originPos;
    Quaternion originRot;
    Vector3 originScale;
    GameObject parentObj;
    void Start()
    {
        //���� ��ġ ����
        originPos = transform.position;
        //���� ȸ�� ����
        originRot = transform.rotation;
        //���� ũ�� ����
        originScale = transform.localScale;

        //�θ� ������Ʈ
        parentObj = transform.parent.gameObject;

        //���� ��ġ�� �����Ѵ�
        transform.position -= Vector3.right * 200;
        //���� ȸ������ �����Ѵ�
        transform.rotation = Quaternion.Euler(0, 0, 20);
        //���� ũ�Ⱚ�� �����Ѵ�
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //�θ� ������Ʈ�� �����ִٸ�
        if(parentObj.activeSelf)
        {
            //���� ��ġ�� ���ƿ´�
            transform.position = Vector3.Lerp(transform.position, originPos, 10 * Time.deltaTime);
            //���� ȸ�������� ���ƿ´�
            transform.rotation = Quaternion.Lerp(transform.rotation, originRot, 8 * Time.deltaTime);
            //���� ũ�Ⱚ���� ���ƿ´� 
            transform.localScale = Vector3.Lerp(transform.localScale, originScale, 20 * Time.deltaTime);
        }
    }

}
