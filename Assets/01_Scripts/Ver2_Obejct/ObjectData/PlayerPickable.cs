using Photon.Pun;
using UnityEngine;

public class PlayerPickable : MonoBehaviourPun
{
    //ī�޶� ��ġ -> �÷��̾ �ٶ󺸰� �ִ� �������� �ؾ���.
    [Header("ī�޶���ġ")]
    [SerializeField] private Transform playerCameraTransform;

    //float �Ǵ� int ������ Ư�� �ּҰ����� �����ϴ� �� ���Ǵ� Ư�� (1 ���ϴ� �� �� ����) 
    [Header("Ray����")] //�Ǻ��� ���� ����
    [SerializeField] [Min(1)] private float hitRange = 3;

    [Header("������ü")]
    [SerializeField] private GameObject inHandItem;

    [Header("����� �� ��ġ")] //Ray���̶� ����(���� ��ü�� �ִ� ��ġ)
    [SerializeField] Transform objectGrabPointTransform;

    //���� ��ü ���� (���� ��ü�� ���� �ٸ��� �����ϸ� -> ��ȣ�ۿ� ����) å, ��ư, ��Ʈ ���
    private RaycastHit hit;

    //��ӹ޾Ƽ� �� �� �ְ� ����� (���� ��ü�� �����ϱ� ������)
    PickUpObject pickUpObject;
    DragObject dragObject;

    private void Update()
    {
        // ���� ���� �� �� �ְ�
        if (photonView.IsMine)
        {
            Debug.Log(PhotonNetwork.NickName + "�� �����ϴ� �ڵ��Դϴ�");

            //���϶����ε� �� �Ǵ� ����?
            RayCheck();

            KeyCheck();
        }
    }

    private void KeyCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //������Ʈ ������ / ��ū
            Click();
        }

        // ������Ʈ ��� , �̵�
        // ���콺 �巡�� �̵�
        if (Input.GetMouseButton(0))
        {
            PickUp(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop(true);
        }

        // �ڵ� �̵�
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop(false);
        }
    }

    void Click()
    {
        //���� ��ü�� ���ٸ� �ڵ�������� �ʴ´�.
        if (hit.collider == null)
        {
            return;
        }

        //X�� �� ȸ��
        if(hit.collider.GetComponent<OpenDrawer>())
        {
            //�ڽ��� ����並 �����´�.
            var pv = hit.collider.GetComponent<PhotonView>();

            //����䰡 �ִٸ�
            if (pv != null)
            {
                //������� DoorAction�� �����Ų��.
                //�Ű����� ���� ���� �� ����. (������� �ʾƵ� �ʿ�)
                pv.RPC("DoorAction", RpcTarget.All , 1);
            }
            else
            {
                print("X�� ��ȸ�� ����䰡 �����ϴ�");
            }
        }

        //Y�� �� ȸ��
        if (hit.collider.GetComponent<OpenDoor>())
        {
            var pv = hit.collider.GetComponent<PhotonView>();

            if (pv != null)
            { 
                pv.RPC("OpenDoorAction", RpcTarget.All, 1);
            }
            else
            {
                print("Y�� ��ȸ�� ����䰡 �����ϴ�");
            }
        }

        //��ūȹ�� �� �����߰�
        if (hit.collider.GetComponent<TokenObject>())
        {
            TokenObject token = hit.collider.GetComponent<TokenObject>();

            token.Sorce();
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
        // �� ��� �������� ����並 �Ѱ����� �� inHandItem�� null�� �Ǿ�� �Ѵ�.
        if (hit.collider != null && inHandItem == null)
        {
            //drage
            if (key)
            {
                dragObject = hit.collider.GetComponent<DragObject>();

                if (dragObject != null)
                {
                    Debug.Log($"�巡�� ������Ʈ {hit.collider.name}");

                    // �տ� �� ��ü�� ���� (������� ������ ������)
                    // ���� �� �������� �Ѱ��ش�. (�׷��� ������ ���常 true�� �Ǳ� ������ ����ȭ ������ ��
                    inHandItem = dragObject.PickUp(photonView.Owner);

                    //������Ʈ�� ��ġ�� ���� ������ ��ġ�� ����
                    objectGrabPointTransform.position = hit.point;

                    //������Ʈ ��ü���� �̵��� �� �ְ� (ī�޶� �ڽ� ��ġ �Ѱ��ֱ�)
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

                    // �տ� �� ��ü�� ���� (������� ������ ������)
                    // ���� �� �������� �Ѱ��ش�. (�׷��� ������ ���常 true�� �Ǳ� ������ ����ȭ ������ ��)
                    inHandItem = pickUpObject.PickUp(photonView.Owner);

                    objectGrabPointTransform.position = hit.point;

                    //������Ʈ ��ü���� �̵��� �� �ְ� (ī�޶� �ڽ� ��ġ �Ѱ��ֱ�)
                    pickUpObject.Grab(objectGrabPointTransform);
                }
            }
        }
    }

    //����
    private void Drop(bool key)
    {
        // ��� �ִ� ��ü�� �ִٸ�
        if (inHandItem != null)
        {
            if (key)
            {
                //�� �� �θ� ������ ������ ����.
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
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(false);
        }

        //�տ� �������� �ִٸ� ���� ���� �ʴ´�.
        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange))
        {
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);

            Outline outLine = hit.collider.GetComponent<Outline>();

            if (outLine != null)
            {
                outLine.OutlineWidth = 6;
            }

            //�±׸� ���� UI ����
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

            else if (hit.collider.CompareTag("Token"))
            {
                UIManager.instance.ResetUI();
                UIManager.instance.tokenUI.SetActive(true);
            }
        }
    }
}