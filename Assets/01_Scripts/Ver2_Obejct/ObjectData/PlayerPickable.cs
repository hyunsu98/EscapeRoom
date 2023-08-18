using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickable : MonoBehaviour
{
    //선택 가능한 레이어 마스크 설정
    //충돌체 감지
    [SerializeField]
    private LayerMask pickableLayerMask;

    //카메라 위치
    //플레이어가 바라보고 있는 방향으로 해야함.
    [SerializeField]
    private Transform playerCameraTransform;

    //마우스 UI
    //현재 닿으면 E키를 누르세요라고 나옴 -> 마우스를 드래그 하는 동안해 할 것임.
    //※ 가져올때 / 이동할때 / 아무것도 안될때 UI 다르게 변경
    [SerializeField]
    private GameObject pickUpUI;

    //먹으면 증가함 알려주기
    internal void AddHealth(int healthBoost)
    {
        Debug.Log($"먹음 {healthBoost}");
    }

    [SerializeField]
    //float 또는 int 변수를 특정 최소값으로 제한하는 데 사용되는 특성
    //1 이하는 될 수 없게
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    //잡았을 때 위치 (손에 넣을 수 있는) 
    //isMain 화면 옆에 , 아니면 애니메이션 앞에
    private Transform pickUpParent;

    //손에 있는 물체 
    [SerializeField]
    private GameObject inHandItem;

    //잡을 때 소리 발생
    [SerializeField]
    private AudioSource pickUpSource;

    //카메라 자식
    [SerializeField] Transform objectGrabPointTransform;

    //InputSystem을 사용한 키 입력!
    //using UnityEngine.LnputSystem; 필요
    /*[SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;*/

    //닿은 물체 저장
    //닿은 물체에 따라서 다르게 지정하면 -> 상호작용 가능
    //책, 버튼, 힌트 등등
    private RaycastHit hit;

    IPickable pickableItem;

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
    //매개변수는 사용하지 않을 것임.
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
            inHandItem.transform.SetParent(null);
            inHandItem = null;

            //지워주기
            pickableItem.Drop();
            pickableItem = null;
            
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
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
           
            pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                //잡을 때 소리
                pickUpSource.Play();
                // 손에 든 아이템과 선택할 수 있는 아이템을 동일하게 할당
                inHandItem = pickableItem.PickUp();

                //1.내 자식으로 들어와서 이동할 수 있게
                //bool 값으로 넘겨주기
                //true 아이템의 월드 위치 유지. 그렇지 않으면 아이템의 로컬 위치 설정
                //inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);

                //2.오브젝트 자체에서 이동할 수 있게
                //카메라 자식 위치 넘겨주기 -> 가능?
                //닿은 지점 넘겨주면?
                //닿은 지점 거리만큼 위치
                pickableItem.Grab(objectGrabPointTransform);
            }
            #region 방법1 -> 유지 보수에 좋지 않음. -> IUsable 생성
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
    #endregion
    //레이캐스트를 발사하여 물체 인지
    private void Update()
    {
        KeyCheck();
        RayCheck();
    }

    private void KeyCheck()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }

        if(Input.GetMouseButtonDown(0))
        {
            Use();
        }
    }


    private void RayCheck()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // 이전에 닿은 물체가 있다면 
        //※ 닿은 물체가 있으면 닿은 오브젝트의 강조 끄고 UI 끄기?
        if (hit.collider != null)
        {
            //?. null이 아닌지 여부 확인 / null이 아니라면 ToggleHighlight(false)로 실행(강조표시)
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            //UI 숨기기
            pickUpUI.SetActive(false);
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
            pickUpUI.SetActive(true);
            //이제 집을 수 있는 상태임!!!!
        }
    }
}