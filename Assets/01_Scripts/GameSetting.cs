using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSetting : MonoBehaviour
{

    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);

        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Locked : 마우스의 커서를 윈도우 정중앙에 고정시킨 후 보이지 않게
        //Confined : 마우스의 커서가 게임 윈도우 밖으로 벗어나지 않게 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked 또는 Confined 되었던 커서를 원래대로 돌려줌
        //Cursor.lockState = CursorLockMode.None;
        #endregion
    }

    private void Update()
    {
       
    }
}
