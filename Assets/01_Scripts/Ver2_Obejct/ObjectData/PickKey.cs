using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickKey : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent Onuse { get; private set; }

    public void Use(GameObject actor)
    {
        //�̼�1 Ŭ����
        //GameManager.instance.KeyEat(true);

        //�̸� ���� �����ϰ� ȣ��� �� �ִ� �Լ�
        //�̺�Ʈ�� �Ҵ�� �޼ҵ尡 �ִ� ��� �� �޼ҵ带 ȣ��.
        //�Դ� �Ҹ� ���
        Onuse?.Invoke();

        //������ �����ؾ���.
        Destroy(gameObject);
    }
}
