using Photon.Pun;
using System.Collections;
using UnityEngine;

//��ȭ�� �ִ� ��
public class Hint : MonoBehaviour
{
    public Animator hintAnim;
    public GameObject key2;
    public GameObject Door;

    PickUpObject pick;
    //����� �� ��Ʈ 1�� Ǫ�� ������ �ִϸ��̼� ����
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

                    //���ӿ�����Ʈ�� ����並 ������! ��Ʈ�� ã�ҽ��ϴ�!
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

        Debug.Log("��Ʈ������� �ִϸ��̼� �����ض�");
        hintAnim.SetBool("HintOpen", true);
        key2.SetActive(true);
        Door.SetActive(true);
        GameManager.instance.MissionOne(name);
    }
}
