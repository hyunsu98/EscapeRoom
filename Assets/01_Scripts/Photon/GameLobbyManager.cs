using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameLobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerList _playerList;

    //들어온 플레이어 보일 수만 있게 하면됨.
    public List<PlayerList> _listings = new List<PlayerList>();

    public GameObject roomOption;

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        PlayerList playerList = Instantiate(_playerList, _content);
        if (playerList != null)
        {
            playerList.SetPlayerInfo(player);
            _listings.Add(playerList);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }
}


