using Photon.Pun;
using UnityEngine;

public class PickObject : MonoBehaviourPunCallbacks
{
    [Header("�� ������Ʈ")]
    //Ű
    [SerializeField] GameObject destroyedVersion = null;
    //��ū
    [SerializeField] GameObject destroyedVersion2 = null;
    //�ƹ��͵� �ƴ�
    [SerializeField] GameObject destroyedVersion3 = null;

    public float speedThreshold = 5.0f; // ���� �ӵ� �Ӱ谪

    Rigidbody rb;

    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ���θ� ��Ÿ���� ����
    public LayerMask groundLayer;    // �ٴ��� ��Ÿ���� ���̾�


    public bool isKey;
    public bool isToken;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // ���϶��� ���� �˷��ְ� ���� �� �ְ� �ϱ�
    [PunRPC]
    public void Break(bool on)
    {
        if (rb != null)
        {
            rb.isKinematic = on;
        }
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            //�� ���ϴ� �� �� �ٸ��� ������ ���� true�� �Ǹ� ������׵� �˷������.

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

                    //�� ����
                    if (isKey)
                    {
                        //�������� �����Ǿ��ҵ�
                        PhotonNetwork.Instantiate("BottleKey", transform.position, Quaternion.identity);
                        //Instantiate(destroyedVersion, transform.position, Quaternion.identity);
                    }

                    else if (isToken)
                    {
                        //Instantiate(destroyedVersion2, transform.position, Quaternion.identity);
                        PhotonNetwork.Instantiate("BottleToken", transform.position, Quaternion.identity);
                    }

                    else
                    {
                        //Instantiate(destroyedVersion3, transform.position, Quaternion.identity);
                        PhotonNetwork.Instantiate("BottleCracked", transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
