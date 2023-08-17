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
        //Locked : 마우스의 커서를 윈도우 정중앙에 고정시킨 후 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;
        #region CursorLockMode : Locked, Confined, None
        //Confined : 마우스의 커서가 게임 윈도우 밖으로 벗어나지 않게 
        //Cursor.lockState = CursorLockMode.Confined;
        //Locked 또는 Confined 되었던 커서를 원래대로 돌려줌
        //Cursor.lockState = CursourLockMode.None;
        #endregion
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            //원래색
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //마우스 클릭시 해당 위치에 UI가 없거나 닿은 물체가 있다면
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            //닿았을 때 위치
            highlight = raycastHit.transform;

            //태그 / 선택된 물체와 닿은 물체가 같지 않으면
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                //닿았을 때의 색이 아니라면
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    //원래색 넣고
                    originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;

                    //닿았을 때 -> 닿을 때 색으로
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }

            else
            {
                highlight = null;
            }
        }

        // Selection
        // 클릭하고 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //닿은 물체라면
            if (highlight)
            {
                //선택한 것이 아니라면
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }

                //선택했을 때
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
                    //다시 원래색
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                }
            }
        }
    }
}