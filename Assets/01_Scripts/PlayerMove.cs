using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 이동속도
    [SerializeField] float speed = 5;
    [SerializeField] float camSpeed = 2;

    //카메라 위치
    [SerializeField] Transform[] camPos;

    //중력
    float gravit = -9.81f;
    //y속력
    float yVelocity = 0;
    //점프 힘
    float jumpPower = 5;
    //점프 상태
    bool isJump = false;

    //방향
    float h;
    float v;

    Animator anim;
    CharacterController cc;

    //카메라 Transform
    Transform cam;
    bool CamMove;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        //시작시 카메라 위치
        cam = Camera.main.transform;
        cam.position = camPos[0].position;
    }

    void Update()
    {
        Move();

        Down();

        if(CamMove == true)
        {
            cam.position = Vector3.Lerp(cam.position, camPos[1].position, camSpeed * Time.deltaTime);
        }

        else
        {
            cam.position = Vector3.Lerp(cam.position, camPos[0].position, camSpeed * Time.deltaTime);
        }
    }

    [Header("달리기")]
    public bool run;

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        #region 방향1 (절대 좌표 이동)
        /*Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();*/
        #endregion

        //방향 만들기 2 -> 플레이어 앞방향을 기준으로 이동. (상대좌표의 이동)
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir2 = dirH + dirV;
        dir2.Normalize();

        //만약에 땅에 닿아있다면
        if (cc.isGrounded == true)
        {
            yVelocity = 0;

            if (isJump == true)
            {
                //*보류 : 착지 애니메이션
            }
            isJump = false;
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = jumpPower;

            //*보류 : 점프 애니메이션
            isJump = true;
        }*/

        //중력 적용
        yVelocity += gravit * Time.deltaTime;

        dir2.y = yVelocity;

        //transform.position += dir2 * speed * Time.deltaTime;
        cc.Move(dir2 * speed * Time.deltaTime);

        //애니메이션에 Parameter 값 전달 -> 자연스럽게 하기 위해
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }

    void Down()
    {
        //카메라 시점 변경
        if (Input.GetKeyDown(KeyCode.C))
        {
            //현재 애니메이션 Down이라면 flase //일어서기
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Down"))
            {
                anim.SetBool("Down", false);
                CamMove = false;
            }

            //앉기
            else
            {
                anim.SetBool("Down", true);
                CamMove = true;
            }
        }
    }
}
