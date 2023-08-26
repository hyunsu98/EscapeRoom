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

    //ó�� ���� ����Ʈ
    //�ִ� ���� // �ּұ���
    

    //���� ��ü ���� (���� ��ü�� ���� �ٸ��� �����ϸ� -> ��ȣ�ۿ� ����) å, ��ư, ��Ʈ ���
    private RaycastHit hit;

    //��ӹ޾Ƽ� �� �� �ְ� �����
    //���� ��ü�� �����ϱ� ������
    //ObjectMove pickableItem;
    IObjectData objectdata;

    private void KeyCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("����");
        }

        if (Input.GetMouseButton(0))
        {
            //PickUp();
            print("������ ��");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop();
            print("��");
        }

        if (Input.GetMouseButtonDown(1))
        {
            Use();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
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
    private void Drop()
    {
        // ��� �ִ� ��ü�� �ִٸ�
        if (inHandItem != null)
        {
            //�� �� �θ� ������ ������ ����.
            //inHandItem.transform.SetParent(null); -> �θ� ���� �ʿ� ����.
            inHandItem = null;

            objectdata.Drop();

            UIManager.instance.ResetUI();
            //objectdata = null;
        }
    }

    //������� �� ��ġ�� ���� �Ѱ��ְ�
    //�̵��� �� �ְ�
    private void PickUp()
    {
        if (hit.collider != null)
        {
            Debug.Log($"���� ������Ʈ {hit.collider.name}");
        }

        // ���� ��ü �ִµ� ��� �ִ� ��ü�� ���ٸ�
        if (hit.collider != null && inHandItem == null)
        {
            objectdata = hit.collider.GetComponent<IObjectData>();

            if (objectdata != null)
            {
                Debug.Log($"�巡�� ������Ʈ {hit.collider.name}");

                // �� ���� �� �Ҹ� ����

                // �տ� �� ��ü�� ���� (������� ������ ������)
                // ���� �� �������� �Ѱ��ش�. (�׷��� ������ ���常 true�� �Ǳ� ������ ����ȭ ������ ��)
                inHandItem = objectdata.PickUp(photonView.Owner);

                //������Ʈ ��ü���� �̵��� �� �ְ� (ī�޶� �ڽ� ��ġ �Ѱ��ֱ�)
                //��ü���� �ٸ� ī�޶��� ��ġ�� �Ѱ��شٸ�?
                objectdata.Grab(objectGrabPointTransform);
                // Ű ��ü���� ������ ����
                // ������� bool ���� true ��� -> Ű�� ����
                // �׷� ���� ������Ʈ�� Ű�� ã�Ҵٰ� �˷��ְ�
                // Ű ������Ʈ�� �ٸ� �ش� ������Ʈ�� ������ ���� ������ �� ����
            }

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

                    /*//���� ������Ʈ���� �̼��� Ǯ������ ���� �� �ְ� �� ����.
                    //������ ���� ��Ҵµ� ���� Key�� ������ �ְ�, Ű�� ��� �ִٸ� �� ������
                    //������ ���� ����� ���� Ű�� �ִٸ� 
                    if (GameManager.instance.Mission1 == true)
                    {
                        // �� ���� �� �Ҹ� ����

                        //�� ����
                        openDoor.isOpen = !openDoor.isOpen;

                        if (openDoor.isOpen == true)
                        {
                            //GameManager.instance.Mission2 = true;
                            //photonView.RPC(nameof(Check), RpcTarget.All, 1);
                        }
                    }
                    else
                    {
                        Debug.Log("�̼�1��ǰ");
                    }*/
                }
            }

            if (hit.collider.GetComponent<TokenObject>())
            {
                TokenObject token = hit.collider.GetComponent<TokenObject>();

                token.Sorce();
            }

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

            //?. null�� �ƴ��� ���� Ȯ�� / null�� �ƴ϶�� ToggleHighlight(false)�� ����(���� �� �ִ� ����)
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
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
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);

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
        }
    }

    //�� ���� �ٽ� �ؾ� ��
    /*[PunRPC]
    public void Check(bool isCk)
    {
        GameManager.instance.Mission2 = true;
    }*/
}