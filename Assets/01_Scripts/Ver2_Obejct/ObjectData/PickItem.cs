using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickItem : MonoBehaviour, IUsable
{
    //���� �̺�Ʈ�� ����ϱ� ����
    //�߻絵 �ϱ� ����
    //���� ���� ������Ʈ ���ο� �Ҹ� ���� ���� �� �ְ�
    //����Ʈ ȿ���� ����

    [field: SerializeField]
    public UnityEvent Onuse { get; private set; }

    public void Use(GameObject actor)
    {
        //Onuse �� null�� �ƴ϶�� Invoke ����
        Onuse?.Invoke();
    }
}

