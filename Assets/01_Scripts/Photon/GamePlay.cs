using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GamePlay : MonoBehaviourPun
{
    public void MainSceneChange()
    {
        photonView.RPC("MoveMainScene", RpcTarget.All);
    }
    [PunRPC]
    void MoveMainScene()
    {
        PhotonNetwork.LoadLevel("GameSceneMain");
    }
}
