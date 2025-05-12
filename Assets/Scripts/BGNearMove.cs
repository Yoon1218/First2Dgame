using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGNearMove : MonoBehaviour
{
    public float speed;
    public Transform tr;
    public BoxCollider2D Box2D;
    private float width;
    void Start()
    {
        tr = GetComponent<Transform>();
        Box2D = GetComponent<BoxCollider2D>();
        speed = 5f;
        width = Box2D.size.x; 
    }

    void Update()
    {
        if(GameManager.Instance.isgameover == true) //게임오버가 되면
        {
            return; //여기서 하위 로직으로 진행 되지 않음
        }
        if (tr.position.x < -width * 1.8f) 
        {
            Vector2 ofsset = new Vector2(width * 2.5f, 0f); 
            tr.position = (Vector2)tr.position + ofsset; 
        }
        tr.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
