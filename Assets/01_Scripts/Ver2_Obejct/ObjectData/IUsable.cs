using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//�������̽� , ��ü����,  �߻�ȭ
//�������̽� : �޼���, �Ӽ�, �̺�Ʈ, �Ӽ����� ������, �̸� ���� �������� �ʰ� ���� ���Ǹ� ���´�.
//�߻� ����θ� ������ �߻� Base Ŭ������ ���������� ����
//��� Ʈ���� ����

//��ư Ŭ���� ����
public interface IUsable
{
    //���ӿ�����Ʈ�� ���
    void Use(GameObject actor);

    //���� �̺�Ʈ�� ���� �� (�Ӽ�)
    //using UnityEngine.Events; ����
    UnityEvent Onuse { get; }
}