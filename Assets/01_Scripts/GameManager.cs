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

    public bool missionOne = false;
    public bool missionTwo = false;
    public bool missionThree = false;

    public bool MissionClear = false;

    OpenDoor opendoor;

    public string photonName;

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
            //JsonSave();
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(3);
        //토큰 다 찾으면 엔딩씬으로
        PhotonNetwork.LoadLevel("EndingScene");
    }

    public void TokenReset()
    {
        token = 0;
    }

    public void MissionOne(string name)
    {
        if(missionOne == true)
        {
            return;
        }

        //한번만 실행되게
        missionOne = true;

        photonName = name;
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_MissionClear);

        GameSetting.instance.MissionOne(name);
    }

    public void MissionTwo(string name)
    {
        if (missionTwo == true)
        {
            return;
        }

        //한번만 실행되게
        missionTwo = true;

        photonName = name;
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_MissionClear);

        GameSetting.instance.MissionOne(name);
    }

    public void MissionThree(string name)
    {
        if (missionThree == true)
        {
            return;
        }

        //한번만 실행되게
        missionThree = true;

        MissionClear = true;
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_OPENDOOR);
        opendoor = GameSetting.instance.door.GetComponent<OpenDoor>();
        opendoor.isOpen = true;

        StartCoroutine("OpenDoor");
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
