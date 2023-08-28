using Photon.Pun;
using UnityEngine;

public class PlayerPickable : MonoBehaviourPun
{
    //ī�޶� ��ġ -> �÷��̾ �ٶ󺸰� �ִ� �������� �ؾ���.
    [Header("ī�޶���ġ")]
    [SerializeField] private Transform playerCameraTransform;

    //float �Ǵ� int ������ Ư�� �ּҰ����� �����ϴ� �� ���Ǵ� Ư�� (1 ���ϴ� �� �� ����)
    [Header("Ray����")]
    [SerializeField] [Min(1)] private float hitRange = 3;

    [Header("������ü")]
    [SerializeField] private GameObject inHandItem;

    [Header("����� �� ��ġ")] //Ray���̶� ����
    [SerializeField] Transform objectGrabPointTransform;
    [SerializeField] Transform picUpslot;

    Transform TestGrabPoint;

    //ó�� ���� ����Ʈ
    //�ִ� ���� // �ּұ���

    //���� ��ü ���� (���� ��ü�� ���� �ٸ��� �����ϸ� -> ��ȣ�ۿ� ����) å, ��ư, ��Ʈ ���
    private RaycastHit hit;

    //��ӹ޾Ƽ� �� �� �ְ� �����
    //���� ��ü�� �����ϱ� ������
    //ObjectMove pickableItem;
    //IObjectData objectdata;

    PickUpObject pickUpObject;
    DragObject dragObject;

    bool iskeydrag;

    private void KeyCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Click();
        }

        if (Input.GetMouseButton(0))
        {
            PickUp(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop(true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Use();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop(false);
        }
    }

    //�̺�Ʈ ȿ��
    private void Use()
    {
        if (inHandItem != null)
        {
            //�տ� �ִ� �������� �ִٸ�.
            //��ü�� �տ� �ִ� ������ �������� ������ҿ� ������ Ȯ��
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                //���� ���ӿ�����Ʈ ����
                usable.Use(this.gameObject);
            }
        }
    }

    //����
    private void Drop(bool key)
    {
        // ��� �ִ� ��ü�� �ִٸ�
        if (inHandItem != null)
        {
            //�� �� �θ� ������ ������ ����.
            //inHandItem.transform.SetParent(null); -> �θ� ���� �ʿ� ����.
  

            if (key)
            {

                if (dragObject != null)
                {
                    inHandItem = null;
                    dragObject.Drop();

                    UIManager.instance.ResetUI();
                }

            }

            else
            {
                if (pickUpObject != null)
                {
                    inHandItem = null;
                    pickUpObject.Drop();

                    UIManager.instance.ResetUI();
                }
                   
            }


            //objectdata = null;
        }
    }

    //������� �� ��ġ�� ���� �Ѱ��ְ�
    //�̵��� �� �ְ�
    private void PickUp(bool key)
    {
        if (hit.collider != null)
        {
            Debug.Log($"���� ������Ʈ {hit.collider.name}");
        }

        // ���� ��ü �ִµ� ��� �ִ� ��ü�� ���ٸ�
        if (hit.collider != null && inHandItem == null)
        {
            //drage
            if (key)
            {
                dragObject = hit.collider.GetComponent<DragObject>();

                if (dragObject != null)
                {
                    Debug.Log($"�巡�� ������Ʈ {hit.collider.name}");

                    // �� ���� �� �Ҹ� ����

                    // �տ� �� ��ü�� ���� (������� ������ ������)
                    // ���� �� �������� �Ѱ��ش�. (�׷��� ������ ���常 true�� �Ǳ� ������ ����ȭ ������ ��)
                    inHandItem = dragObject.PickUp(photonView.Owner);

                    //���� hit�� �Ÿ��� z ������ �Ǿ���
                    float distance = Vector3.Distance(objectGrabPointTransform.position, inHandItem.transform.position);
                    Debug.Log("�Ÿ���" + distance);
                    //�ڽ��� �Ѱ��ְ�

                    objectGrabPointTransform.position = hit.point;

                    //������Ʈ ��ü���� �̵��� �� �ְ� (ī�޶� �ڽ� ��ġ �Ѱ��ֱ�)
                    //��ü���� �ٸ� ī�޶��� ��ġ�� �Ѱ��شٸ�?
                    dragObject.Grab(objectGrabPointTransform);
                }

            }

            //pick
            else
            {
                pickUpObject = hit.collider.GetComponent<PickUpObject>();

                if (pickUpObject != null)
                {
                    Debug.Log($"�巡�� ������Ʈ {hit.collider.name}");

                    // �� ���� �� �Ҹ� ����

                    // �տ� �� ��ü�� ���� (������� ������ ������)
                    // ���� �� �������� �Ѱ��ش�. (�׷��� ������ ���常 true�� �Ǳ� ������ ����ȭ ������ ��)
                    inHandItem = pickUpObject.PickUp(photonView.Owner);

                    //���� hit�� �Ÿ��� z ������ �Ǿ���
                    float distance = Vector3.Distance(objectGrabPointTransform.position, inHandItem.transform.position);
                    Debug.Log("�Ÿ���" + distance);
                    //�ڽ��� �Ѱ��ְ�

                    objectGrabPointTransform.position = hit.point;

                    //������Ʈ ��ü���� �̵��� �� �ְ� (ī�޶� �ڽ� ��ġ �Ѱ��ֱ�)
                    //��ü���� �ٸ� ī�޶��� ��ġ�� �Ѱ��شٸ�?
                    pickUpObject.Grab(objectGrabPointTransform);
                }
            }


        }
    }

    void Click()
    {
        //X�� �̵� ����
        if (hit.collider.GetComponent<OpenDrawer>())
        {
            OpenDrawer openDrawer = hit.collider.GetComponent<OpenDrawer>();

            if (openDrawer != null)
            {
                // �� ���� �� �Ҹ� ����

                //openDrawer.isOpen = !openDrawer.isOpen;
                //�ڽ��� ����並 �����´�.
                var pv = hit.collider.GetComponent<PhotonView>();

                //����䰡 �ִٸ�
                if (pv != null)
                {
                    //������� DoorAction�� �����Ų��.
                    //�Ű����� ���� ���� �� ����.
                    pv.RPC("DoorAction", RpcTarget.All, 1);
                }
                else
                {
                    print("����䰡 �����");
                }
            }
        }

        //Y�� �� ȸ��
        //������ ���� 
        if (hit.collider.GetComponent<OpenDoor>())
        {
            OpenDoor openDoor = hit.collider.GetComponent<OpenDoor>();

            if (openDoor != null)
            {
                var pv = hit.collider.GetComponent<PhotonView>();

                //����䰡 �ִٸ�
                if (pv != null)
                {
                    //������� DoorAction�� �����Ų��.
                    //�Ű����� ���� ���� �� ����.
                    pv.RPC("OpenDoorAction", RpcTarget.All, 1);
                }
                else
                {
                    print("����䰡 �����");
                }
            }
        }

        if (hit.collider.GetComponent<TokenObject>())
        {
            TokenObject token = hit.collider.GetComponent<TokenObject>();

            token.Sorce();
        }
    }

    private void Update()
    {
        // ���� ���� �� �� �ְ�
        if (photonView.IsMine)
        {
            KeyCheck();

            //���϶����ε� �� �Ǵ� ����?
            RayCheck();

            //GrabPos();
        }
    }

    //UI ���� (���� �� �ִ� ������Ʈ Ȯ��)
    private void RayCheck()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // ���� ������Ʈ�� ���� ��
        if (hit.collider != null)
        {
            //�⺻ ����
            UIManager.instance.BaseUI();

            Outline outLine = hit.collider.GetComponent<Outline>();
            if (outLine != null)
            {
                outLine.OutlineWidth = 0;
            }
            //?. null�� �ƴ��� ���� Ȯ�� / null�� �ƴ϶�� ToggleHighlight(false)�� ����(���� �� �ִ� ����)
            //hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(false);
        }

        //�տ� �������� �ִٸ� ���� ���� �ʴ´�.
        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange))
        {
            //hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);

            Outline outLine = hit.collider.GetComponent<Outline>();
            if (outLine != null)
            {
                outLine.OutlineWidth = 6;
            }

            //�±׸� ����
            if (hit.collider.CompareTag("DragObj"))
            {
                UIManager.instance.ResetUI();
                UIManager.instance.dragUI.SetActive(true);
            }

            else if (hit.collider.CompareTag("PickUpObj"))
            {
                UIManager.instance.ResetUI();
                UIManager.instance.pickUpUI.SetActive(true);
            }

            else if (hit.collider.GetComponent<OpenHighlight>())
            {
                UIManager.instance.ResetUI();
                UIManager.instance.opneUI.SetActive(true);
            }

            //else if (hit.collider.C)
        }
    }
}