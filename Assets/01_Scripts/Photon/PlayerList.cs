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

    //������
    public Player Player { get; private set; }


    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _text.text = player.NickName;

        print("���� �÷��̾�" + player.NickName);
        // ���� �÷��̾��� �̹����� ��û�Ѵ�
        // ����� �ؽ�Ʈ�� �Էµ� �ؽ�Ʈ�� ������ ������ �ҷ��´�
    }
}
