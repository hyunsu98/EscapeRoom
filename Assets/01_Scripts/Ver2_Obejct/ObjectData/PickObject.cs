using Photon.Pun;
using UnityEngine;

public class PickObject : MonoBehaviourPun
{
    [Header("�� ������Ʈ")]
    //Ű
    [SerializeField] GameObject bottleKey = null;
    //��ū
    [SerializeField] GameObject bottleToken = null;
    //�ƹ��͵� �ƴ�
    [SerializeField] GameObject bottleCracked = null;

    public float speedThreshold = 5.0f; // ���� �ӵ� �Ӱ谪

    Rigidbody rb;

    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ���θ� ��Ÿ���� ����
    public LayerMask groundLayer;    // �ٴ��� ��Ÿ���� ���̾�

    public bool isKey;
    public bool isToken;

    int num;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            //�� ���ϴ� �� �� �ٸ��� ������ ���� true�� �Ǹ� ������׵� �˷������.

            // Rigidbody�� ���� �ӵ��� ����մϴ�.
            float currentSpeed = rb.velocity.magnitude;

            // ���� �ӵ��� �Ѿ����� üũ�մϴ�.
            if (currentSpeed > speedThreshold)
            {
                Debug.Log("�ӵ��� ���� �ӵ��� �Ѿ����ϴ�!");

                // �ٴڿ� ��Ҵ��� ���θ� üũ�մϴ�.
                //GetComponent<Collider>().bounds.extents.y -> �ݶ��̴��� ������ ������ ��Ÿ��, �ٴ����κ��� ĳ���� �߽ɱ����� �Ÿ��� ���Ҷ� ���
                isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayer);

                if (isGrounded)
                {
                    if (isKey)
                    {
                        PhotonNetwork.Instantiate("BottleKey", transform.position, transform.rotation);
                    }

                    else if (isToken)
                    {
                        PhotonNetwork.Instantiate("BottleToken", transform.position, transform.rotation);
                    }

                    else
                    {
                        PhotonNetwork.Instantiate("BottleCracked", transform.position, transform.rotation);
                    }

                    photonView.RPC(nameof(DestroyPun), RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    void DestroyPun()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BOTTLE);
        Destroy(this.gameObject);
        Debug.Log("��� ����");
    }
}

