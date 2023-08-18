using UnityEngine;

//ī�޶� ����ٴϴ� ������Ʈ��.
//ī�޶� �ڽİ� ������Ʈ �Ÿ���ŭ ��ġ���� �����̰� �����
public class LHS_PlayerPickUpDrop : MonoBehaviour
{
    [Header("Raycast ����")]
    //���� �� ���̶� ī�޶� ���̶� ���� �����
    [SerializeField] float pickupDistance = 2f;
    //��� �ΰ��� 1.���������� ��ü�� 2.�÷��̾� ���� �� (2)
    [SerializeField] LayerMask pickUpLayerMask;
    //ī�޶� �ڽ�
    [SerializeField] Transform objectGrabPointTransform;

    //�÷��̾� ī�޶�
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
            //�������� �ϴ� ��ü�� ��� ���� ���� ��
            if (objectGrabbable == null)
            {
                //����ĳ��Ʈ�� �̿��Ͽ� ��ü Ȯ��
                //�� �չ����� �ƴ� ī�޶��� �չ����� ����
                //�浹�Ǹ� true�� ��ȯ�ǰ� �浹���� ���� �Ѱ���

                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance))
                {
                    //���� �� �ִ� ��ü���
                    //TryGetComponent -> bool���Լ� / ã������ true -> out �Ǵ� component �Ҵ�
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        //ī�޶� �ڽ���ġ �Ѱ��ֱ�
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);

                    }
                }

                // Ray �߻�

            }

            //���� ������ �ִ� �� ����
            else
            {
                objectGrabbable.Drop();
                //�����ֱ�
                objectGrabbable = null;
            }

        }
    }
}
