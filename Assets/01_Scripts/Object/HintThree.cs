using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//붓있는 곳
public class HintThree : MonoBehaviour
{
    PickUpObject pick;
    //닿았을 때 힌트 1번 푸는 열쇠라면 애니메이션 실행
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PickUpObject>())
        {
            pick = other.gameObject.GetComponent<PickUpObject>();

            if (pick != null)
            {
                if (pick.finalKey)
                {
                    other.gameObject.GetComponent<HintHighlight>()?.ToggleHighlight(true);


                    //게임오브젝트의 포톤뷰를 보낸다! 힌트를 찾았습니다!
                    var pv = other.gameObject.GetComponent<PhotonView>();

                    if (pv != null)
                    {
                        StartCoroutine("HintStart", pv.Owner.NickName);
                    }
                }
            }
        }
        else
        {
            Debug.Log("안열림");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PickUpObject>())
        {
            if (pick != null)
            {
                if (pick.finalKey)
                {
                    other.gameObject.GetComponent<HintHighlight>()?.ToggleHighlight(false);
                }
            }
        }
        else
        {
            Debug.Log("안열림");
        }
    }

    IEnumerator HintStart(string name)
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("힌트열쇠맞음 애니메이션 실행해라");
        GameManager.instance.MissionThree(name);
        this.gameObject.GetComponent<OpenHighlight>()?.ToggleHighlight(true);
    }
}
