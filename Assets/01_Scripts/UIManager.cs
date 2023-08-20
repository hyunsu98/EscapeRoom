using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //토큰 텍스트
    [SerializeField] Text tokenText;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        tokenText.text = GameManager.instance.token.ToString() + " / " + GameManager.instance.maxToken.ToString();
    }
}
