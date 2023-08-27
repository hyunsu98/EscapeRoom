using Photon.Pun;
using System.Collections;
using UnityEngine;

//도화지 있는 곳
public class Hint : MonoBehaviour
{
    public Animator hintAnim;
    public GameObject key2;
    public GameObject Door;

    PickUpObject pick;
    //닿았을 때 힌트 1번 푸는 열쇠라면 애니메이션 실행
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PickUpObject>())
        {
            pick = other.gameObject.GetComponent<PickUpObject>();

            if (pick != null)
            {
                if (pick.hintOne)
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PickUpObject>())
        {
            if (pick != null)
            {
                if (pick.hintOne)
                {
                    other.gameObject.GetComponent<HintHighlight>()?.ToggleHighlight(false);
                }
            }
        }
    }

    IEnumerator HintStart(string name)
    {
        yield return new WaitForSeconds(1);

        Debug.Log("힌트열쇠맞음 애니메이션 실행해라");
        hintAnim.SetBool("HintOpen", true);
        key2.SetActive(true);
        Door.SetActive(true);
        GameManager.instance.MissionOne(name);
    }
}
