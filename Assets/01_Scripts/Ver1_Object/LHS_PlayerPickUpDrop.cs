using UnityEngine;

//카메라를 따라다니는 오브젝트임.
//카메라 자식과 오브젝트 거리만큼 위치에서 움직이게 만들면
public class LHS_PlayerPickUpDrop : MonoBehaviour
{
    [Header("Raycast 정보")]
    //레이 쏜 길이랑 카메라 길이랑 같게 만들면
    [SerializeField] float pickupDistance = 2f;
    //방법 두가지 1.가져오려는 객체만 2.플레이어 빼고 다 (2)
    [SerializeField] LayerMask pickUpLayerMask;
    //카메라 자식
    [SerializeField] Transform objectGrabPointTransform;

    //플레이어 카메라
    [SerializeField] Transform playerCameraTransform;

    private LHS_ObjectGrabbable objectGrabbable;

    private void Start()
    {
        //playerCameraTransform = Camera.main.transform;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            //잡으려고 하는 물체를 들고 있지 않을 때
            if (objectGrabbable == null)
            {
                //레이캐스트를 이용하여 객체 확인
                //내 앞방향이 아닌 카메라의 앞방향을 기준
                //충돌되면 true로 변환되고 충돌지점 정보 넘겨줌

                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance))
                {
                    //잡을 수 있는 물체라면
                    //TryGetComponent -> bool형함수 / 찾았으면 true -> out 되는 component 할당
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        //카메라 자식위치 넘겨주기
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);

                    }
                }

                // Ray 발사

            }

            //현재 가지고 있는 거 놓기
            else
            {
                objectGrabbable.Drop();
                //지워주기
                objectGrabbable = null;
            }

        }
    }
}
