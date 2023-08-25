using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//���� �� �ִ� ������Ʈ
//�Ÿ���� �� ���� �� ����
//���� ����
//������ ���콺 Ŭ�� �� �κ��丮 ����
//�����̵� ����
//�κ��丮 ���� �� UI ����
public class PickUpObject : MonoBehaviourPun, IObjectData
{
    [Header("Ű������Ʈ")]
    public bool key;

    public bool mission;

    [Header("�̵��ӵ�")]
    public float lerpSpeed = 10;

    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    //���� ������ ������
    bool ishiddenObject = false;

    //�̵�����ġ
    Transform objectGrabPointTransform;

    //������ �ٵ� �ʿ�
    Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //RPC�� ���� A��ü�� ����� �� ��� ������� A��ü �߷� ������
    [PunRPC]
    public void OnOff(bool on)
    {
        if (rb != null)
        {
            rb.isKinematic = on;
        }
    }

    [PunRPC]
    //��� �������׵� �����ٰ� �˷���� ��.
    public void OnExit(bool exit)
    {
        ishiddenObject = exit;
    }

    public GameObject PickUp(Player owner)
    {
        //��ü�� ������ �Ű����������� �ٲ۴�.
        //���� ������ �����ϱ� (Takeover)
        if (photonView != null)
        {
            photonView.TransferOwnership(owner);

            //�߷� ���� ��� ������� �˷���� ��.
            photonView.RPC(nameof(OnOff), RpcTarget.All, true);
        }
        
        //�ؿ��⼭ ���� �ؾ���!
        //�������� ��ü�̱� ������
        transform.rotation = Quaternion.identity;
        Debug.Log("������ ȹ��");

        if (key)
        {
            //GameManager.instance.KeyEat(true);
            Debug.Log("Ű�� ȹ����");
        }

        return this.gameObject;
    }
    #region ��� / ���� ���� �� ���� ��
    public void Grab(Transform objectGrabPointTransform)
    {
        //������ ����ٴϱ� ���ֱ�
        //ishiddenObject = false;
        //��ü ��� ������ ����
        this.objectGrabPointTransform = objectGrabPointTransform;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;

        if (photonView != null)
        {
            //�߷� ���� ��� ������� �˷���� ��.
            photonView.RPC(nameof(OnOff), RpcTarget.All, false);
        }
    }
    #endregion

    #region �̵� ���
    //������ �ٵ� �̵�
    private void FixedUpdate()
    {
        //�̵��� ��ġ�� �ִٸ�
        if (objectGrabPointTransform != null)
        {
            //Lerp�̵� [���ձ� �ݴ�� ����]
            // 1. ī�޶�~�չ������� Ray�� ���� �ε��� �������� �Ÿ�
            // 2. ī�޶�~objectGrabPointTransform.position���� �Ÿ�
            // 1�� 2�� ª�� �Ÿ��� �ش��ϴ� //�̵���

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // MovePosition�� ������ٵ� ����(rigidbody interpolation)�� Ȱ��ȭ �� ������, �� ������ ������ ���̿����� �ڿ������� �̵��� ���� �� ����
            // ���� ����) ���� zero�� ������ �ڲ� ������ 
            rb.MovePosition(newPosition);
        }

        //������ hiddenObject �����ϸ� �ȵ�.
        else
        {
            if (ishiddenObject)
            {
                //�� ������ �ٵ� �̵������ϸ� ��鸮�� ���� �Ͼ. ��?
                transform.position = contactPlatform.transform.position - distance;
            }
        }
    }
    #endregion

    #region �����ȿ� ���� �� �̵�
    //���� �ȿ� ���� �� ���� ������ �˷��ְ� �̵��� �� �ְ�
    private void OnTriggerEnter(Collider other)
    {
        //�浹 �ߴµ� �� ������Ʈ�� �������� �Ÿ�
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            if (photonView != null)
            {
                //�߷� ���� ��� ������� �˷���� ��.

                Debug.Log("�߷�����");
                photonView.RPC(nameof(OnOff), RpcTarget.All, true);
            }

            //�浹�� ������Ʈ�� ��ġ�� �� ��ġ�� ���� �ض�.
            Debug.Log($"���� �ȿ� �ִ� ������Ʈ {other.gameObject}");
            contactPlatform = other.gameObject;

            //���� ���ذ� �Ȱ��� �� ����.
            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }

    //������ ����ٴ��� �ʰ�
    private void OnTriggerExit(Collider other)
    {
        photonView.RPC(nameof(OnExit), RpcTarget.All, false);
        //ishiddenObject = false;
    }
    #endregion 


}
