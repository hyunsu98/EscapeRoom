using Photon.Pun;
using UnityEngine;

public class PlayerPickable : MonoBehaviourPun
{
    //선택 가능한 레이어 마스크 설정 -> 충돌체 감지
    [Header("충돌가능오브젝트")]
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private LayerMask openLayerMask;
    [SerializeField] private LayerMask openKeyLayerMask;
    [SerializeField] private LayerMask openDoorLayerMask;

    //카메라 위치 -> 플레이어가 바라보고 있는 방향으로 해야함.
    [Header("카메라위치")]
    [SerializeField] private Transform playerCameraTransform;

    //마우스 UI (현재 닿으면 E키를 누르세요라고 나옴 -> 마우스를 드래그 하는 동안해 할 것임)
    //※ 가져올때 / 이동할때 / 아무것도 안될때 UI 다르게 변경
    /*[Header("마우스UI")]
    [SerializeField] private GameObject pickUpUI;
    [SerializeField] private GameObject dragUI;
    [SerializeField] private GameObject opneUI;*/

    //float 또는 int 변수를 특정 최소값으로 제한하는 데 사용되는 특성 (1 이하는 될 수 없게)
    [Header("Ray길이")]
    [SerializeField] [Min(1)] private float hitRange = 3;

    //잡았을 때 위치 (손에 넣을 수 있는) -> isMain 화면 옆에 , 아니면 애니메이션 앞에
    [Header("EatItemPos")]
    [SerializeField] private Transform pickUpParent;

    [Header("잡은물체")]
    [SerializeField] private GameObject inHandItem;
    [Header("잡은물체")]
    [SerializeField] private GameObject KeyItem;

    [Header("잡았을 때 위치")] //Ray길이랑 같게
    [SerializeField] Transform objectGrabPointTransform;

    [Header("Sound")] //사운드매니저로 관리
    [SerializeField] private AudioSource pickUpSource;

    //InputSystem을 사용한 키 입력 (using UnityEngine.LnputSystem; 필요)
    //[SerializeField] private InputActionReference interactionInput, dropInput, useInput;

    //닿은 물체 저장 (닿은 물체에 따라서 다르게 지정하면 -> 상호작용 가능) 책, 버튼, 힌트 등등
    private RaycastHit hit;

    //상속받아서 쓸 수 있게 만들기
    ObjectMove pickableItem;
    GrabObject grabObject;
    OpenDrawer openDrawer;
    OpenDoor openDoor;


    private Vector3 initialLocalScale;
    private Vector3 initialGlobalScale;

    //키를 얻고 있을때 bool 처리하기 위해 
    bool isFindKey = false;

    #region 키보드 Ver01
    /*private void Start()
    {
        //더해주기만 하면됨
        //if(Input.GetKeyDown(KeyCode.K)) 와 같은 기능임
        //키 누를 시 메소드 호출
        interactionInput.action.performed += PickUp; //e버튼
        dropInput.action.performed += Drop; //q버튼
        useInput.action.performed += Use;
    }

    //매개변수는 사용하지 않을 것임.
    private void Use(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            //손에 있는 아이템이 있다면.
            //객체가손에 있는 아이템 가져오기 구성요소와 같은지 확인
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                //나의 게임오브젝트 전달
                usable.Use(this.gameObject); //->hit.collider.GetComponent<Food>() 와 같은 유형을 확인할 필요가 없음
            }
        }
    }

    //놓기
    private void Drop(InputAction.CallbackContext obj)
    {
        // 잡고 있는 물체가 있다면
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
            Debug.Log($"닿은물체 {hit.collider.name}");
        }

        // 닿은 물체가 있고 손에 든 것이 없으면
        // 집고 있는 상태에서 e 누르면 아무 일도 일어나지 않는다.
        if (hit.collider != null && inHandItem == null)
        {
            //선언
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                pickUpSource.Play();
                // 손에 든 아이템과 선택할 수 있는 아이템을  동일하게 할당
                inHandItem = pickableItem.PickUp();
                //bool 값으로 넘겨주기
                //false일때 
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
            }

            #region 방법1 -> 유지 보수에 좋지 않음. -> IUsable 생성
            *//*Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            //Food스크립트를 가지고 있다면 Weapon
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Weapon>())
            {
                //음식이라면 손아이템 변형 위치
                Debug.Log("It's food!");
                inHandItem = hit.collider.gameObject;
                //화면 앞으로 놓기 위한 정의
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                //오브젝트를 다른 오브젝트 하위로 생성 SetParennt 메소드 사용
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                //리지드 바디 꺼주기
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

    #region 키보드 Ver02

    //이벤트 효과
    private void Use()
    {
        if (inHandItem != null)
        {
            //손에 있는 아이템이 있다면.
            //객체가손에 있는 아이템 가져오기 구성요소와 같은지 확인
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                //나의 게임오브젝트 전달
                usable.Use(this.gameObject); //->hit.collider.GetComponent<Food>() 와 같은 유형을 확인할 필요가 없음
            }
        }
    }

    //놓기
    private void Drop()
    {
        // 잡고 있는 물체가 있다면
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
    //놓기
    private void Drop2()
    {
        // 잡고 있는 물체가 있다면
        if (inHandItem != null)
        {
            if (hit.collider.GetComponent<GrabObject>())
            {
                // 부모에서 분리할 때 전역 스케일을 유지
                Vector3 localScale = DivideVector3(inHandItem.transform.lossyScale, inHandItem.transform.parent.lossyScale);

                inHandItem.transform.SetParent(null, true);

                inHandItem.transform.localScale = localScale;
                inHandItem = null;

                grabObject.Drop();
                grabObject = null;
            }
        }

        //잡고 있는 물체가 없지만
        //키도 놓게 하고 싶음...
        //어떻게 해야할까?
        /*else
        {
            if(KeyItem != null)
            {
                Debug.Log("잡고 있는 물체는 없지만 ");
                if (KeyItem.GetComponent<GrabObject>())
                {
                    Debug.Log("키가 있다면 놓을 수 있음");
                    // 부모에서 분리할 때 전역 스케일을 유지
                    Vector3 localScale = DivideVector3(KeyItem.transform.lossyScale, KeyItem.transform.parent.lossyScale);

                    KeyItem.transform.SetParent(null, true);

                    KeyItem.transform.localScale = localScale;
                    KeyItem = null;

                    grabObject.Drop();
                    grabObject = null;
                }
            }
        }*/
    }


    //잡았을때 내 위치의 정보 넘겨주고
    //이동할 수 있게
    private void PickUp()
    {
        if (hit.collider != null)
        {
            Debug.Log($"닿은물체 {hit.collider.name}");
        }

        // 닿은 물체가 있고 손에 든 것이 없으면
        // 집고 있는 상태에서 e 누르면 아무 일도 일어나지 않는다.
        if (hit.collider != null && inHandItem == null)
        {
            if (hit.collider.GetComponent<ObjectMove>())
            {
                pickableItem = hit.collider.GetComponent<ObjectMove>();

                if (pickableItem != null)
                {
                    //잡을 때 소리
                    pickUpSource.Play();

                    // 손에 든 아이템과 선택할 수 있는 아이템을 동일하게 할당
                    inHandItem = pickableItem.PickUp(photonView.Owner);

                    if (isMouse)
                    {
                        //2.오브젝트 자체에서 이동할 수 있게 (카메라 자식 위치 넘겨주기)
                        pickableItem.Grab(objectGrabPointTransform);
                    }
                }
            }


            #region 방법1 -> 유지 보수에 좋지 않음. -> IPickable 생성
            /*Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            //Food스크립트를 가지고 있다면 Weapon
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Weapon>())
            {
                //음식이라면 손아이템 변형 위치
                Debug.Log("It's food!");
                inHandItem = hit.collider.gameObject;
                //화면 앞으로 놓기 위한 정의
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                //오브젝트를 다른 오브젝트 하위로 생성 SetParennt 메소드 사용
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                //리지드 바디 꺼주기
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
            Debug.Log($"닿은물체 {hit.collider.name}");
        }

        // 닿은 물체가 있고 손에 든 것이 없으면
        // 집고 있는 상태에서 e 누르면 아무 일도 일어나지 않는다.
        if (hit.collider != null && inHandItem == null)
        {
            if (hit.collider.GetComponent<GrabObject>())
            {
                grabObject = hit.collider.GetComponent<GrabObject>();

                if (grabObject != null)
                {
                    //잡을 때 소리
                    pickUpSource.Play();

                    // 손에 든 아이템과 선택할 수 있는 아이템을 동일하게 할당
                    inHandItem = grabObject.PickUp(photonView.Owner);

                    // 시작할 때 초기 로컬 스케일과 전역 스케일을 기록
                    initialLocalScale = inHandItem.transform.localScale;
                    initialGlobalScale = inHandItem.transform.lossyScale;

                    // 새로운 부모로 이동할 때 전역 스케일을 유지
                    inHandItem.transform.SetParent(pickUpParent.transform, grabObject.isKeepWorldPosition);

                    inHandItem.transform.localScale = DivideVector3(inHandItem.transform.lossyScale, pickUpParent.lossyScale);

                    if (hit.collider.CompareTag("Key"))
                    {
                        KeyItem = inHandItem;
                        inHandItem = null;
                        isFindKey = true;
                        GameManager.instance.KeyEat(true);
                        Debug.Log("얻은 것이 키라면 inHandltem null로");
                    }
                }
            }

            if (hit.collider.GetComponent<OpenDrawer>())
            {
                openDrawer = hit.collider.GetComponent<OpenDrawer>();

                if (openDrawer != null)
                {
                    //잡을 때 소리
                    pickUpSource.Play();

                    //모두한테 붙여줘야함.?
                    //openDrawer.isOpen = !openDrawer.isOpen;
                    var pv = hit.collider.GetComponent<PhotonView>();
                    if (pv != null)
                    {
                        pv.RPC("DoorAction", RpcTarget.All, 1);
                    }
                    else
                    {
                        print("포톤뷰가 없어요.");
                    }
                }
            }

            if (hit.collider.GetComponent<OpenDoor>())
            {
                Debug.Log("들어오긴하나2");

                openDoor = hit.collider.GetComponent<OpenDoor>();

                if (openDoor != null)
                {
                    if (isFindKey)
                    {
                        Debug.Log("들어오긴하나2");

                        //문여는 곳이 닿았는데 내가 Key를 가지고 있고, 키를 들고 있다면 문 열리자
                        if (GameManager.instance.Mission1 == true)
                        {
                            //잡을 때 소리
                            pickUpSource.Play();
                            Debug.Log("미션1풀어보자");
                            openDoor.isOpen = !openDoor.isOpen;
                            Destroy(KeyItem);

                            if (openDoor.isOpen == true)
                            {
                                //GameManager.instance.Mission2 = true;
                                photonView.RPC(nameof(Check), RpcTarget.All, 1);
                            }
                        }
                        else
                        {
                            Debug.Log("미션1못품");
                        }
                    }
                }
            }
        }
    }

    [PunRPC]
    public void Check(bool isCk)
    {
        GameManager.instance.Mission2 = true;
    }

    //전역 스케일은 부모의 스케일과 자식의 로컬 스케일이 곱해진 값입니다.이를 올바르게 관리하여 크기를 유지
    private Vector3 DivideVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }


    #endregion
    //레이캐스트를 발사하여 물체 인지
    private void Update()
    {
        if (photonView.IsMine)
        {
            KeyCheck();
            RayCheck();
        }
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

            if (isEnding)
            {
                GameManager.instance.Ending();
            }
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

    //레이어 마스크 체크 어떻게 하는 게 좋을지 생각하기
    private void RayCheck()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // 이전에 닿은 물체가 있다면 
        //※ 닿은 물체가 있으면 닿은 오브젝트의 강조 끄고 UI 끄기?
        if (hit.collider != null)
        {
            //?. null이 아닌지 여부 확인 / null이 아니라면 ToggleHighlight(false)로 실행(강조표시)
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            //문열게 한다
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(false);
            //UI 숨기기
            UIManager.instance.pickUpUI.SetActive(false);
            UIManager.instance.dragUI.SetActive(false);
            UIManager.instance.opneUI.SetActive(false);
        }

        //손에 아이템이 있다면
        //감지 하지 않는다.
        if (inHandItem != null)
        {
            return;
        }

        //레이 케스트 발사 (감지할 수 있는 오브젝트만) 
        //(플레이어 카메라, 앞방향으로, 닿은 오브젝트 정보 얻기, 발사할 길이, 닿을 레이어)
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange, pickableLayerMask))
        {
            //true 라면
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);

            if (hit.collider.GetComponent<ObjectMove>())
            {
                UIManager.instance.pickUpUI.SetActive(true);
                //이제 집을 수 있는 상태임!!!!
            }

            else if (hit.collider.GetComponent<GrabObject>())
            {
                UIManager.instance.dragUI.SetActive(true);
            }
        }

        //문 열리게 하는
        else if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange, openLayerMask))
        {
            //문열게 한다
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);
            UIManager.instance.opneUI.SetActive(true);
        }

        //문 열리게 하는
        /*else if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit,
            hitRange, openDoorLayerMask))
        {
            if(GameManager.instance.MissionClear == true)
            {
                Debug.Log("끝남");

                //문열게 한다
                hit.collider.GetComponent<OpenDoor>()?.OpenDoorChek(true);
                hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(true);

                UIManager.instance.opneUI.SetActive(true);

                isEnding = true;
            }
        }*/
    }

    public bool isEnding;

    //먹으면 증가함 알려주기
    public void AddHealth(int healthBoost)
    {
        Debug.Log($"먹음 {healthBoost}");
    }
}