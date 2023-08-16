using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵��ӵ�
    [SerializeField] float speed = 5;
    [SerializeField] float camSpeed = 2;

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
    Transform cam;
    bool CamMove;

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

        if(CamMove == true)
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

        //�ִϸ��̼ǿ� Parameter �� ���� -> �ڿ������� �ϱ� ����
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
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
}
