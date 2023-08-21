using Photon.Pun;
using UnityEngine;

public class PickObject : MonoBehaviourPunCallbacks
{
    [Header("깰 오브젝트")]
    //키
    [SerializeField] GameObject destroyedVersion = null;
    //토큰
    [SerializeField] GameObject destroyedVersion2 = null;
    //아무것도 아님
    [SerializeField] GameObject destroyedVersion3 = null;

    public float speedThreshold = 5.0f; // 일정 속도 임계값

    Rigidbody rb;

    private bool isGrounded = false; // 바닥에 닿았는지 여부를 나타내는 변수
    public LayerMask groundLayer;    // 바닥을 나타내는 레이어


    public bool isKey;
    public bool isToken;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // 나일때만 값을 알려주고 깨질 수 있게 하기
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
            //힘 가하는 게 다 다르기 때문에 내가 true가 되면 상대한테도 알려줘야함.

            // Rigidbody의 현재 속도를 계산합니다.
            float currentSpeed = rb.velocity.magnitude;

            // 일정 속도를 넘었는지 체크합니다.
            if (currentSpeed > speedThreshold)
            {
                Debug.Log("속도가 일정 속도를 넘었습니다!");
                // 여기에 원하는 처리를 추가하세요.

                // 바닥에 닿았는지 여부를 체크합니다.
                //GetComponent<Collider>().bounds.extents.y -> 콜라이더의 높이의 절바을 나타냄, 바닥으로부터 캐릭터 중심까지의 거리를 구할때 사용
                isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f, groundLayer);

                if (isGrounded)
                {
                    Debug.Log("바닥에 닿았습니다!");
                    // 여기에 원하는 처리를 추가하세요.

                    //나 삭제
                    if (isKey)
                    {
                        //포톤으로 생성되야할듯
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
