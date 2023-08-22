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

    //Spans 위치를 담아놓을 변수
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

        //그렇지 않으면
        else
        {
            //나를 파괴하자 -> 새로만들어진애는 파괴 
            Destroy(gameObject);
        }
    }

    //모든 Player들의 PhotonView를 가지는 List
    //new 로 할당하지 않고 public 을 해놓으면 사용가능하지만 아님 사용되지 않음 
    public List<PhotonView> listPlayer = new List<PhotonView>();

    private void Start()
    {
        //RPC 호출 빈도
        PhotonNetwork.SendRate = 30;

        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 30;

        SetSpawnPos();

        //내가 위치해야 하는 idx 구하자
        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        //나의 Player 생성
        PhotonNetwork.Instantiate("Player", spawPos[idx], Quaternion.identity);


        //Locked : 마우스의 커서를 윈도우 정중앙에 고정시킨 후 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : 마우스의 커서가 게임 윈도우 밖으로 벗어나지 않게 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked 또는 Confined 되었던 커서를 원래대로 돌려줌
        //Cursor.lockState = CursourLockMode.None;
        #endregion
    }


    //적을 기준으로 인원수만큼 계산해서 생성시킴
    void SetSpawnPos()
    {
        //최대 인원 만큼 spawnPos의 공간을 할당
        spawPos = new Vector3[PhotonNetwork.CurrentRoom.MaxPlayers];

        //count 나중에 인원 수 가져올 에정
        //간격 (angle) 
        float angle = 360 / spawPos.Length;

        for (int i = 0; i < spawPos.Length; i++)
        {
            trSpawnPosGroup.Rotate(0, angle, 0);

            //이 위치를 기준으로 360 돌려서 생성
            //360도로 들어온 수만큼 배치 될 수 있게
            spawPos[i] = trSpawnPosGroup.position + trSpawnPosGroup.forward * 2; //거리
        }
    }

    //참여한 Player의 PhotonView 추가
    public void AddPlayer(PhotonView pv)
    {
        listPlayer.Add(pv);

        //지금은 > 모든 플레이어가 참여했다면 Turn을 시작하자
        if (listPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            //Turn 을 시작하자
            //시간이 갈 수 있게!
            Debug.Log("다 들어왔습니다.");
            Timer.SetActive(true);

            //Timer.instance.Being();
        }
    }

    //새로운 인원이 방에 들어왔을때 호출되는 함수 -> puncallback 상속받아야 함
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print(newPlayer.NickName + "님이 들어왔습니다!"); // UI로 구성하면됨
    }

    //토큰 얻기
    public void UpdateToken(int num)
    {
        token += num;
        Debug.Log($"토큰 찾음{token}");

        if (token == maxToken)
        {
            //토큰 다 찾은 것 찾을 때마다 UI발생
            Mission3= true;
        }
    }

    //키를 획득한 상태
    //키가 어떤 객체와 닿으면 문 열릴 수 있게
    public void KeyEat(bool isFindKey)
    {
        Mission1 = isFindKey;
    }

    [PunRPC]
    public void Mission1Chek(bool isOk)
    {
        Mission1 = isOk;
    }

    private void Update()
    {

        //토큰 다 찾고 열쇠찾고 문 열리면
        if(Mission1 && Mission3)
        {
            //문을 설정해 둠!
            opendoor = door.GetComponent<OpenDoor>();
            opendoor.isOpen = true;

            MissionClear = true;

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                //문열림
                PhotonNetwork.LoadLevel("EndingScene");
            }
        }
    }

    public void Ending()
    {
        PhotonNetwork.LoadLevel("EndingScene");
    }
}
