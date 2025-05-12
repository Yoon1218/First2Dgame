using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketMove : MonoBehaviour
{
    private float h = 0f; // A,D Ű�� �ޱ� ���� ����
    private float v = 0f; //W,S Ű�� �ޱ� ���� ����
    public float speed = 5f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source; // ����� �ҽ�
    public AudioClip hitclip; //����� Ŭ��
    private Vector2 startpos = Vector2.zero;//������ġ
    public Transform firepos;
    public GameObject coinPrefab;
    void Start()
    {
        source = GetComponent<AudioSource>(); // ������ҽ� ������Ʈ ��������
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (GameManager.Instance.isgameover == true) //���ӿ����� �Ǹ�
        {
            return; //���⼭ ���� �������� ���� ���� ����
        }
            if(Application.platform == RuntimePlatform.WindowsEditor) //windows�÷����϶�
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
                Vector3 normal = (h * Vector3.right) + (v * Vector3.up);
                tr.Translate(normal.normalized * speed * Time.deltaTime);
        }
            if(Application.platform == RuntimePlatform.Android) // �ȵ���̵� �÷����϶�
        {
            TouchMove();
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer) //iphone  �÷����϶�
        {
            TouchMove();
        }

        //����ȭ 2���� Ű�� ���ÿ� ������ ���� ������ �����̰�.
        // tr.Translate(Vector3.up*v*speed*Time.deltaTime);
        #region �ʺ��� ����
        // if (tr.position.x>7.83f)
        //     tr.position = new Vector3(7.83f,tr.position.y,tr.position.z);
        //else if (tr.position.x<-7.83f)
        //     tr.position = new Vector3(-7.83f,tr.position.y,tr.position.z) ;
        //if(tr.position.y> 4.51f)
        //     tr.position = new Vector3(tr.position.x,4.51f, tr.position.z);
        //else if(tr.position.y<-3.46f)
        //     tr.position = new Vector3(tr.position.x, -3.46f, tr.position.z);
        #endregion
        #region �߱��� ����
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7.83f, 7.83f), Mathf.Clamp(tr.position.y, -3.46f, 4.51f), tr.position.z);
        //����Ŭ����(mathf.clamp(what?,min,max) // ���� �����ϴ� �Լ�
        #endregion


    }

    private void TouchMove()
    {
        if (Input.touchCount > 0)//�ѹ��̶� ��ġ�� �Ǿ��ٸ�..
        {
            Touch touch = Input.GetTouch(0); // ��ġ�� ������ ������
                                             //gettouch(0) // ��ġ�� ��ġ���� �迭�� ���� �Ǿ��µ� ù��°�� ��ġ�� ��ġ�� ������
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startpos = touch.position;
                    break;
                case TouchPhase.Moved:
                    Vector3 touchdelta = touch.position - startpos; // ��ġ�� ��ġ - ������ġ�� �Ÿ��� ����
                    Vector3 moveDir = new Vector3(touchdelta.x, touchdelta.y, 0f);
                    tr.Translate(moveDir.normalized * speed * Time.deltaTime);
                    startpos = touch.position;
                    break;

            }
        }
    }

    //trigger �浹 ó�� �ݹ� �Լ�
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            Destroy(col.gameObject);//�浹�� ������Ʈ ����(asteroid)
            Debug.Log($"�浹: ");
            var eff = Instantiate(effect,col.transform.position,Quaternion.identity);
            Destroy(eff,1f);// ����Ʈ ����
            GameManager.Instance.TurnOn(); //ī�޶� ��鸲
            GameManager.Instance.RocketHealtpoint(50); //ü�� ����
            source.PlayOneShot(hitclip, 1f); // �Ҹ����
            // �Ҹ��� �ѹ��� ����� (������?) (����)
        }
    }

    public void Fire()
    {
       var COIN =  Instantiate(coinPrefab, firepos.position, Quaternion.identity);
        Destroy (COIN, 1.5f);
    }
}
