using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using Photon.Realtime;
using System.IO;
using System.Text;


//Json 저장 내용 (이름/맵/토큰/끝냈는지)
[System.Serializable]
public class GameInfo
{
    public string name;
    public int map;
    public int token;
    public bool missionClear;
}

public class GameManager : MonoBehaviour
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

    public bool MissionClear = false;

    OpenDoor opendoor;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //그렇지 않으면
        else
        {
            //나를 파괴하자 -> 새로만들어진애는 파괴 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    //토큰 얻기
    public void UpdateToken(int num)
    {
        token += num;

        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_TOKEN);
        Animator anim = GameSetting.instance.TokenUI.GetComponent<Animator>();

        anim.SetTrigger("IsAction");

        Debug.Log($"토큰 찾음{token}");

        if (token == maxToken)
        {
            //Mission3= true;
            Debug.Log($"토큰 다 찾았다");

            MissionClear = true;
            //JsonSave();

            SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_OPENDOOR);
            opendoor = GameSetting.instance.door.GetComponent<OpenDoor>();
            opendoor.isOpen = true;


            StartCoroutine("OpenDoor");
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(3);
        //토큰 다 찾으면 엔딩씬으로
        PhotonNetwork.LoadLevel("EndingScene");
    }

    //키를 획득한 상태
    //키가 어떤 객체와 닿으면 문 열릴 수 있게
    //인벤토리로
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

    }

    public void TokenReset()
    {
        token = 0;
    }

    public void JsonSave()
    {
        gameInfo = new GameInfo();

        //내 이름
        gameInfo.name = PhotonNetwork.LocalPlayer.NickName;
        gameInfo.map = 1;
        gameInfo.token = this.token;
        gameInfo.missionClear = MissionClear;

        string jsonData = JsonUtility.ToJson(gameInfo, true);
        Debug.Log(jsonData);

        //파일 저장
        FileStream file = new FileStream(Application.dataPath + "/"+ PhotonNetwork.LocalPlayer.NickName+".txt", FileMode.Create);
        byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
        file.Write(byteData, 0, byteData.Length);
        file.Close();
    }

    public void JsonCall()
    {
        //파일 열기
        FileStream file = new FileStream(Application.dataPath + "/" + PhotonNetwork.LocalPlayer.NickName + ".txt", FileMode.Open);
        byte[] byteData = new byte[file.Length];
        file.Read(byteData, 0, byteData.Length);
        file.Close();

        //정보 셋팅
        string jsonData = Encoding.UTF8.GetString(byteData);

        //다시 담아줘야 함
        gameInfo = JsonUtility.FromJson<GameInfo>(jsonData);
        Debug.Log(jsonData);
    }
}
