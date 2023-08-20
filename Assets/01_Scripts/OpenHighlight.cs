using System.Collections.Generic;
using UnityEngine;

public class OpenHighlight : MonoBehaviour
{
    //렌더러 변하게 할 것임.
    //※ 라인렌더러 테두리만 변하게 변경
    [SerializeField]
    private List<Renderer> renderers;

    //변경할 색
    [SerializeField]
    private Color color = Color.white;

    //변경할 메터리얼 배열 수 
    [SerializeField]
    private int num;

    //Material를 변경
    private List<Material> materials;

    private void Awake()
    {
        materials = new List<Material>();

        foreach (var renderer in renderers)
        {
            //Randerer -> Materials(하위에 여러개 있을 수 있음)
            //Add -> 요소 하나. AddRange는 범위(배열, List 등) 추가
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    //플레이어에서 Ray쏜 후 설정해 줄 것임.
    public void ToggleHighlight(bool val)
    {
        // 켜지기
        if (val)
        {
            //Emission을 동적으로 변경하고 싶음.

            //활성화 할 키워드
            //※ 이 오브젝트가 활성화 되었다
            materials[num].EnableKeyword("_EMISSION");
            //색을 설정하기 전에 
            materials[num].SetColor("_EmissionColor", color);
        }

        //꺼지기
        else
        {
            //REMINSE를 비활성화하면 됩니다
            //다른 곳에서 배출 색상을 사용하지 않으면
            materials[num].DisableKeyword("_EMISSION");
        }
    }
}
