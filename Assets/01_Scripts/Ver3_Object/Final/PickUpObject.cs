using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

//���� �� �ִ� ������Ʈ
//�Ÿ���� �� ���� �� ����
//���� ����
//������ ���콺 Ŭ�� �� �κ��丮 ����
//�����̵� ����
//�κ��丮 ���� �� UI ����
[RequireComponent(typeof(Rigidbody))]
public class PickUpObject : MonoBehaviourPun, IObjectData
{
    [Header("Ű������Ʈ")]
    public bool hintOne;
    public bool key;
    public bool finalKey;

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

    float finalDistance;

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
        //transform.rotation = Quaternion.identity;
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

    //Lerp�̵� [���ձ� �ݴ�� ����]
    // 1. ī�޶�~�չ������� Ray�� ���� �ε��� �������� �Ÿ�
    // 2. ī�޶�~objectGrabPointTransform.position���� �Ÿ�
    // 1�� 2�� ª�� �Ÿ��� �ش��ϴ� //�̵���

   

    private void FixedUpdate()
    {
        //�̵��� ��ġ�� �ִٸ�
        if (objectGrabPointTransform != null)
        {
            #region 1
            /*//�̵��� ��ġ�� ī�޶��� �Ÿ� ���ϱ�
            float distance = Vector3.Distance(objectGrabPointTransform.position, Camera.main.transform.position);

            //ó�� �Ÿ� ���� //objectGrabPointTransform;
            Vector3 savePos = objectGrabPointTransform.position;

            //�̵��� ��ġ���� ī�޶��� �������� �Ÿ���ŭ�� ���̽��
            Debug.DrawRay(objectGrabPointTransform.position, Camera.main.transform.forward * -distance, Color.green);

            //�÷��̾� ���̾ �����ϰ� �浹 üũ
            int layerMask = ((1 << LayerMask.NameToLayer("Player")));
            layerMask = ~layerMask;

            //�÷��̾� ���� ���� ��ġ�� ī�޶� ������ ���� ��ü�� �ִ��� Ȯ��
            if (Physics.Linecast(transform.position, Camera.main.transform.position, out RaycastHit hit, layerMask))
            {
                //������
                //finalDistance = Mathf.Clamp(hit.distance, 0, 10);

                finalDistance = hit.distance;
                Debug.Log($"������ü {hit.collider.name} �Ÿ��� {hit.distance}");
            }
            else
            {
                //������
                finalDistance = 0;
                Debug.Log($"���� �ʾƾ���");
            }

            //������ ��ġ�� = ������Ʈ ��ġ�������� ī�޶� �������� �����Ÿ���ŭ �̵��� ��ġ�� ���� ��ġ�� ����
            //Vector3 finalPosition = objectGrabPointTransform.position + -Camera.main.transform.forward * finalDistance;
            Vector3 finalPosition = savePos + -Camera.main.transform.forward * finalDistance;

            //Vector3 newPosition = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * lerpSpeed);
            transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * lerpSpeed);

            //Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // MovePosition�� ������ٵ� ����(rigidbody interpolation)�� Ȱ��ȭ �� ������, �� ������ ������ ���̿����� �ڿ������� �̵��� ���� �� ����
            // ���� ����) ���� zero�� ������ �ڲ� ������ 
            //rb.MovePosition(newPosition);*/
            #endregion

            //�̵��� ��ġ�� ī�޶��� �Ÿ� ���ϱ�
            float distance = Vector3.Distance(objectGrabPointTransform.position, Camera.main.transform.position);

            //ó�� �Ÿ� ���� //objectGrabPointTransform;
            Vector3 savePos = objectGrabPointTransform.position;

            //�̵��� ��ġ���� ī�޶��� �������� �Ÿ���ŭ�� ���̽��
            Debug.DrawRay(objectGrabPointTransform.position, Camera.main.transform.forward * -distance, Color.green);

            //�÷��̾� ���̾ �����ϰ� �浹 üũ
            int layerMask = ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Pickable")));
            layerMask = ~layerMask;

            //�÷��̾� ���� ���� ��ġ�� ī�޶� ������ ���� ��ü�� �ִ��� Ȯ��
            if (Physics.Linecast(savePos, Camera.main.transform.position, out RaycastHit hit, layerMask))
            {
                //������
                //finalDistance = Mathf.Clamp(hit.distance, 0, 10);

                //finalDistance = Mathf.Clamp(hit.distance * 0.2f, 0, 5); ;

                //finalDistance = Mathf.Clamp(hit.distance * 2, 1, 10);
                finalDistance = Mathf.Clamp(hit.distance * 2, 1, 3);
                //finalDistance = hit.distance * 2;

                //Debug.Log($"������ü {hit.collider.name} �Ÿ��� {hit.distance}");
            }
            else
            {
                //������
                finalDistance = 0;
                //Debug.Log($"���� �ʾƾ���");
            }

            //������ ��ġ�� = ������Ʈ ��ġ�������� ī�޶� �������� �����Ÿ���ŭ �̵��� ��ġ�� ���� ��ġ�� ����
            //Vector3 finalPosition = objectGrabPointTransform.position + -Camera.main.transform.forward * finalDistance;
            Vector3 finalPosition = savePos + -Camera.main.transform.forward * finalDistance;

            //Vector3 newPosition = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * lerpSpeed);
            //moveObject.transform.position = Vector3.Lerp(moveObject.transform.position, finalPosition, Time.deltaTime * lerpSpeed);

            //transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * lerpSpeed);
            Vector3 newPosition = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * lerpSpeed);


            //Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

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
