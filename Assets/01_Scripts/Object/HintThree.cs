using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ִ� ��
public class HintThree : MonoBehaviour
{
    PickUpObject pick;
    //����� �� ��Ʈ 1�� Ǫ�� ������ �ִϸ��̼� ����
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


                    //���ӿ�����Ʈ�� ����並 ������! ��Ʈ�� ã�ҽ��ϴ�!
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
            Debug.Log("�ȿ���");
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
            Debug.Log("�ȿ���");
        }
    }

    IEnumerator HintStart(string name)
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("��Ʈ������� �ִϸ��̼� �����ض�");
        GameManager.instance.MissionThree(name);
        this.gameObject.GetComponent<OpenHighlight>()?.ToggleHighlight(true);
    }
}
