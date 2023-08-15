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
    Transform cam;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        //���۽� ī�޶� ��ġ
        cam = Camera.main.transform;
        cam.position = camPos[0].position;
    }

    void Update()
    {
        Move();

        Down();
    }

    [Header("�޸���")]
    public bool run;

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

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
        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
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
                cam.position = camPos[0].transform.position;
            }

            else
            {
                anim.SetBool("Down", true);
                cam.position = camPos[1].transform.position;
            }
        }
    }
}
