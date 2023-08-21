using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectMove : MonoBehaviourPun //IPunObservable
{
    [Header("������ġ����")]
    public bool isKeepWorldPosition;

    [Header("ȹ�������")]
    public bool isEatItem;

    [Header("�̵��ӵ�")]
    public float lerpSpeed = 10;

    //�̵�����ġ"
    Transform objectGrabPointTransform;

    //������ ���� �̵��ϱ� ����
    [Header("�����̵�")]
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;

    //���� ������ ������
    bool ishiddenObject = false;

    //������ �ٵ� �ʿ�
    public Rigidbody rb;

    /// <summary>
    /// ---------------------------------------
    /// </summary>
    //�������� �Ѿ���� ��ġ��
    Vector3 receivePos;
    //�������� �Ѿ���� ȸ����
    Quaternion receiveRot = Quaternion.identity;
    //�����ϴ� �ӷ�
    float endSpeed = 50;

    //����
    float h;
    float v;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [PunRPC]
    public void OnOfff(bool on)
    {
        if (rb != null)
        {
            rb.isKinematic = on;
        }
    }

    //����� �� �߷� ���� ���� �ʰ�
    //���� ��ġ�� �˷��ֱ�
    //using Photon.Realtime; -> Player ����Ϸ��� �ʿ�(�ٸ� ��ũ��Ʈ �̸� ������ �ȵ�)
    public GameObject PickUp(Player owner)
    {
        //��ü�� ������ �Ű����������� �ٲ۴�.
        photonView.TransferOwnership(owner);
        /* if (rb != null)
         {
             rb.isKinematic = true;
         }*/
        photonView.RPC(nameof(OnOfff), RpcTarget.All, true);
        /*if (isEatItem)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Debug.Log("���� �� �ȵ�?");
        }*/

        return this.gameObject;
    }

    #region ���콺 ����Ʈ �ޱ�
    /*Vector3 mousePosition;
    
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
    }*/
    #endregion

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

        /*if (rb != null)
        {
            rb.isKinematic = false;
        }*/
        photonView.RPC(nameof(OnOfff), RpcTarget.All, false);

    }

    //������ �ٵ� �̵�
    private void FixedUpdate()
    {
        /*if(photonView.IsMine)
        {

        }*/
        //�̵��� ��ġ�� �ִٸ�
        if (objectGrabPointTransform != null)
        {
            //Lerp�̵� [���ձ� �ݴ�� ����]
            // 1. ī�޶�~�չ������� Ray�� ���� �ε��� �������� �Ÿ�
            // 2. ī�޶�~objectGrabPointTransform.position���� �Ÿ�
            // 1�� 2�� ª�� �Ÿ��� �ش��ϴ� //�̵���

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // MovePosition�� ������ٵ� ����(rigidbody interpolation)�� Ȱ��ȭ �� ������, �� ������ ������ ���̿����� �ڿ������� �̵��� ���� �� ����
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

    private void Update()
    {

    }

    //���� �ȿ� ���� �� 
    //���� ������ �˷��ְ� �̵��� �� �ְ�
    private void OnTriggerEnter(Collider other)
    {
        //�浹 �ߴµ� �� ������Ʈ�� �������� �Ÿ�
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            //�浹�� ������Ʈ�� ��ġ�� �� ��ġ�� ���� �ض�.
            Debug.Log($"������ ������Ʈ {other.gameObject}");
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

    //[PunRPC]

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //�� Player ���
        if (stream.IsWriting)
        {
            //���� ��ġ���� ������.
            stream.SendNext(transform.position);
            //���� ȸ������ ������.
            stream.SendNext(transform.rotation);
            //h �� ������.
            //stream.SendNext(h);
            //v �� ������.
            //stream.SendNext(v);
        }

        //�� Player �ƴ϶��
        else
        {
            //��ġ���� ����.
            receivePos = (Vector3)stream.ReceiveNext();
            //ȸ������ ����.
            receiveRot = (Quaternion)stream.ReceiveNext();
            //h �� ����.
            //h = (float)stream.ReceiveNext();
            //v �� ����.
            //v = (float)stream.ReceiveNext();
        }
    }
}
