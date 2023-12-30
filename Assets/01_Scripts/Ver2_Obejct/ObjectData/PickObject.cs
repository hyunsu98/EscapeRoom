using Photon.Pun;
using UnityEngine;

public class PickObject : MonoBehaviourPun
{
    [Header("깰 오브젝트")]
    //키
    [SerializeField] GameObject bottleKey = null;
    //토큰
    [SerializeField] GameObject bottleToken = null;
    //아무것도 아님
    [SerializeField] GameObject bottleCracked = null;

    public float speedThreshold = 5.0f; // 일정 속도 임계값

    Rigidbody rb;

    private bool isGrounded = false; // 바닥에 닿았는지 여부를 나타내는 변수
    public LayerMask groundLayer;    // 바닥을 나타내는 레이어

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
            //힘 가하는 게 다 다르기 때문에 내가 true가 되면 상대한테도 알려줘야함.

            // Rigidbody의 현재 속도를 계산합니다.
            float currentSpeed = rb.velocity.magnitude;

            // 일정 속도를 넘었는지 체크합니다.
            if (currentSpeed > speedThreshold)
            {
                Debug.Log("속도가 일정 속도를 넘었습니다!");

                // 바닥에 닿았는지 여부를 체크합니다.
                //GetComponent<Collider>().bounds.extents.y -> 콜라이더의 높이의 절반을 나타냄, 바닥으로부터 캐릭터 중심까지의 거리를 구할때 사용
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
        Debug.Log("모두 삭제");
    }
}

