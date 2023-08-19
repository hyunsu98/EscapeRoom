using UnityEngine;

public class PickableLerp : MonoBehaviour, IPickable
{
    //���� ����? ���ص� ��
    public bool KeepWorldPosition { get; private set; }

    Rigidbody rb;

    //��ü ��� ������ ����
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null)
            rb.isKinematic = true;
        return gameObject;
    }

    public void Update()
    {
        //ī�޶� �ڽ� ��ġ �ް�
    }

    //�÷��̾�� ȣ���� �� �ֵ���
    //����Ʈ�� ���� ��ȯ�� �޵���
    public void Grab(Transform objectGrabPointTransform)
    {
        //������ ����ٴϱ� ���ֱ�
        hiddenObject = false;
        //��ü ��� ������ ����
        this.objectGrabPointTransform = objectGrabPointTransform;
        //������� �߷� ���ֱ�
        //rb.useGravity = false;
    }

    //���� -> �÷��̾�� ����
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
    }

    //�̵�
    private void FixedUpdate()
    {
        //��ü�� �ִٸ� 
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            //Lerp�̵�
            // 1. ī�޶�~�չ������� Ray�� ���� �ε��� �������� �Ÿ�
            // 2. ī�޶�~objectGrabPointTransform.position���� �Ÿ�
            // 1�� 2�� ª�� �Ÿ��� �ش��ϴ� ��ġ

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            rb.MovePosition(newPosition);
        }

        //������ hiddenObject �����ϸ� �ȵ�.
        else
        {
            //�ڵ� ���� �ʿ�
            if (hiddenObject)
            {
                transform.position = contactPlatform.transform.position - distance;
                Debug.Log("�̵��ؾ���");
            }
        }
    }

    //������ ���� �̵��ϱ� ���� �ڵ�
    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    bool hiddenObject;

    //���� ������ �˷��ְ� �̵��� �� �ְ�
    private void OnTriggerEnter(Collider other)
    {
        //�浹 �ߴµ� �� ������Ʈ�� �������� �Ÿ�
        if (other.gameObject.CompareTag("HiddenObject"))
        {
            //�浹�� ������Ʈ�� ��ġ�� �� ��ġ�� ���� �ض�.
            Debug.Log("��Ѵ�");
            contactPlatform = other.gameObject;

            platformPosition = contactPlatform.transform.position;
            distance = platformPosition - transform.position;

            hiddenObject = true;
        }
    }

    //�ٴ� ������
    private void OnTriggerExit(Collider other)
    {
        hiddenObject = false;
    }
}
