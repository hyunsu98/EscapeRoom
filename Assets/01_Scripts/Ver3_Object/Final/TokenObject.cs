using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��ū�� ������ �ٷ� ���� �߰�
//��ο��� �˷��ֱ�
public class TokenObject : MonoBehaviourPun
{
    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    //���� ������ ������
    bool ishiddenObject = false;

    public void Sorce()
    {
        photonView.RPC(nameof(Add), RpcTarget.All, 1);
    }

    [PunRPC]
    public void Add(int i)
    {
        //���� �߰�
        GameManager.instance.UpdateToken(i);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (ishiddenObject)
        {
            //�� ������ �ٵ� �̵������ϸ� ��鸮�� ���� �Ͼ. ��?
            transform.position = contactPlatform.transform.position - distance;
        }
    }

    //���� �ȿ� ���� �� ���� ������ �˷��ְ� �̵��� �� �ְ�
    private void OnTriggerEnter(Collider other)
    {
        //�浹 �ߴµ� �� ������Ʈ�� �������� �Ÿ�
        if (other.gameObject.CompareTag("HiddenObject"))
        {
           
            //�浹�� ������Ʈ�� ��ġ�� �� ��ġ�� ���� �ض�.
            Debug.Log($"���� �ȿ� �ִ� ������Ʈ {other.gameObject}");
            contactPlatform = other.gameObject;

            //���� ���ذ� �Ȱ��� �� ����.
            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }
}
