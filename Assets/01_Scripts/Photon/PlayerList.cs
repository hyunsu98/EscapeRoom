using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    //생성자
    public Player Player { get; private set; }


    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _text.text = player.NickName;

        print("들어온 플레이어" + player.NickName);
        // 들어온 플레이어의 이미지를 요청한다
        // 저장된 텍스트랑 입력된 텍스트가 같으면 사진을 불러온다
    }
}
