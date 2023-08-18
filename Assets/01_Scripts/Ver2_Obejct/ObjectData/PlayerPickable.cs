using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickable : MonoBehaviour
{
    //���� ������ ���̾� ����ũ ����
    //�浹ü ����
    [SerializeField]
    private LayerMask pickableLayerMask;

    //ī�޶� ��ġ
    //�÷��̾ �ٶ󺸰� �ִ� �������� �ؾ���.
    [SerializeField]
    private Transform playerCameraTransform;

    //���콺 UI
    //���� ������ EŰ�� ���������� ���� -> ���콺�� �巡�� �ϴ� ������ �� ����.
    //�� �����ö� / �̵��Ҷ� / �ƹ��͵� �ȵɶ� UI �ٸ��� ����
    [SerializeField]
    private GameObject pickUpUI;

    //������ ������ �˷��ֱ�
    internal void AddHealth(int healthBoost)
    {
        Debug.Log($"���� {healthBoost}");
    }

    [SerializeField]
    //float �Ǵ� int ������ Ư�� �ּҰ����� �����ϴ� �� ���Ǵ� Ư��
    //1 ���ϴ� �� �� ����
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    //����� �� ��ġ (�տ� ���� �� �ִ�) 
    //isMain ȭ�� ���� , �ƴϸ� �ִϸ��̼� �տ�
    private Transform pickUpParent;

    //�տ� �ִ� ��ü 
    [SerializeField]
    private GameObject inHandItem;

    //���� �� �Ҹ� �߻�
    [SerializeField]
    private AudioSource pickUpSource;

    //ī�޶� �ڽ�
    [SerializeField] Transform objectGrabPointTransform;

    //InputSystem�� ����� Ű �Է�!
    //using UnityEngine.LnputSystem; �ʿ�
    /*[SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;*/

    //���� ��ü ����
    //���� ��ü�� ���� �ٸ��� �����ϸ� -> ��ȣ�ۿ� ����
    //å, ��ư, ��Ʈ ���
    private RaycastHit hit;

    IPickable pickableItem;

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
    //�Ű������� ������� ���� ����.
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
            inHandItem.transform.SetParent(null);
            inHandItem = null;

            //�����ֱ�
            pickableItem.Drop();
            pickableItem = null;
            
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
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
           
            pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                //���� �� �Ҹ�
                pickUpSource.Play();
                // �տ� �� �����۰� ������ �� �ִ� �������� �����ϰ� �Ҵ�
                inHandItem = pickableItem.PickUp();

                //1.�� �ڽ����� ���ͼ� �̵��� �� �ְ�
                //bool ������ �Ѱ��ֱ�
                //true �������� ���� ��ġ ����. �׷��� ������ �������� ���� ��ġ ����
                //inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);

                //2.������Ʈ ��ü���� �̵��� �� �ְ�
                //ī�޶� �ڽ� ��ġ �Ѱ��ֱ� -> ����?
                //���� ���� �Ѱ��ָ�?
                //���� ���� �Ÿ���ŭ ��ġ
                pickableItem.Grab(objectGrabPointTransform);
            }
            #region ���1 -> ���� ������ ���� ����. -> IUsable ����
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
    #endregion
    //����ĳ��Ʈ�� �߻��Ͽ� ��ü ����
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

        // ������ ���� ��ü�� �ִٸ� 
        //�� ���� ��ü�� ������ ���� ������Ʈ�� ���� ���� UI ����?
        if (hit.collider != null)
        {
            //?. null�� �ƴ��� ���� Ȯ�� / null�� �ƴ϶�� ToggleHighlight(false)�� ����(����ǥ��)
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
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
    }
}