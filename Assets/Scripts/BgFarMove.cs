using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BgFarMove : MonoBehaviour
{
    public float speed;
    public Transform tr;
    public BoxCollider2D Box2D;
    private float width; // �� �ʺ�
    void Start()
    {
        tr = GetComponent<Transform>();
        Box2D = GetComponent<BoxCollider2D>();
        speed = 5f;
        width = Box2D.size.x; // �ڽ� �ݶ��̴��� ������ x���� �ʺ�� ����
    }

    void Update()
    {
        if (GameManager.Instance.isgameover == true) //���ӿ����� �Ǹ�
        {
            return; //���⼭ ���� �������� ���� ���� ����
        }
        RePosition();
    }

    private void RePosition()
    {
        if (tr.position.x < -width * 1.8f) // Ʈ�������� ������ x���� �ʺ񺸴� ������
        {
            Vector2 ofsset = new Vector2(width * 2.5f, 0f); // �������� �ʺ��� 2.5f�� ����
            tr.position = (Vector2)tr.position + ofsset; // Ʈ�������� �����ǿ� �������� ����
        }
        tr.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
