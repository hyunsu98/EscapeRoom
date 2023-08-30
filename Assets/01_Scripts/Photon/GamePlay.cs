using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;
using System.Text;

public class GamePlay : MonoBehaviourPun
{
    public Text tokenText;
    public GameObject clearUI;

    public void Start()
    {
        tokenText.text = GameManager.instance.token.ToString() + " / " + GameManager.instance.maxToken.ToString();
        if(GameManager.instance.MissionClear == true)
        {
            clearUI.SetActive(true);
        }
    }

    public void MainSceneChange()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);
        photonView.RPC("MoveMainScene", RpcTarget.All);
    }

    [PunRPC]
    void MoveMainScene()
    {
        PhotonNetwork.LoadLevel("GameSceneMain");
    }

    public void MainSceneChange2()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);
        photonView.RPC("MoveMainScene2", RpcTarget.All);
    }

    [PunRPC]
    void MoveMainScene2()
    {
        PhotonNetwork.LoadLevel("GameSceneMain_SSB2");
    }

    //�� ����
    private void Update()
    {
        //tokenText.text = GameManager.instance.token.ToString() + "/" + GameManager.instance.maxToken.ToString();
    }

    /*public void JsonCall()
    {
        GameInfo gameInfo = new GameInfo();

        //���� ����
        FileStream file = new FileStream(Application.dataPath + "/" + PhotonNetwork.LocalPlayer.NickName + ".txt", FileMode.Open);

        if(file != null)
        {
            byte[] byteData = new byte[file.Length];
            file.Read(byteData, 0, byteData.Length);
            file.Close();

            //���� ����
            string jsonData = Encoding.UTF8.GetString(byteData);

            //�ٽ� ������ ��
            gameInfo = JsonUtility.FromJson<GameInfo>(jsonData);
            Debug.Log(jsonData);

            tokenText.text = gameInfo.token.ToString() + "/" + GameManager.instance.maxToken.ToString();
        }
    }*/
}
