using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPun//, IPunObservable
{
    // �̵��ӵ�
    [SerializeField] float speed = 5;
    [SerializeField] float camSpeed = 2;

    //ī�޶� ��ġ
    [SerializeField] Transform[] camPos;

    //NickName Text �� ��������
    public Text nickName;

    //UI Canvas
    public GameObject myUI;

    //�߷�
    float gravit = -9.81f;
    //y�ӷ�
    float yVelocity = 0;
    //���� ��
    //float jumpPower = 5;
    //���� ����
    bool isJump = false;

    //����
    float h;
    float v;

    public Animator anim;
    CharacterController cc;

    //ī�޶� Transform
    public Transform cam;
    bool CamMove;


    //�������� �Ѿ���� ��ġ��
    Vector3 receivePos;
    //�������� �Ѿ���� ȸ����
    Quaternion receiveRot = Quaternion.identity;
    //�����ϴ� �ӷ�
    float lerpSpeed = 50;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        //���࿡ ���� ���� Player ���
        if (photonView.IsMine == true)
        {
            //UI �� ��Ȱ��ȭ ����
            myUI.SetActive(false);
        }

        else
        {
            //nickName ����
            nickName.text = photonView.Owner.NickName;
        }

        //���� PhotonView GameManager�� �˷�����
        GameSetting.instance.AddPlayer(photonView);

        //���۽� ī�޶� ��ġ
        cam.position = camPos[0].position;
    }

    void Update()
    {
        //���� ���� �÷��̾���
        if (photonView.IsMine)
        {
            Move();

            Down();


            //���� ������ ���� �����ִ� �� ���� �����̸� �ȵȴ�.
            //�ִϸ��̼ǿ� Parameter �� ���� -> �ڿ������� �ϱ� ����
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        }

        //else
        //{
        //    //��ġ ����
        //    transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
        //    //ȸ�� ����
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

    [Header("�޸���")]
    public bool run;

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        #region ����1 (���� ��ǥ �̵�)
        /*Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();*/
        #endregion

        //���� ����� 2 -> �÷��̾� �չ����� �������� �̵�. (�����ǥ�� �̵�)
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir2 = dirH + dirV;
        dir2.Normalize();

        //���࿡ ���� ����ִٸ�
        if (cc.isGrounded == true)
        {
            yVelocity = 0;

            if (isJump == true)
            {
                //*���� : ���� �ִϸ��̼�
            }
            isJump = false;
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = jumpPower;

            //*���� : ���� �ִϸ��̼�
            isJump = true;
        }*/

        //�߷� ����
        yVelocity += gravit * Time.deltaTime;

        dir2.y = yVelocity;

        //transform.position += dir2 * speed * Time.deltaTime;
        cc.Move(dir2 * speed * Time.deltaTime);
/*
        //�ִϸ��̼ǿ� Parameter �� ���� -> �ڿ������� �ϱ� ����
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
*/
    }

    void Down()
    {
        //ī�޶� ���� ����
        if (Input.GetKeyDown(KeyCode.C))
        {
            //���� �ִϸ��̼� Down�̶�� flase //�Ͼ��
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Down"))
            {
                anim.SetBool("Down", false);
                CamMove = false;
            }

            //�ɱ�
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
        //�� Player ���
        if (stream.IsWriting)
        {
            //���� ��ġ���� ������.
            stream.SendNext(transform.position);
            //���� ȸ������ ������.
            stream.SendNext(transform.rotation);
            //h �� ������.
            stream.SendNext(h);
            //v �� ������.
            stream.SendNext(v);
        }

        //�� Player �ƴ϶��
        else
        {
            //��ġ���� ����.
            receivePos = (Vector3)stream.ReceiveNext();
            //ȸ������ ����.
            receiveRot = (Quaternion)stream.ReceiveNext();
            //h �� ����.
            h = (float)stream.ReceiveNext();
            //v �� ����.
            v = (float)stream.ReceiveNext();
        }
    }*/
}

