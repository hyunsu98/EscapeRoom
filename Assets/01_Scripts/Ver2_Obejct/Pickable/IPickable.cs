using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̵��� ����
public interface IPickable
{
    //��ġ ���
    bool KeepWorldPosition { get; }

    //�Ⱦ� ���
    GameObject PickUp();

}
