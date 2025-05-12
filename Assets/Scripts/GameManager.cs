using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱��� ���� ::��ü������ �ѹ��� �ǰ� Ŭ�������� ���� �����ϱ� ����
    //asteroid ������ ����
    public GameObject asteroidprefab;
    // asteroid ���� �ֱ� �� �ð�
    private float timePreve; //�ð�����
    [Header("bool GameOver")] // ��Ʃ����Ʈ
    public bool isgameover = false; // ���ӿ��� ����
    [Header("CamereShake Logic Member")] // ī�޶� ��鸲 ����
    public bool isShake = false;
    public Vector3 PosCamera; // ī�޶� ��ġ ����( ���ȴٰ� ���� ��ġ�� ���ƿ;��ϱ� ������)
    public float beginTime; // ī�޶� ��鸮�� ������ �ð�
    [Header("HpBar UI")]
    public int hp; //ü���� �پ��� ����
    public int maxhp = 100;
    public Image hpBar; // ü�¹� ui
    public Text hpText; // ü�� ui
    [Header("GameOver obj")]
    public GameObject gameoverobj; //���� ���� ������Ʈ
    public Text scoreTxt;
    public float curScore = 0;
    public float Plusscore = 100f;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);
        Instance = this; // �̱��� ���� instance = getcomponent<gamemanager>
        //DontDestroyOnLoad(this.gameObject);
        //���� ������ ����ø� ����
        timePreve = Time.time; // ����ð� ����
        hp = maxhp; // ü�� �ʱ�ȭ
    }
    void Update()
    {
        // ����� - �����ð� = �귯�� �ð�
        GameOver();
        AsteroidSpwan();
        CameraShake();
    }

    private static void GameOver()
    {
        if (GameManager.Instance.isgameover == true) //���ӿ����� �Ǹ�
        {
            return; //���⼭ ���� �������� ���� ���� ����
        }
    }

    private void CameraShake()
    {
        if (isShake == true) // ���࿡ ī�޶� ��鸰�ٸ�
        {
            float x = Random.Range(-0.1f, 0.1f); //�������� x��ǥ ����
            float y = Random.Range(-0.1f, 0.1f);
            Camera.main.transform.position += new Vector3(x, y, 0f);
            if (Time.time - beginTime > 0.3f)
            {
                isShake = false; // ī�޶� ��鸲 ����
                Camera.main.transform.position = PosCamera; // ī�޶� ���� ��ġ�� ����
            }
        }
        ScoreCount();
    }

    private void ScoreCount()
    {
        curScore += Plusscore * Time.deltaTime;
        //Time.realtimeSinceStartup : ������ ������ ������ �ð��� �ʴ����� ��ȯ readonly
        scoreTxt.text = $"{Mathf.FloorToInt(curScore)}";
        //mathf.floorToInt() float������ �۰ų� ���� ū ������ ��ȯ �Ѵ�.
        //mathf.floortoint(3.7)�� ��� ���� 3�� ��ȯ
        //mathf.FloorToInt(-3,2)�� ��� ���� -4�� ��ȯ
        //mathf.FloorToInt(-3.7)�ΰ�� ���� -4�� ��ȯ
    }

    private void AsteroidSpwan()
    {
        if (Time.time - timePreve > 2.5f && !isgameover)
        {
            float randomy = Random.Range(-2.22f, 4.26f);//�������� y��ǥ ����                          ȸ������ �ʰ� �����ϰ�
            Instantiate(asteroidprefab, new Vector3(12f, randomy, asteroidprefab.transform.position.z), Quaternion.identity);
            //������Ʈ�����Լ� (What?       where,                                                   how ȸ���� ���ΰ�)
            timePreve = Time.time; // ����ð� ����
        }
    }

    public void TurnOn()
    {
        isShake = true;
        PosCamera = Camera.main.transform.position; // ī�޶� ��鸮���� ��ġ ����
        beginTime = Time.time; //ī�޶� ��鸮�� ������ �ð� ����
    }
    
    public void RocketHealtpoint(int damage)
    {
        hp -= damage;//ü�°���
        hp = Mathf.Clamp(hp, 0, 100); //ü�� 0~100���� ����
        hpBar.fillAmount = (float)hp / (float)maxhp; // ü�¹� ui����
        hpText.text = $"HP :  <color=#ff0000>{hp}</color>";
        if (hp <= 0)
        {
            isgameover = true; //���ӿ���
            gameoverobj.SetActive(true);//���ӿ��� ������Ʈ Ȱ��ȭ
            //���� ������Ʈ�� Ȱ��ȭ ��Ȱ��ȭ �ϴ� �Լ�
            Invoke("LobbySceneMove", 3.0f);
            //��Ʈ�� ���ڸ� �о ���ϴ� �ð��� ȣ�� �ϴ� �Լ�
        }
    }
    public void LobbySceneMove()
    {
        SceneManager.LoadScene("LobbyScene"); //���� �κ�� ��ȯ
    }

}
