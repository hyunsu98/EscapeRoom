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

        //���� ���� Player �� �ƴҶ�
        if (photonView.IsMine == false)
        {
            //PlayerFire ������Ʈ�� ��Ȱ��ȭ
            this.enabled = false;
        }
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
                // ���⿡ ���ϴ� ó���� �߰��ϼ���.

                // �ٴڿ� ��Ҵ��� ���θ� üũ�մϴ�.
                //GetComponent<Collider>().bounds.extents.y -> �ݶ��̴��� ������ ������ ��Ÿ��, �ٴ����κ��� ĳ���� �߽ɱ����� �Ÿ��� ���Ҷ� ���
                isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayer);

                if (isGrounded)
                {

                    if (isKey)
                    {
                        num = 1;
                    }

                    else if (isToken)
                    {
                        num = 2;
                    }

                    else
                    {
                        num = 3;
                        Debug.Log("3���̶�");
                    }

                    photonView.RPC(nameof(BreakBottle), RpcTarget.All, transform.position, transform.rotation, num);

                    Debug.Log("�ٴڿ� ��ҽ��ϴ�!");
                }
            }
        }
    }

    [PunRPC]
    void BreakBottle(Vector3 breakPos, Quaternion breakRot, int check)
    {
        //�� ����
        if (check == 1)
        {
            //�������� �����Ǿ��ҵ�
            //PhotonNetwork.Instantiate("BottleKey", transform.position, Quaternion.identity);
            GameObject bottle = Instantiate(bottleKey);
            bottle.transform.position = breakPos;
            bottle.transform.rotation = breakRot;
            Debug.Log("Ű����");
        }

        else if (check == 2)
        {
            GameObject bottle = Instantiate(bottleToken);
            bottle.transform.position = breakPos;
            bottle.transform.rotation = breakRot;
            Debug.Log("��ū");
        }

        else if(check == 3)
        {
            GameObject bottle = Instantiate(bottleCracked);
            bottle.transform.position = breakPos;
            bottle.transform.rotation = breakRot;
            Debug.Log("����");
        }

        Destroy(this.gameObject);
    }
}

