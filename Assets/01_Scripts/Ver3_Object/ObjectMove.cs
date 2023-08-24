using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//EX ���
//������ ������ �����ϰ� ��ü�� ������ ���̴�.
//�� �ʿ��� Ư�� ������Ʈ �ڵ� �߰� -> ������ �ٵ� �ʿ�, Highlight
[RequireComponent(typeof(Rigidbody), typeof(Highlight))]
public class ObjectMove : MonoBehaviourPun
{
    [Header("�̵��ӵ�")]
    public float lerpSpeed = 10;

    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    //���� ������ ������
    bool ishiddenObject = false;

    [Header("Ű������Ʈ")]
    public bool key;

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

    #region �÷��̾�� ���� ����� �� �� �ѱ�� (���� �������� �ޱ�)
    //����� �� �߷� ���� ���� �ʰ�
    //�� ��ġ ��ü��
    //using Photon.Realtime; -> Player ����Ϸ��� �ʿ�(�ٸ� ��ũ��Ʈ �̸� ������ �ȵ�)
    public GameObject PickUp(Player owner)
    {
        //��ü�� ������ �Ű����������� �ٲ۴�.
        //���� ������ �����ϱ� (Takeover)
        if(photonView != null)
        {
            photonView.TransferOwnership(owner);

            //�߷� ���� ��� ������� �˷���� ��.
            photonView.RPC(nameof(OnOff), RpcTarget.All, true);
        }
       
        if(gameObject.CompareTag("PickUpObj"))
        {
            //�������� ��ü�̱� ������
            transform.rotation = Quaternion.identity;

            if(key)
            {
                GameManager.instance.KeyEat(true);
                Debug.Log("Ű�� ȹ��");
            }
        }

        return this.gameObject;
    }
    #endregion

    #region ��� / ���� ���� �� ���� ��
    //�̵��� ��ġ �ޱ�
    public void Grab(Transform objectGrabPointTransform)
    {
        //������ ����ٴϱ� ���ֱ�
        ishiddenObject = false;
        //��ü ��� ������ ����
        this.objectGrabPointTransform = objectGrabPointTransform;
    }

    //������ �� -> �̵��� ��ġ ���ֱ�
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
    //���� �ȿ� ���� �� 
    //���� ������ �˷��ְ� �̵��� �� �ְ�
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

            //photonView.RPC(nameof(HiddenCheck), RpcTarget.All, true);
        }
    }

    //������ ����ٴ��� �ʰ�
    private void OnTriggerExit(Collider other)
    {
        ishiddenObject = false;
        
        //photonView.RPC(nameof(HiddenCheck), RpcTarget.All, false);
    }

    //�������� ��뿡�Ե� �˷������
    /*[PunRPC]
    public void HiddenCheck(bool isHidden)
    {
        ishiddenObject = isHidden;
    }*/
    #endregion 
}
