using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterialHighlight;
    private Material originalMaterialSelection;

    private Transform highlight;
    private Transform selection;

    private RaycastHit raycastHit;

    private void Start()
    {
        //Locked : ���콺�� Ŀ���� ������ ���߾ӿ� ������Ų �� ������ �ʰ�
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : ���콺�� Ŀ���� ���� ������ ������ ����� �ʰ� 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked �Ǵ� Confined �Ǿ��� Ŀ���� ������� ������
        //Cursor.lockState = CursourLockMode.None;
        #endregion
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            //������
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //���콺 Ŭ���� �ش� ��ġ�� UI�� ���ų� ���� ��ü�� �ִٸ�
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            //����� �� ��ġ
            highlight = raycastHit.transform;

            //�±� / ���õ� ��ü�� ���� ��ü�� ���� ������
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                //����� ���� ���� �ƴ϶��
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    //������ �ְ�
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;

                    //����� �� -> ���� �� ������
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }

            else
            {
                highlight = null;
            }
        }

        // Selection
        // Ŭ���ϰ� 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //���� ��ü���
            if (highlight)
            {
                //������ ���� �ƴ϶��
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }

                //�������� ��
                selection = raycastHit.transform;

                if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                {  
                    originalMaterialSelection = originalMaterialHighlight;

                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                }
                highlight = null;
            }

            else
            {
                if (selection)
                {
                    //�ٽ� ������
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                }
            }
        }
    }
}