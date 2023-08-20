using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public static Timer instance;

    /*private void Awake()
    {
        if(instance = null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    [SerializeField] Text uiText;

    public int Duration;

    int remainingDuration;

    public bool istimr;

    void Start()
    {
        Being(Duration);
        //remainingDuration = Duration;
    }

    void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
        Debug.Log("게임 시작2");
    }

/*    public void Being()
    {
        //remainingDuration = Duration;
        StartCoroutine(UpdateTimer());
    }*/

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        onEnd();
    }

    private void onEnd()
    {
        print("End");
        GameManager.instance.isTimeOver = true;
    }
}
