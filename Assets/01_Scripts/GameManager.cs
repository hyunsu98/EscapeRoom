using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public int token;
    public int maxToken;

    public bool isTimeOver;

    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool MissionClear = false;

    //spawnPosGroup Transform
    public Transform trSpawnPosGroup;

    //Spans ��ġ�� ��Ƴ��� ����
    public Vector3[] spawPos;


    public GameObject Timer;
    public GameObject door;

    OpenDoor opendoor;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //�׷��� ������
        else
        {
            //���� �ı����� -> ���θ�������ִ� �ı� 
            Destroy(gameObject);
        }
    }

    //��� Player���� PhotonView�� ������ List
    //new �� �Ҵ����� �ʰ� public �� �س����� ��밡�������� �ƴ� ������ ���� 
    public List<PhotonView> listPlayer = new List<PhotonView>();

    private void Start()
    {
        //RPC ȣ�� ��
        PhotonNetwork.SendRate = 30;

        //OnPhotonSerializeView ȣ�� ��
        PhotonNetwork.SerializationRate = 30;

        SetSpawnPos();

        //���� ��ġ�ؾ� �ϴ� idx ������
        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        //���� Player ����
        PhotonNetwork.Instantiate("Player", spawPos[idx], Quaternion.identity);


        //Locked : ���콺�� Ŀ���� ������ ���߾ӿ� ������Ų �� ������ �ʰ�
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : ���콺�� Ŀ���� ���� ������ ������ ����� �ʰ� 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked �Ǵ� Confined �Ǿ��� Ŀ���� ������� ������
        //Cursor.lockState = CursourLockMode.None;
        #endregion
    }


    //���� �������� �ο�����ŭ ����ؼ� ������Ŵ
    void SetSpawnPos()
    {
        //�ִ� �ο� ��ŭ spawnPos�� ������ �Ҵ�
        spawPos = new Vector3[PhotonNetwork.CurrentRoom.MaxPlayers];

        //count ���߿� �ο� �� ������ ����
        //���� (angle) 
        float angle = 360 / spawPos.Length;

        for (int i = 0; i < spawPos.Length; i++)
        {
            trSpawnPosGroup.Rotate(0, angle, 0);
            //�� ��ġ�� �������� 360 ������ ����
            //360���� ���� ����ŭ ��ġ �� �� �ְ�
            spawPos[i] = trSpawnPosGroup.position + trSpawnPosGroup.forward * 2; //�Ÿ�

            //���������� �ȸ��� ����
            /*GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

            go.transform.position = spawPos[i];*/
        }
    }

    //������ Player�� PhotonView �߰�
    public void AddPlayer(PhotonView pv)
    {
        listPlayer.Add(pv);

        //������ > ��� �÷��̾ �����ߴٸ� Turn�� ��������
        if (listPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            //Turn �� ��������
            //�ð��� �� �� �ְ�!
            Timer.SetActive(true);
            Debug.Log("���� ����1");

            //Timer.instance.Being();
        }
    }

    //���ο� �ο��� �濡 �������� ȣ��Ǵ� �Լ� -> puncallback ��ӹ޾ƾ� ��
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print(newPlayer.NickName + "���� ���Խ��ϴ�!"); // UI�� �����ϸ��
    }

    //��ū ���
    public void UpdateToken(int num)
    {
        token += num;
        Debug.Log($"��ū ã��{token}");

        if (token == maxToken)
        {
            //��ū �� ã�� ��
            //ã�� ������ UI�߻�
            Mission3 = true;
        }
    }

    //�̼� 1�ܰ� ����
    public void KeyEat(bool isFindKey)
    {
        //Debug.Log($"��� 1�ܰ� ����");
        Mission1 = isFindKey;
        //photonView.RPC(nameof(Mission1Chek), RpcTarget.All, isFindKey);
    }

    [PunRPC]
    public void Mission1Chek(bool isOk)
    {
        Mission1 = isOk;
    }

    private void Update()
    {
       /* if (isTimeOver)
        {
            Debug.Log("����1");
            if (Mission2)
            {
                //��������
                opendoor = door.GetComponent<OpenDoor>();
                opendoor.isOpen = true;

                //������
                PhotonNetwork.LoadLevel("EndingScene");
            }

            else
            {
                Debug.Log("����2");
            }
        }*/

        //��ū �� ã�� ����ã�� �� ������
        if(Mission1 && Mission3)
        {
            //���� ������ ��!
            opendoor = door.GetComponent<OpenDoor>();
            opendoor.isOpen = true;

            MissionClear = true;

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                //������
                PhotonNetwork.LoadLevel("EndingScene");
            }
        }
    }

    public void Ending()
    {
        PhotonNetwork.LoadLevel("EndingScene");
    }
}
