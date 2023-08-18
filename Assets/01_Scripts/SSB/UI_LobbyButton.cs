using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//부모오브젝트가 켜졌을 때 회전하면서 원래 크기로 되고싶다
public class UI_LobbyButton : MonoBehaviour
{
    Vector3 originPos;
    Quaternion originRot;
    Vector3 originScale;
    GameObject parentObj;
    void Start()
    {
        //원래 위치 저장
        originPos = transform.position;
        //원래 회전 저장
        originRot = transform.rotation;
        //원래 크기 저장
        originScale = transform.localScale;

        //부모 오브젝트
        parentObj = transform.parent.gameObject;

        //현재 위치를 변경한다
        transform.position -= Vector3.right * 200;
        //현재 회전값을 변경한다
        transform.rotation = Quaternion.Euler(0, 0, 20);
        //현재 크기값을 변경한다
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //부모 오브젝트가 켜져있다면
        if(parentObj.activeSelf)
        {
            //원래 위치로 돌아온다
            transform.position = Vector3.Lerp(transform.position, originPos, 10 * Time.deltaTime);
            //원래 회전값으로 돌아온다
            transform.rotation = Quaternion.Lerp(transform.rotation, originRot, 8 * Time.deltaTime);
            //원래 크기값으로 돌아온다 
            transform.localScale = Vector3.Lerp(transform.localScale, originScale, 20 * Time.deltaTime);
        }
    }

}
