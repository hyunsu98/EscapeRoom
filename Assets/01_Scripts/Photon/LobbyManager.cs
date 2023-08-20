using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public void JoinRoom()
    {
        //�� ���� or ����
        RoomOptions roomOptioin = new RoomOptions();

        //�濡 ���� �� �ִ� �ִ� �ο�
        roomOptioin.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOptioin, TypedLobby.Default);

        //�� ���� ��û
        //PhotonNetwork.JoinRoom(inputRoomName.text + inputPassword.text);
    }

    //�� ���� �Ϸ�� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(nameof(OnCreatedRoom));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print(nameof(OnCreateRoomFailed));

        //�� ���� ���� ������ �����ִ� �˾� ������ ����?
    }

    //�� ���� �Ϸ�� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�� ���� �Ϸ�");

        //GameScene ���� �̵�
        PhotonNetwork.LoadLevel("GameLobbyScene");
    }

    //�濡 ���� �̸��� ������ �Ҷ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //�� ���� ����
        print("�� ���� ���� : " + message);
    }
}
