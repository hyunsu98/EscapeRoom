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
        //Locked : ���콺�� Ŀ���� ������ ���߾ӿ� ������Ų �� ������ �ʰ�
        //Confined : ���콺�� Ŀ���� ���� ������ ������ ����� �ʰ� 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked �Ǵ� Confined �Ǿ��� Ŀ���� ������� ������
        //Cursor.lockState = CursorLockMode.None;
        #endregion
    }

    private void Update()
    {
       
    }
}
