using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 패턴 ::객체생성은 한번만 되고 클래스에서 쉽게 접근하기 위해
    //asteroid 프리팹 생성
    public GameObject asteroidprefab;
    // asteroid 생성 주기 및 시간
    private float timePreve; //시간저장
    [Header("bool GameOver")] // 애튜리뷰트
    public bool isgameover = false; // 게임오버 여부
    [Header("CamereShake Logic Member")] // 카메라 흔들림 여부
    public bool isShake = false;
    public Vector3 PosCamera; // 카메라 위치 저장( 흔들렸다가 원래 위치로 돌아와야하기 때문에)
    public float beginTime; // 카메라 흔들리기 시작한 시간
    [Header("HpBar UI")]
    public int hp; //체력이 줄어드는 변수
    public int maxhp = 100;
    public Image hpBar; // 체력바 ui
    public Text hpText; // 체력 ui
    [Header("GameOver obj")]
    public GameObject gameoverobj; //게임 오버 오브젝트
    public Text scoreTxt;
    public float curScore = 0;
    public float Plusscore = 100f;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);
        Instance = this; // 싱글톤 패턴 instance = getcomponent<gamemanager>
        //DontDestroyOnLoad(this.gameObject);
        //게임 시작전 현재시를 저장
        timePreve = Time.time; // 현재시간 저장
        hp = maxhp; // 체력 초기화
    }
    void Update()
    {
        // 현재시 - 지난시간 = 흘러간 시간
        GameOver();
        AsteroidSpwan();
        CameraShake();
    }

    private static void GameOver()
    {
        if (GameManager.Instance.isgameover == true) //게임오버가 되면
        {
            return; //여기서 하위 로직으로 진행 되지 않음
        }
    }

    private void CameraShake()
    {
        if (isShake == true) // 만약에 카메라가 흔들린다면
        {
            float x = Random.Range(-0.1f, 0.1f); //랜덤으로 x좌표 설정
            float y = Random.Range(-0.1f, 0.1f);
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - beginTime > 0.3f)
            {
                isShake = false; // 카메라 흔들림 종료
                Camera.main.transform.position = PosCamera; // 카메라 원래 위치로 복귀
            }
        }
        ScoreCount();
    }

    private void ScoreCount()
    {
        curScore += Plusscore * Time.deltaTime;
        //Time.realtimeSinceStartup : 게임이 시작한 이후의 시간을 초단위로 변환 readonly
        scoreTxt.text = $"{Mathf.FloorToInt(curScore)}";
        //mathf.floorToInt() float값보다 작거나 같은 큰 정수를 반환 한다.
        //mathf.floortoint(3.7)인 경우 정수 3을 반환
        //mathf.FloorToInt(-3,2)인 경우 정수 -4를 반환
        //mathf.FloorToInt(-3.7)인경우 정수 -4를 반환
    }

    private void AsteroidSpwan()
    {
        if (Time.time - timePreve > 2.5f && !isgameover)
        {
            float randomy = Random.Range(-2.22f, 4.26f);//랜덤으로 y좌표 설정                          회전하지 않고 일정하게
            Instantiate(asteroidprefab, new Vector3(12f, randomy, asteroidprefab.transform.position.z), Quaternion.identity);
            //오브젝트생성함수 (What?       where,                                                   how 회전할 것인가)
            timePreve = Time.time; // 현재시간 저장
        }
    }

    public void TurnOn()
    {
        isShake = true;
        PosCamera = Camera.main.transform.position; // 카메라 흔들리기전 위치 저장
        beginTime = Time.time; //카메라 흔들리기 시작한 시간 지정
    }
    
    public void RocketHealtpoint(int damage)
    {
        hp -= damage;//체력감소
        hp = Mathf.Clamp(hp, 0, 100); //체력 0~100으로 제한
        hpBar.fillAmount = (float)hp / (float)maxhp; // 체력바 ui갱신
        hpText.text = $"HP :  <color=#ff0000>{hp}</color>";
        if (hp <= 0)
        {
            isgameover = true; //게임오버
            gameoverobj.SetActive(true);//게임오버 오브젝트 활성화
            //게임 오브젝트를 활성화 비활성화 하는 함수
            Invoke("LobbySceneMove", 3.0f);
            //스트링 문자를 읽어서 원하는 시간에 호출 하는 함수
        }
    }
    public void LobbySceneMove()
    {
        SceneManager.LoadScene("LobbyScene"); //씬을 로비로 전환
    }

}
