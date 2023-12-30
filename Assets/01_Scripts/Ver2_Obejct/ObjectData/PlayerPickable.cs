using Photon.Pun;
using UnityEngine;

public class PlayerPickable : MonoBehaviourPun
{
    //카메라 위치 -> 플레이어가 바라보고 있는 방향으로 해야함.
    [Header("카메라위치")]
    [SerializeField] private Transform playerCameraTransform;

    //float 또는 int 변수를 특정 최소값으로 제한하는 데 사용되는 특성 (1 이하는 될 수 없게) 
    [Header("Ray길이")] //판별을 위한 변수
    [SerializeField] [Min(1)] private float hitRange = 3;

    [Header("잡은물체")]
    [SerializeField] private GameObject inHandItem;

    [Header("잡았을 때 위치")] //Ray길이랑 같게(실제 물체가 있는 위치)
    [SerializeField] Transform objectGrabPointTransform;

    //닿은 물체 저장 (닿은 물체에 따라서 다르게 지정하면 -> 상호작용 가능) 책, 버튼, 힌트 등등
    private RaycastHit hit;

    //상속받아서 쓸 수 있게 만들기 (잡은 객체를 꺼야하기 때문에)
    PickUpObject pickUpObject;
    DragObject dragObject;

    private void Update()
    {
        // 나일 때만 할 수 있게
        if (photonView.IsMine)
        {
            Debug.Log(PhotonNetwork.NickName + "이 실행하는 코드입니다");

            //나일때만인데 왜 되는 거지?
            RayCheck();

            KeyCheck();
        }
    }

    private void KeyCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //오브젝트 문열기 / 토큰
            Click();
        }

        // 오브젝트 잡기 , 이동
        // 마우스 드래그 이동
        if (Input.GetMouseButton(0))
        {
            PickUp(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop(true);
        }

        // 자동 이동
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
        //닿은 물체가 없다면 코드실행하지 않는다.
        if (hit.collider == null)
        {
            return;
        }

        //X축 문 회전
        if(hit.collider.GetComponent<OpenDrawer>())
        {
            //자신의 포톤뷰를 가져온다.
            var pv = hit.collider.GetComponent<PhotonView>();

            //포톤뷰가 있다면
            if (pv != null)
            {
                //포톤뷰의 DoorAction을 실행시킨다.
                //매개변수 값이 들어가야 할 것임. (사용하지 않아도 필요)
                pv.RPC("DoorAction", RpcTarget.All , 1);
            }
            else
            {
                print("X축 문회전 포톤뷰가 없습니다");
            }
        }

        //Y축 문 회전
        if (hit.collider.GetComponent<OpenDoor>())
        {
            var pv = hit.collider.GetComponent<PhotonView>();

            if (pv != null)
            { 
                pv.RPC("OpenDoorAction", RpcTarget.All, 1);
            }
            else
            {
                print("Y축 문회전 포톤뷰가 없습니다");
            }
        }

        //토큰획득 시 점수추가
        if (hit.collider.GetComponent<TokenObject>())
        {
            TokenObject token = hit.collider.GetComponent<TokenObject>();

            token.Sorce();
        }
    }
    
    //잡았을때 내 위치의 정보 넘겨주고
    //이동할 수 있게
    private void PickUp(bool key)
    {
        if (hit.collider != null)
        {
            Debug.Log($"잡을 오브젝트 {hit.collider.name}");
        }

        // 닿은 객체 있는데 잡고 있는 객체가 없다면
        // ※ 상대 유저에게 포톤뷰를 넘겨줬을 때 inHandItem은 null이 되어야 한다.
        if (hit.collider != null && inHandItem == null)
        {
            //drage
            if (key)
            {
                dragObject = hit.collider.GetComponent<DragObject>();

                if (dragObject != null)
                {
                    Debug.Log($"드래그 오브젝트 {hit.collider.name}");

                    // 손에 든 객체를 설정 (포톤뷰의 주인을 보낸다)
                    // 잡을 때 소유권을 넘겨준다. (그렇지 않으면 방장만 true로 되기 때문에 동기화 오류가 됨
                    inHandItem = dragObject.PickUp(photonView.Owner);

                    //오브젝트의 위치는 닿은 지점의 위치로 변경
                    objectGrabPointTransform.position = hit.point;

                    //오브젝트 자체에서 이동할 수 있게 (카메라 자식 위치 넘겨주기)
                    dragObject.Grab(objectGrabPointTransform);
                }
            }

            //pick
            else
            {
                pickUpObject = hit.collider.GetComponent<PickUpObject>();

                if (pickUpObject != null)
                {
                    Debug.Log($"드래그 오브젝트 {hit.collider.name}");

                    // 손에 든 객체를 설정 (포톤뷰의 주인을 보낸다)
                    // 잡을 때 소유권을 넘겨준다. (그렇지 않으면 방장만 true로 되기 때문에 동기화 오류가 됨)
                    inHandItem = pickUpObject.PickUp(photonView.Owner);

                    objectGrabPointTransform.position = hit.point;

                    //오브젝트 자체에서 이동할 수 있게 (카메라 자식 위치 넘겨주기)
                    pickUpObject.Grab(objectGrabPointTransform);
                }
            }
        }
    }

    //놓기
    private void Drop(bool key)
    {
        // 잡고 있는 물체가 있다면
        if (inHandItem != null)
        {
            if (key)
            {
                //둘 다 부모 설정을 안해줄 것임.
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

    //UI 셋팅 (닿을 수 있는 오브젝트 확인)
    private void RayCheck()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // 닿은 오브젝트가 없을 시
        if (hit.collider != null)
        {
            //기본 셋팅
            UIManager.instance.BaseUI();

            Outline outLine = hit.collider.GetComponent<Outline>();

            if (outLine != null)
            {
                outLine.OutlineWidth = 0;
            }

            //?. null이 아닌지 여부 확인 / null이 아니라면 ToggleHighlight(false)로 실행(잡을 수 있는 상태)
            hit.collider.GetComponent<OpenHighlight>()?.ToggleHighlight(false);
        }

        //손에 아이템이 있다면 감지 하지 않는다.
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

            //태그를 통해 UI 셋팅
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