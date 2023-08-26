using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPun//, IPunObservable
{
    // 이동속도
    [SerializeField] float speed = 5;
    [SerializeField] float camSpeed = 2;

    //카메라 위치
    [SerializeField] Transform[] camPos;

    //NickName Text 를 가져오자
    public Text nickName;

    //UI Canvas
    public GameObject myUI;

    //중력
    float gravit = -9.81f;
    //y속력
    float yVelocity = 0;
    //점프 힘
    //float jumpPower = 5;
    //점프 상태
    bool isJump = false;

    //방향
    float h;
    float v;

    public Animator anim;
    CharacterController cc;

    //카메라 Transform
    public Transform cam;
    bool CamMove;


    //서버에서 넘어오는 위치값
    Vector3 receivePos;
    //서버에서 넘어오는 회전값
    Quaternion receiveRot = Quaternion.identity;
    //보정하는 속력
    float lerpSpeed = 50;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        //만약에 내가 만든 Player 라면
        if (photonView.IsMine == true)
        {
            //UI 를 비활성화 하자
            myUI.SetActive(false);
        }

        else
        {
            //nickName 설정
            nickName.text = photonView.Owner.NickName;
        }

        //나의 PhotonView GameManager에 알려주자
        GameSetting.instance.AddPlayer(photonView);

        //시작시 카메라 위치
        cam.position = camPos[0].position;
    }

    void Update()
    {
        //내가 만든 플레이어라면
        if (photonView.IsMine)
        {
            Move();

            Down();


            //내가 움직일 때는 보여주는 데 상대는 움직이면 안된다.
            //애니메이션에 Parameter 값 전달 -> 자연스럽게 하기 위해
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        }

        //else
        //{
        //    //위치 보정
        //    transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
        //    //회전 보정
        //    transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        //}

        if (CamMove == true)
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
/*
        //애니메이션에 Parameter 값 전달 -> 자연스럽게 하기 위해
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
*/
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

    //[PunRPC]
    /*void SetTriggerRpc(string parameter)
    {
        anim.SetTrigger("");
    }*/

   /* public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //내 Player 라면
        if (stream.IsWriting)
        {
            //나의 위치값을 보낸다.
            stream.SendNext(transform.position);
            //나의 회전값을 보낸다.
            stream.SendNext(transform.rotation);
            //h 값 보낸다.
            stream.SendNext(h);
            //v 값 보낸다.
            stream.SendNext(v);
        }

        //내 Player 아니라면
        else
        {
            //위치값을 받자.
            receivePos = (Vector3)stream.ReceiveNext();
            //회전값을 받자.
            receiveRot = (Quaternion)stream.ReceiveNext();
            //h 값 받자.
            h = (float)stream.ReceiveNext();
            //v 값 받자.
            v = (float)stream.ReceiveNext();
        }
    }*/
}

