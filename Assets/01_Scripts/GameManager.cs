using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        //Locked : ���콺�� Ŀ���� ������ ���߾ӿ� ������Ų �� ������ �ʰ�
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : ���콺�� Ŀ���� ���� ������ ������ ����� �ʰ� 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked �Ǵ� Confined �Ǿ��� Ŀ���� ������� ������
        //Cursor.lockState = CursourLockMode.None;
        #endregion
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
