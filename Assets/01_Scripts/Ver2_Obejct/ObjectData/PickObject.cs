using UnityEngine;

public class PickObject : MonoBehaviour
{
    [Header("�� ������Ʈ")]
    [SerializeField] GameObject destroyedVersion = null;

    public float speedThreshold = 5.0f; // ���� �ӵ� �Ӱ谪

    Rigidbody rb;

    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ���θ� ��Ÿ���� ����
    public LayerMask groundLayer;    // �ٴ��� ��Ÿ���� ���̾�


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Rigidbody�� ���� �ӵ��� ����մϴ�.
        float currentSpeed = rb.velocity.magnitude;

        // ���� �ӵ��� �Ѿ����� üũ�մϴ�.
        if (currentSpeed > speedThreshold)
        {
            Debug.Log("�ӵ��� ���� �ӵ��� �Ѿ����ϴ�!");
            // ���⿡ ���ϴ� ó���� �߰��ϼ���.

            // �ٴڿ� ��Ҵ��� ���θ� üũ�մϴ�.
            //GetComponent<Collider>().bounds.extents.y -> �ݶ��̴��� ������ ������ ��Ÿ��, �ٴ����κ��� ĳ���� �߽ɱ����� �Ÿ��� ���Ҷ� ���
            isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayer);

            if (isGrounded)
            {
                Debug.Log("�ٴڿ� ��ҽ��ϴ�!");
                // ���⿡ ���ϴ� ó���� �߰��ϼ���.

                if (destroyedVersion != null)
                {
                    //�� ����
                    Instantiate(destroyedVersion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

}
