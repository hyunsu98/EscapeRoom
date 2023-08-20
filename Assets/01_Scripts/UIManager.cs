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

    //��ū �ؽ�Ʈ
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
