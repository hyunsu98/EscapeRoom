using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//발사 애니메이션 실행
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
