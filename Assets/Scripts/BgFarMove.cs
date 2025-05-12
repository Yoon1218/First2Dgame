using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BgFarMove : MonoBehaviour
{
    public float speed;
    public Transform tr;
    public BoxCollider2D Box2D;
    private float width; // 폭 너비
    void Start()
    {
        tr = GetComponent<Transform>();
        Box2D = GetComponent<BoxCollider2D>();
        speed = 5f;
        width = Box2D.size.x; // 박스 콜라이더의 사이즈 x값을 너비로 지정
    }

    void Update()
    {
        if (GameManager.Instance.isgameover == true) //게임오버가 되면
        {
            return; //여기서 하위 로직으로 진행 되지 않음
        }
        RePosition();
    }

    private void RePosition()
    {
        if (tr.position.x < -width * 1.8f) // 트랜스폼의 포지션 x값이 너비보다 작으면
        {
            Vector2 ofsset = new Vector2(width * 2.5f, 0f); // 오프셋을 너비의 2.5f로 지정
            tr.position = (Vector2)tr.position + ofsset; // 트랜스폼의 포지션에 오프셋을 더함
        }
        tr.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
