using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�߻� �ִϸ��̼� ����
public class TriggerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    string triggerName;

    public void SetTrigger()
    {
        animator.enabled = true;
        animator.SetTrigger(triggerName);
    }
}
