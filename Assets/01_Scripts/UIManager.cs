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

    //���콺 UI (���� ������ EŰ�� ���������� ���� -> ���콺�� �巡�� �ϴ� ������ �� ����)
    //�� �����ö� / �̵��Ҷ� / �ƹ��͵� �ȵɶ� UI �ٸ��� ����
    [Header("���콺UI")]
    public GameObject pickUpUI;
    public GameObject dragUI;
    public GameObject opneUI;
    public GameObject baseUI;

    //��ū �ؽ�Ʈ
    [SerializeField] Text tokenText;

    void Update()
    {
        tokenText.text = GameManager.instance.token.ToString() + " / " + GameManager.instance.maxToken.ToString();
    }

    //UI �ʱ�ȭ �Լ�
    public void ResetUI()
    {
        baseUI.SetActive(false);
        pickUpUI.SetActive(false);
        dragUI.SetActive(false);
        opneUI.SetActive(false);
    }

    //UI �⺻ ����
    public void BaseUI()
    {
        baseUI.SetActive(true);
        pickUpUI.SetActive(false);
        dragUI.SetActive(false);
        opneUI.SetActive(false);
    }
}
