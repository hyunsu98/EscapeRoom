using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickHint : MonoBehaviour, IUsable //�������̽� ����
{
    //�ν����Ϳ� �Ӽ��� ����Ǿ� �Ҵ��� �� ����
    [field: SerializeField]
    public UnityEvent Onuse { get; private set; }
    //public UnityEvent Onuse => throw new System.NotImplementedException();

    //������ ������ ������ ���� //��ū
    private int healthBoost = 1;

    public void Use(GameObject actor)
    {
        //�ǰ��ν��͸� ���� ��Ų��.
        //actor.GetComponent<PlayerPickable>().AddHealth(healthBoost);

        GameManager.instance.UpdateToken(1);

        //�̸� ���� �����ϰ� ȣ��� �� �ִ� �Լ�
        //�̺�Ʈ�� �Ҵ�� �޼ҵ尡 �ִ� ��� �� �޼ҵ带 ȣ��.
        //�Դ� �Ҹ� ���
        Onuse?.Invoke();
        //������ �����ؾ���.
        Destroy(gameObject);
    }
}