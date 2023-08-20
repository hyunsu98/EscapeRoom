using System.Collections.Generic;
using UnityEngine;

public class OpenHighlight : MonoBehaviour
{
    //������ ���ϰ� �� ����.
    //�� ���η����� �׵θ��� ���ϰ� ����
    [SerializeField]
    private List<Renderer> renderers;

    //������ ��
    [SerializeField]
    private Color color = Color.white;

    //������ ���͸��� �迭 �� 
    [SerializeField]
    private int num;

    //Material�� ����
    private List<Material> materials;

    private void Awake()
    {
        materials = new List<Material>();

        foreach (var renderer in renderers)
        {
            //Randerer -> Materials(������ ������ ���� �� ����)
            //Add -> ��� �ϳ�. AddRange�� ����(�迭, List ��) �߰�
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    //�÷��̾�� Ray�� �� ������ �� ����.
    public void ToggleHighlight(bool val)
    {
        // ������
        if (val)
        {
            //Emission�� �������� �����ϰ� ����.

            //Ȱ��ȭ �� Ű����
            //�� �� ������Ʈ�� Ȱ��ȭ �Ǿ���
            materials[num].EnableKeyword("_EMISSION");
            //���� �����ϱ� ���� 
            materials[num].SetColor("_EmissionColor", color);
        }

        //������
        else
        {
            //REMINSE�� ��Ȱ��ȭ�ϸ� �˴ϴ�
            //�ٸ� ������ ���� ������ ������� ������
            materials[num].DisableKeyword("_EMISSION");
        }
    }
}
