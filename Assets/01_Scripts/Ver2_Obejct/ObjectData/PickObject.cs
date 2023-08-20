using UnityEngine;

public class PickObject : MonoBehaviour
{
    [Header("깰 오브젝트")]
    [SerializeField] GameObject destroyedVersion = null;

    public float speedThreshold = 5.0f; // 일정 속도 임계값

    Rigidbody rb;

    private bool isGrounded = false; // 바닥에 닿았는지 여부를 나타내는 변수
    public LayerMask groundLayer;    // 바닥을 나타내는 레이어


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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

                if (destroyedVersion != null)
                {
                    //나 삭제
                    Instantiate(destroyedVersion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

}
