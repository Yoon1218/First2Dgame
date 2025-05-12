using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketMove : MonoBehaviour
{
    private float h = 0f; // A,D 키를 받기 위한 변수
    private float v = 0f; //W,S 키를 받기 위한 변수
    public float speed = 5f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source; // 오디오 소스
    public AudioClip hitclip; //오디오 클립
    private Vector2 startpos = Vector2.zero;//시작위치
    public Transform firepos;
    public GameObject coinPrefab;
    void Start()
    {
        source = GetComponent<AudioSource>(); // 오디오소스 컴포넌트 가져오기
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (GameManager.Instance.isgameover == true) //게임오버가 되면
        {
            return; //여기서 하위 로직으로 진행 되지 않음
        }
            if(Application.platform == RuntimePlatform.WindowsEditor) //windows플랫폼일때
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
                Vector3 normal = (h * Vector3.right) + (v * Vector3.up);
                tr.Translate(normal.normalized * speed * Time.deltaTime);
        }
            if(Application.platform == RuntimePlatform.Android) // 안드로이드 플랫폼일때
        {
            TouchMove();
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer) //iphone  플랫폼일때
        {
            TouchMove();
        }

        //정규화 2개의 키를 동시에 누르면 같은 힘으로 움직이게.
        // tr.Translate(Vector3.up*v*speed*Time.deltaTime);
        #region 초보자 로직
        // if (tr.position.x>7.83f)
        //     tr.position = new Vector3(7.83f,tr.position.y,tr.position.z);
        //else if (tr.position.x<-7.83f)
        //     tr.position = new Vector3(-7.83f,tr.position.y,tr.position.z) ;
        //if(tr.position.y> 4.51f)
        //     tr.position = new Vector3(tr.position.x,4.51f, tr.position.z);
        //else if(tr.position.y<-3.46f)
        //     tr.position = new Vector3(tr.position.x, -3.46f, tr.position.z);
        #endregion
        #region 중급자 로직
        tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7.83f, 7.83f), Mathf.Clamp(tr.position.y, -3.46f, 4.51f), tr.position.z);
        //수학클래스(mathf.clamp(what?,min,max) // 값을 제한하는 함수
        #endregion


    }

    private void TouchMove()
    {
        if (Input.touchCount > 0)//한번이라도 터치가 되었다면..
        {
            Touch touch = Input.GetTouch(0); // 터치한 정보를 가져옴
                                             //gettouch(0) // 터치한 위치값을 배열에 저장 되었는데 첫번째로 터치한 위치를 가져옴
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startpos = touch.position;
                    break;
                case TouchPhase.Moved:
                    Vector3 touchdelta = touch.position - startpos; // 터치한 위치 - 시작위치로 거리를 구함
                    Vector3 moveDir = new Vector3(touchdelta.x, touchdelta.y, 0f);
                    tr.Translate(moveDir.normalized * speed * Time.deltaTime);
                    startpos = touch.position;
                    break;

            }
        }
    }

    //trigger 충돌 처리 콜백 함수
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            Destroy(col.gameObject);//충돌한 오브젝트 삭제(asteroid)
            Debug.Log($"충돌: ");
            var eff = Instantiate(effect,col.transform.position,Quaternion.identity);
            Destroy(eff,1f);// 이펙트 삭제
            GameManager.Instance.TurnOn(); //카메라 흔들림
            GameManager.Instance.RocketHealtpoint(50); //체력 감소
            source.PlayOneShot(hitclip, 1f); // 소리재생
            // 소리를 한번만 울려라 (무엇을?) (볼륨)
        }
    }

    public void Fire()
    {
       var COIN =  Instantiate(coinPrefab, firepos.position, Quaternion.identity);
        Destroy (COIN, 1.5f);
    }
}
