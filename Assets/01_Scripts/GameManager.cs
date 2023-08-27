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

        //�׷��� ������
        else
        {
            //���� �ı����� -> ���θ�������ִ� �ı� 
            Destroy(gameObject);
        }
    }

    //��ū ���
    public void UpdateToken(int num)
    {
        token += num;

        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_TOKEN);
        Animator anim = GameSetting.instance.TokenUI.GetComponent<Animator>();

        anim.SetTrigger("IsAction");

        Debug.Log($"��ū ã��{token}");

        if (token == maxToken)
        {
            //Mission3= true;
            Debug.Log($"��ū �� ã�Ҵ�");
            //JsonSave();
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(3);
        //��ū �� ã���� ����������
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

        //�ѹ��� ����ǰ�
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

        //�ѹ��� ����ǰ�
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

        //�ѹ��� ����ǰ�
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

        //�� �̸�
        gameInfo.name = PhotonNetwork.LocalPlayer.NickName;
        gameInfo.map = 1;
        gameInfo.token = this.token;
        gameInfo.missionClear = MissionClear;

        string jsonData = JsonUtility.ToJson(gameInfo, true);
        Debug.Log(jsonData);

        //���� ����
        FileStream file = new FileStream(Application.dataPath + "/"+ PhotonNetwork.LocalPlayer.NickName+".txt", FileMode.Create);
        byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
        file.Write(byteData, 0, byteData.Length);
        file.Close();
    }

    public void JsonCall()
    {
        //���� ����
        FileStream file = new FileStream(Application.dataPath + "/" + PhotonNetwork.LocalPlayer.NickName + ".txt", FileMode.Open);
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
