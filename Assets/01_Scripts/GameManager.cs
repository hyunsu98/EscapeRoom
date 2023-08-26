using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using Photon.Realtime;
using System.IO;
using System.Text;


//Json ���� ���� (�̸�/��/��ū/���´���)
[System.Serializable]
public class GameInfo
{
    public string name;
    public int map;
    public int token;
    public bool missionClear;
}

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public GameInfo gameInfo;

    public int token;
    public int maxToken;

    public bool isTimeOver;

    /*public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool MissionClear = false;*/

    //spawnPosGroup Transform
    public Transform trSpawnPosGroup;

    //Spans ��ġ�� ��Ƴ��� ����
    public Vector3[] spawPos;

    //���� ������ ��
    public GameObject Timer;
    public GameObject Rain;
    public GameObject TokenUI;

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
        //PhotonNetwork.Instantiate("Player", spawPos[idx], Quaternion.identity);
        PhotonNetwork.Instantiate("Player", trSpawnPosGroup.position, Quaternion.identity);

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
        }
    }

    //������ Player�� PhotonView �߰�
    public void AddPlayer(PhotonView pv)
    {
        listPlayer.Add(pv);

        //������ > ��� �÷��̾ �����ߴٸ� Turn�� ��������
        if (listPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("�� ���Խ��ϴ�.");
            
            //�ð�, �� ���� �� �ְ� ����
            Timer.SetActive(true);
            Rain.SetActive(true);
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

        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_TOKEN);
        Animator anim = TokenUI.GetComponent<Animator>();
        anim.SetTrigger("IsAction");

        Debug.Log($"��ū ã��{token}");

        if (token == maxToken)
        {
            //��ū �� ã�� �� ã�� ������ UI�߻�
            //Mission3= true;
            Debug.Log($"��ū �� ã�Ҵ�");

            //��ū �� ã���� ����������
            PhotonNetwork.LoadLevel("EndingScene");
        }
    }

    //Ű�� ȹ���� ����
    //Ű�� � ��ü�� ������ �� ���� �� �ְ�
    //�κ��丮��
    public void KeyEat(bool isFindKey)
    {
        //Mission1 = isFindKey;
    }

    [PunRPC]
    public void Mission1Chek(bool isOk)
    {
        //Mission1 = isOk;
    }

    private void Update()
    {

        //��ū �� ã�� ����ã�� �� ������
        /*if(Mission1 && Mission3)
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
        }*/

        if(Input.GetKeyDown(KeyCode.F1))
        {
            JsonSave();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            JsonCall();
        }
    }

    void JsonSave()
    {
        gameInfo = new GameInfo();

        //�� �̸�
        gameInfo.name = PhotonNetwork.LocalPlayer.NickName;
        gameInfo.map = 1;
        gameInfo.token = this.token;
        gameInfo.missionClear = true;

        string jsonData = JsonUtility.ToJson(gameInfo, true);
        Debug.Log(jsonData);

        //���� ����
        FileStream file = new FileStream(Application.dataPath + "/myInfo.txt", FileMode.Create);
        byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
        file.Write(byteData, 0, byteData.Length);
        file.Close();
    }


    void JsonCall()
    {
        //���� ����
        FileStream file = new FileStream(Application.dataPath + "/myInfo.txt", FileMode.Open);
        byte[] byteData = new byte[file.Length];
        file.Read(byteData, 0, byteData.Length);
        file.Close();

        //���� ����
        string jsonData = Encoding.UTF8.GetString(byteData);

        //�ٽ� ������ ��
        gameInfo = JsonUtility.FromJson<GameInfo>(jsonData);
        Debug.Log(jsonData);
    }
}
