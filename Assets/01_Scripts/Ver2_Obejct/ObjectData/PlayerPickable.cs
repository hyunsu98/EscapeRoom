using UnityEngine;

public class PlayerPickable : MonoBehaviour
{
    //���� ������ ���̾� ����ũ ���� -> �浹ü ����
    [Header("�浹���ɿ�����Ʈ")]
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private LayerMask openLayerMask;
    [SerializeField] private LayerMask openKeyLayerMask;

    //ī�޶� ��ġ -> �÷��̾ �ٶ󺸰� �ִ� �������� �ؾ���.
    [Header("ī�޶���ġ")]
    [SerializeField] private Transform playerCameraTransform;

    //���콺 UI (���� ������ EŰ�� ���������� ���� -> ���콺�� �巡�� �ϴ� ������ �� ����)
    //�� �����ö� / �̵��Ҷ� / �ƹ��͵� �ȵɶ� UI �ٸ��� ����
    [Header("���콺UI")]
    [SerializeField] private GameObject pickUpUI;

    //float �Ǵ� int ������ Ư�� �ּҰ����� �����ϴ� �� ���Ǵ� Ư�� (1 ���ϴ� �� �� ����)
    [Header("Ray����")]
    [SerializeField] [Min(1)] private float hitRange = 3;

    //����� �� ��ġ (�տ� ���� �� �ִ�) -> isMain ȭ�� ���� , �ƴϸ� �ִϸ��̼� �տ�
    [Header("EatItemPos")]
    [SerializeField] private Transform pickUpParent;

    [Header("������ü")]
    [SerializeField] private GameObject inHandItem;

    [Header("����� �� ��ġ")] //Ray���̶� ����
    [SerializeField] Transform objectGrabPointTransform;

    [Header("Sound")] //����Ŵ����� ����
    [SerializeField] private AudioSource pickUpSource;

    //InputSystem�� ����� Ű �Է� (using UnityEngine.LnputSystem; �ʿ�)
    //[SerializeField] private InputActionReference interactionInput, dropInput, useInput;

    //���� ��ü ���� (���� ��ü�� ���� �ٸ��� �����ϸ� -> ��ȣ�ۿ� ����) å, ��ư, ��Ʈ ���
    private RaycastHit hit;

    //��ӹ޾Ƽ� �� �� �ְ� �����
    ObjectMove pickableItem;
    GrabObject grabObject;
    OpenDrawer openDrawer;
    OpenDoor openDoor;


    private Vector3 initialLocalScale;
    private Vector3 initialGlobalScale;

    #region Ű���� Ver01
    /*private void Start()
    {
        //�����ֱ⸸ �ϸ��
        //if(Input.GetKeyDown(KeyCode.K)) �� ���� �����
        //Ű ���� �� �޼ҵ� ȣ��
        interactionInput.action.performed += PickUp; //e��ư
        dropInput.action.performed += Drop; //q��ư
        useInput.action.performed += Use;
    }

    //�Ű������� ������� ���� ����.
    private void Use(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            //�տ� �ִ� �������� �ִٸ�.
            //��ü���տ� �ִ� ������ �������� ������ҿ� ������ Ȯ��
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                //���� ���ӿ�����Ʈ ����
                usable.Use(this.gameObject); //->hit.collider.GetComponent<Food>() �� ���� ������ Ȯ���� �ʿ䰡 ����
            }
        }
    }

    //����
    private void Drop(InputAction.CallbackContext obj)
    {
        // ��� �ִ� ��ü�� �ִٸ�
        if (inHandItem != null)
        {
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void PickUp(InputAction.CallbackContext obj)
    {
        if (hit.collider != null)
        {
            Debug.Log($"������ü {hit.collider.name}");
        }

        // ���� ��ü�� �ְ� �տ� �� ���� ������
        // ���� �ִ� ���¿��� e ������ �ƹ� �ϵ� �Ͼ�� �ʴ´�.
        if (hit.collider != null && inHandItem == null)
        {
            //����
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                pickUpSource.Play();
                // �տ� �� �����۰� ������ �� �ִ� ��������  �����ϰ� �Ҵ�
                inHandItem = pickableItem.PickUp();
                //bool ������ �Ѱ��ֱ�
                //false�϶� 
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
            }

            #region ���1 -> ���� ������ ���� ����. -> IUsable ����
            *//*Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            //Food��ũ��Ʈ�� ������ �ִٸ� Weapon
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Weapon>())
            {
                //�����̶�� �վ����� ���� ��ġ
                Debug.Log("It's food!");
                inHandItem = hit.collider.gameObject;
                //ȭ�� ������ ���� ���� ����
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                //������Ʈ�� �ٸ� ������Ʈ ������ ���� SetParennt �޼ҵ� ���
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                //������ �ٵ� ���ֱ�
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }

            if (hit.collider.GetComponent<Item>())
            {
                Debug.Log("It's a useless item!");
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.SetParent(pickUpParent.transform, true);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }*//*
            #endregion
        }
    }*/

    #endregion

    #region Ű���� Ver02

    //�̺�Ʈ ȿ��
    private void Use()
    {
        if (inHandItem != null)
        {
            //�տ� �ִ� �������� �ִٸ�.
            //��ü���տ� �ִ� ������ �������� ������ҿ� ������ Ȯ��
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                //���� ���ӿ�����Ʈ ����
                usable.Use(this.gameObject); //->hit.collider.GetComponent<Food>() �� ���� ������ Ȯ���� �ʿ䰡 ����
            }
        }
    }

    //����
    private void Drop()
    {
        // ��� �ִ� ��ü�� �ִٸ�
        if (inHandItem != null)
        {
            if (hit.collider.GetComponent<ObjectMove>())
            {
                inHandItem.transform.SetParent(null);
                inHandItem = null;

                pickableItem.Drop();
                pickableItem = null;
            }

        }
    }
    //����
    private void Drop2()
    {
        // ��� �ִ� ��ü�� �ִٸ�
        if (inHandItem != null)
        {
            if (hit.collider.GetComponent<GrabObject>())
            {
                // �θ𿡼� �и��� �� ���� �������� ����
                Vector3 localScale = DivideVector3(inHandItem.transform.lossyScale, inHandItem.transform.parent.lossyScale);

                inHandItem.transform.SetParent(null, true);

                inHandItem.transform.localScale = localScale;
                inHandItem = null;

                grabObject.Drop();
                grabObject = null;
            }

        }
    }


    //������� �� ��ġ�� ���� �Ѱ��ְ�
    //�̵��� �� �ְ�
    private void PickUp()
    {
        if (hit.collider != null)
        {
            Debug.Log($"������ü {hit.collider.name}");
        }

        // ���� ��ü�� �ְ� �տ� �� ���� ������
        // ���� �ִ� ���¿��� e ������ �ƹ� �ϵ� �Ͼ�� �ʴ´�.
        if (hit.collider != null && inHandItem == null)
        {
            if (hit.collider.GetComponent<ObjectMove>())
            {
                pickableItem = hit.collider.GetComponent<ObjectMove>();

                if (pickableItem != null)
                {
                    //���� �� �Ҹ�
                    pickUpSource.Play();

                    // �տ� �� �����۰� ������ �� �ִ� �������� �����ϰ� �Ҵ�
                    inHandItem = pickableItem.PickUp();

                    if (isMouse)
                    {
                        //2.������Ʈ ��ü���� �̵��� �� �ְ� (ī�޶� �ڽ� ��ġ �Ѱ��ֱ�)
                        pickableItem.Grab(objectGrabPointTransform);
                    }
                }
            }


            #region ���1 -> ���� ������ ���� ����. -> IPickable ����
            /*Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            //Food��ũ��Ʈ�� ������ �ִٸ� Weapon
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Weapon>())
            {
                //�����̶�� �վ����� ���� ��ġ
                Debug.Log("It's food!");
                inHandItem = hit.collider.gameObject;
                //ȭ�� ������ ���� ���� ����
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                //������Ʈ�� �ٸ� ������Ʈ ������ ���� SetParennt �޼ҵ� ���
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                //������ �ٵ� ���ֱ�
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }

            if (hit.collider.GetComponent<Item>())
            {
                Debug.Log("It's a useless item!");
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.SetParent(pickUpParent.transform, true);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }*/
            #endregion
        }
    }

    private void PickUp2()
    {
        if (hit.collider != null)
        {
            Debug.Log($"������ü {hit.collider.name}");
        }

        // ���� ��ü�� �ְ� �տ� �� ���� ������
        // ���� �ִ� ���¿��� e ������ �ƹ� �ϵ� �Ͼ�� �ʴ´�.
        if (hit.collider != null && inHandItem == null)
        {

            if (hit.collider.GetComponent<GrabObject>())
            {
                grabObject = hit.collider.GetComponent<GrabObject>();

                if (grabObject != null)
                {
                    //���� �� �Ҹ�
                    pickUpSource.Play();

                    // �տ� �� �����۰� ������ �� �ִ� �������� �����ϰ� �Ҵ�
                    inHandItem = grabObject.PickUp();

                    // ������ �� �ʱ� ���� �����ϰ� ���� �������� ���
                    initialLocalScale = inHandItem.transform.localScale;
                    initialGlobalScale = inHandItem.transform.lossyScale;

                    // ���ο� �θ�� �̵��� �� ���� �������� ����
                    inHandItem.transform.SetParent(pickUpParent.transform, grabObject.isKeepWorldPosition);

                    inHandItem.transform.localScale = DivideVector3(inHandItem.transform.lossyScale, pickUpParent.lossyScale);
                }
            }

            if (hit.collider.GetComponent<OpenDrawer>())
            {
                openDrawer = hit.collider.GetComponent<OpenDrawer>();

                if (openDrawer != null)
                {
                    //���� �� �Ҹ�
                    pickUpSource.Play();

                    openDrawer.isOpen = !openDrawer.isOpen;
                }
            }
        }

        else if(hit.collider != null && inHandItem == true)
        {
            Debug.Log("�������ϳ�");

            if (hit.collider.GetComponent<OpenDoor>())
            {
                openDoor = hit.collider.GetComponent<OpenDoor>();

                if (openDoor != null && inHandItem.name == "inHandItem.name")
                {
                    Debug.Log("�������ϳ�2");
                    //������ ���� ��Ҵµ� ���� Key�� ������ �ְ�, Ű�� ��� �ִٸ� �� ������
                    if (GameManager.instance.Mission1 == true)
                    {
                        //���� �� �Ҹ�
                        pickUpSource.Play();
                        Debug.Log("�̼�1Ǯ���");
                        openDoor.isOpen = !openDoor.isOpen;
                    }
                    else
                    {
                        Debug.Log("�̼�1��ǰ");
                        openDoor.isOpen = !openDoor.isOpen;
                    }
                }
            }
        }
    }

    //���� �������� �θ��� �����ϰ� �ڽ��� ���� �������� ������ ���Դϴ�.�̸� �ùٸ��� �����Ͽ� ũ�⸦ ����
    private Vector3 DivideVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }


    #endregion
    //����ĳ��Ʈ�� �߻��Ͽ� ��ü ����
    private void Update()
    {
        KeyCheck();
        RayCheck();
    }

    bool isMouse;
    bool isEat;

    private void KeyCheck()
    {

        if (Input.GetMouseButton(0))
        {
            isMouse = true;
            PickUp();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouse = false;
            Drop();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp2();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop2();
        }

        if (Input.GetMouseButtonDown(1))
        {
            //PickUp2();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Use();
        }
    }

    //���̾� ����ũ üũ ��� �ϴ� �� ������ �����ϱ�
    private void RayCheck()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // ������ ���� ��ü�� �ִٸ� 
        //�� ���� ��ü�� ������ ���� ������Ʈ�� ���� ���� UI ����?
        if (hit.collider != null)
        {
            //?. null�� �ƴ��� ���� Ȯ�� / null�� �ƴ϶�� ToggleHighlight(false)�� ����(����ǥ��)
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            //������ �Ѵ�
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(false);
            //�� ���� �Ѵ� 2
            //hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(false);
            //UI �����
            pickUpUI.SetActive(false);
        }

        //�տ� �������� �ִٸ�
        //���� ���� �ʴ´�.
        if (inHandItem != null)
        {
            return;
        }

        //���� �ɽ�Ʈ �߻� (������ �� �ִ� ������Ʈ��) 
        //(�÷��̾� ī�޶�, �չ�������, ���� ������Ʈ ���� ���, �߻��� ����, ���� ���̾�)
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange, pickableLayerMask))
        {
            //true ���
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
            //���� ���� �� �ִ� ������!!!!
        }

        //�� ������ �ϴ�
        else if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange, openLayerMask))
        {
            //������ �Ѵ�
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }

        /*else if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange, openKeyLayerMask))
        {
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }*/
    }

    //������ ������ �˷��ֱ�
    public void AddHealth(int healthBoost)
    {
        Debug.Log($"���� {healthBoost}");
    }
}