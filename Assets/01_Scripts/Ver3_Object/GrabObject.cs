using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//EX �ٳ���
public class GrabObject : MonoBehaviourPun
{
    [Header("������ġ����")]
    public bool isKeepWorldPosition;

    public bool isKey;

    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;

    //���� ������ ������
    bool ishiddenObject = false;

    //�̵�����ġ
    //Transform objectGrabPointTransform;

    //������ �ٵ� �ʿ�
    public Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [PunRPC]
    public void OnOff(bool on)
    {
        if (rb != null)
        {
            rb.isKinematic = on;
        }
    }

    #region �÷��̾�� ���� ����� �� �� �ѱ�� (���� �������� �ޱ�)
    public GameObject PickUp(Player owner)
    {
        photonView.TransferOwnership(owner);

        photonView.RPC(nameof(OnOff), RpcTarget.All, true);

        //Ű ���ǹ� -> ���ȿ� �ִ� Ű�� �Ű����� ũ�⸦ �����ϰ� �����ϱ� ����
        //�ڽ� -> �ٸ� �θ��� �ڽ� -> ����
        //�� ��ü������ ������ǥ�� �ȴ�. �ٸ��� ������ �� �� ����.
        if (isKey)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = transform.lossyScale;
        }

        //Ű �ƴҶ�
        //�ڽ� -> �ٸ� �θ��� �ڽ�
        else
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        return this.gameObject;
    }
    #endregion

    #region ��� / ���� ���� �� ���� ��
    public void Grab(Transform objectGrabPointTransform)
    {
        ishiddenObject = false;
        //this.objectGrabPointTransform = objectGrabPointTransform;
    }

    //������ �� -> �̵��� ��ġ ���ֱ�
    public void Drop()
    {
        photonView.RPC(nameof(OnOff), RpcTarget.All, false);
        // this.objectGrabPointTransform = null;

    }
    #endregion

    //������ �ٵ� �̵�
    private void Update()
    {
        if (ishiddenObject)
        {
            //�� ������ �ٵ� �̵������ϸ� ��鸮�� ���� �Ͼ. ��?
            transform.position = contactPlatform.transform.position - distance;
        }
    }

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

            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            ishiddenObject = true;
        }
    }

    //������ ����ٴ��� �ʰ�
    private void OnTriggerExit(Collider other)
    {
        ishiddenObject = false;
    }
    #endregion 
}
