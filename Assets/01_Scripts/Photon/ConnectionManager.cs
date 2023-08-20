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
        //액션 델리게이트 함수를 담을 수 있는 변수 자료형 //무명함수 //람다식을 이용하여 사용할 수 있음
        inputNickName.onValueChanged.AddListener((string s) => { btnConnect.interactable = s.Length > 0; });

        //inputNickName 에서 엔터 쳤을 때 호출되는 함수 등록
        inputNickName.onSubmit.AddListener(
            (string s) =>
            {
                //버튼이 활성화 되어있다면 그때 호출
                if (btnConnect.interactable)
                {
                    //OnClickConnect 호출
                    OnClickConnect();
                }
            });

        //버튼 비활성
        btnConnect.interactable = false;
    }

    void OnValueChanged(string s)
    {
        btnConnect.interactable = s.Length > 0;

    }

    //버튼 클릭시 연결 요청할 수 있도록
    public void OnClickConnect()
    {
        //서버 접속 요청
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        //닉네임 설정
        PhotonNetwork.NickName = inputNickName.text;

        //기본 로비 진입 요청
        PhotonNetwork.JoinLobby();
    }

    //로비 진입 성공 시
    //옵션 추가 (채널) - 채널별 로비
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        //[참고]실행되고 있는 함수의 이름
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

        //로비 씬으로 이동
        PhotonNetwork.LoadLevel("LobbyScene");
    }
}
