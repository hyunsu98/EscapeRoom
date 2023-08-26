using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSetting : MonoBehaviour
{
    void Start()
    {
        //마우스 포인터를 비활성화
        //Cursor.visible = false;

        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);

        //Locked : 마우스의 커서를 윈도우 정중앙에 고정시킨 후 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : 마우스의 커서가 게임 윈도우 밖으로 벗어나지 않게 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked 또는 Confined 되었던 커서를 원래대로 돌려줌
        //Cursor.lockState = CursorLockMode.None;
        #endregion
    }

    private void Update()
    {
        //Cursor.visible = false;

        /*//마우스 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭시 해당 위치에 UI가 없으면
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //마우스 포인터를 비활성화
                Cursor.visible = false;
            }
        }*/
    }
}
