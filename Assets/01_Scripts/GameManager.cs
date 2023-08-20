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

        //그렇지 않으면
        else
        {
            //나를 파괴하자 -> 새로만들어진애는 파괴 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Locked : 마우스의 커서를 윈도우 정중앙에 고정시킨 후 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : 마우스의 커서가 게임 윈도우 밖으로 벗어나지 않게 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked 또는 Confined 되었던 커서를 원래대로 돌려줌
        //Cursor.lockState = CursourLockMode.None;
        #endregion
    }

    //토큰 얻기
    public void UpdateToken(int num)
    {
        token += num;
        Debug.Log($"토큰 찾음{token}");

        if (token == maxToken)
        {
            //토큰 다 찾은 것
            //찾을 때마다 UI발생
        }
    }

    //미션 1단계 성공
    public void KeyEat(bool isFindKey)
    {
        Mission1 = isFindKey;
        Debug.Log($"1단계 성공");
    }
}
