using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    //변수지정: 어디로 이동할지, 속도, 방향
    private float x = 0f, y = 0f;
    public float speed = 5f;
    public MeshRenderer MeshRenderer; // 왜? why?
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
        //메쉬랜더러 안에 머터리얼 안에 메인텍스쳐의 오프셋 = new 벡터2(x,y);
    }
}
