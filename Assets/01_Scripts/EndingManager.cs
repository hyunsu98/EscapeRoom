using Photon.Pun;
using UnityEngine;


public class EndingManager : MonoBehaviourPun
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        //Cursor.visible = true;

        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_ENDING);
    }

    public void OnClickBack()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.SFX_BUTTON);
        PhotonNetwork.LoadLevel("GameLobbyScene");
    }
}