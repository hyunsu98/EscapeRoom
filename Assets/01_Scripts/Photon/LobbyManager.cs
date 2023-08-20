using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public void JoinRoom()
    {
        //방 생성 or 참여
        RoomOptions roomOptioin = new RoomOptions();

        //방에 들어올 수 있는 최대 인원
        roomOptioin.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOptioin, TypedLobby.Default);

        //방 입장 요청
        //PhotonNetwork.JoinRoom(inputRoomName.text + inputPassword.text);
    }

    //방 생성 완료시 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(nameof(OnCreatedRoom));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print(nameof(OnCreateRoomFailed));

        //방 생성 실패 원인을 보여주는 팝업 띄워줘야 겠죠?
    }

    //방 입장 완료시 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("방 입장 완료");

        //GameScene 으로 이동
        PhotonNetwork.LoadLevel("GameLobbyScene");
    }

    //방에 없는 이름에 들어가려고 할때
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //방 생성 실패
        print("방 입장 실패 : " + message);
    }
}
