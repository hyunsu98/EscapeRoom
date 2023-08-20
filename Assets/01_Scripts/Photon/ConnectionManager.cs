using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    //NickName InputField
    public InputField inputNickName;

    //Connect Button
    public Button btnConnect;

    void Start()
    {
        //�׼� ��������Ʈ �Լ��� ���� �� �ִ� ���� �ڷ��� //�����Լ� //���ٽ��� �̿��Ͽ� ����� �� ����
        inputNickName.onValueChanged.AddListener((string s) => { btnConnect.interactable = s.Length > 0; });

        //inputNickName ���� ���� ���� �� ȣ��Ǵ� �Լ� ���
        inputNickName.onSubmit.AddListener(
            (string s) =>
            {
                //��ư�� Ȱ��ȭ �Ǿ��ִٸ� �׶� ȣ��
                if (btnConnect.interactable)
                {
                    //OnClickConnect ȣ��
                    OnClickConnect();
                }
            });

        //��ư ��Ȱ��
        btnConnect.interactable = false;
    }

    void OnValueChanged(string s)
    {
        btnConnect.interactable = s.Length > 0;

    }

    //��ư Ŭ���� ���� ��û�� �� �ֵ���
    public void OnClickConnect()
    {
        //���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        //�г��� ����
        PhotonNetwork.NickName = inputNickName.text;

        //�⺻ �κ� ���� ��û
        PhotonNetwork.JoinLobby();
    }

    //�κ� ���� ���� ��
    //�ɼ� �߰� (ä��) - ä�κ� �κ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        //[����]����ǰ� �ִ� �Լ��� �̸�
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //�κ� ������ �̵�
        PhotonNetwork.LoadLevel("LobbyScene");
    }
}
