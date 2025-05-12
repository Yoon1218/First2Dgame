using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

//

public class Asteroid : MonoBehaviour
{
    public float speed;
    private Transform tr;
    readonly string cointag = "COIN";

    void Start()
    {
        speed = Random.Range(20f, 35f);
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        //              방향 * 속도 = velocity (위치값은 더하거나 뺀다)
        tr.Translate(Vector3.left * speed *  Time.deltaTime);// 왼쪽으로 이동
        if (tr.position.x <= -10f)
            Destroy(this.gameObject);

    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == cointag)
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject, 0.1f);


        }
    }
}
