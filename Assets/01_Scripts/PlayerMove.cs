using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵��ӵ�
    [SerializeField] float speed = 5;

    //ī�޶� ��ġ
    [SerializeField] Transform[] camPos;

    //�߷�
    float gravit = -9.81f;
    //y�ӷ�
    float yVelocity = 0;
    //���� ��
    float jumpPower = 5;
    //���� ����
    bool isJump = false;

    //����
    float h;
    float v;

    Animator anim;
    CharacterController cc;

    //ī�޶� Transform
    public Transform trCam;

    bool CamMove = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        //���۽� ī�޶� ��ġ
        //cam = Camera.main.transform;
        trCam.position = camPos[0].position;
    }

    void Update()
    {

        Move();

        Down();

        if(CamMove == true)
        {
            trCam.position = Vector3.Lerp(trCam.position, camPos[0].position, 2 * Time.deltaTime);
        }

        else
        {
            trCam.position = Vector3.Lerp(trCam.position, camPos[1].position, 2 * Time.deltaTime);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = jumpPower;

            //*���� : ���� �ִϸ��̼�

            isJump = true;
        }

        //�߷� ����
        yVelocity += gravit * Time.deltaTime;

        dir2.y = yVelocity;

        //transform.position += dir2 * speed * Time.deltaTime;
        cc.Move(dir2 * speed * Time.deltaTime);

        //�ִϸ��̼ǿ� Parameter �� ����
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }

    void Down()
    {
        //ī�޶� ���� ����
        if (Input.GetKeyDown(KeyCode.C))
        {
            //���� �ִϸ��̼� Down�̶�� flase
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Down"))
            {
                anim.SetBool("Down", false);
                CamMove = true;
            }

            else
            {
                anim.SetBool("Down", true);
                CamMove = false;
            }
        }
    }
}
