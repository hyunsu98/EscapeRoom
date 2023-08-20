using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int token;
    public int maxToken;

    public bool Mission1 = false;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //�׷��� ������
        else
        {
            //���� �ı����� -> ���θ�������ִ� �ı� 
            Destroy(gameObject);
        }
    }

    private void Start()
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

    //��ū ���
    public void UpdateToken(int num)
    {
        token += num;
        Debug.Log($"��ū ã��{token}");

        if (token == maxToken)
        {
            //��ū �� ã�� ��
            //ã�� ������ UI�߻�
        }
    }

    //�̼� 1�ܰ� ����
    public void KeyEat(bool isFindKey)
    {
        Mission1 = isFindKey;
        Debug.Log($"1�ܰ� ����");
    }
}
