using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSetting : MonoBehaviour
{
    void Start()
    {
        //���콺 �����͸� ��Ȱ��ȭ
        //Cursor.visible = false;

        SoundManager.instance.PlayBGM(SoundManager.EBgm.BGM_GAME);

        //Locked : ���콺�� Ŀ���� ������ ���߾ӿ� ������Ų �� ������ �ʰ�
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : ���콺�� Ŀ���� ���� ������ ������ ����� �ʰ� 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked �Ǵ� Confined �Ǿ��� Ŀ���� ������� ������
        //Cursor.lockState = CursorLockMode.None;
        #endregion
    }

    private void Update()
    {
        //Cursor.visible = false;

        /*//���콺 Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            //���콺 Ŭ���� �ش� ��ġ�� UI�� ������
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //���콺 �����͸� ��Ȱ��ȭ
                Cursor.visible = false;
            }
        }*/
    }
}
