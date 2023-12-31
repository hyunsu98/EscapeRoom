using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //마우스 UI (현재 닿으면 E키를 누르세요라고 나옴 -> 마우스를 드래그 하는 동안해 할 것임)
    //※ 가져올때 / 이동할때 / 아무것도 안될때 UI 다르게 변경
    [Header("마우스UI")]
    public GameObject pickUpUI;
    public GameObject dragUI;
    public GameObject opneUI;
    public GameObject baseUI;
    public GameObject tokenUI;

    //토큰 텍스트
    [SerializeField] Text tokenText;

    void Update()
    {
        tokenText.text = GameManager.instance.token.ToString() + " / " + GameManager.instance.maxToken.ToString();
    }

    //UI 초기화 함수
    public void ResetUI()
    {
        baseUI.SetActive(false);
        pickUpUI.SetActive(false);
        dragUI.SetActive(false);
        opneUI.SetActive(false);
        tokenUI.SetActive(false);
    }

    //UI 기본 설정
    public void BaseUI()
    {
        baseUI.SetActive(true);
        pickUpUI.SetActive(false);
        dragUI.SetActive(false);
        opneUI.SetActive(false);
        tokenUI.SetActive(false);
    }
}
