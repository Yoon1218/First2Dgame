using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    //��������: ���� �̵�����, �ӵ�, ����
    private float x = 0f, y = 0f;
    public float speed = 5f;
    public MeshRenderer MeshRenderer; // ��? why?
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Backgroundmove();
    }

    private void Backgroundmove()
    {
        x += Time.deltaTime * speed;
        //y -= Time.deltaTime * speed;
        MeshRenderer.material.mainTextureOffset = new Vector2(x, y);
        //�޽������� �ȿ� ���͸��� �ȿ� �����ؽ����� ������ = new ����2(x,y);
    }
}
