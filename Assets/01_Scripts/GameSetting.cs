using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSetting : MonoBehaviourPunCallbacks
{
    public static GameSetting instance;

    //spawnPosGroup Transform
    public Transform trSpawnPosGroup;

    //Spans ��ġ�� ��Ƴ��� ����
    public Vector3[] spawPos;

    //���� ������ ��
    public GameObject Timer;
    public GameObject Rain;

    public GameObject TokenUI;
    public GameObject MissionUI;

    public GameObject door;

    OpenDoor opendoor;

    //��� Player���� PhotonView�� ������ List
    //new �� �Ҵ����� �ʰ� public �� �س����� ��밡�������� �ƴ� ������ ���� 
    public List<PhotonView> listPlayer = new List<PhotonView>();

    private void Awake()
    {
        if (instance == null)
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

    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);
        GameManager.instance.TokenReset();

        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Locked : ���콺�� Ŀ���� ������ ���߾ӿ� ������Ų �� ������ �ʰ�
        //Confined : ���콺�� Ŀ���� ���� ������ ������ ����� �ʰ� 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked �Ǵ� Confined �Ǿ��� Ŀ���� ������� ������
        //Cursor.lockState = CursorLockMode.None;
        #endregion

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

    public void MissionOne(string name)
    {
        //UI�� ���� ��
        Animator anim = MissionUI.GetComponent<Animator>();
        anim.SetTrigger("IsAction");

        Text missionText = MissionUI.GetComponentInChildren<Text>();

        missionText.text = name + " ����";
    }
}
