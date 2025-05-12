using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public void GameStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void GameQuit()
    {
#if UNITY_EDITOR //매크로 지정 컴파일러가 에디터에서 실행중인지 확인
        UnityEditor.EditorApplication.isPlaying = false;// 에디터에서 게임 종료
#else
        Application.Quit(); // 게임 종료- 빌드한 파일이 꺼진다, 유니티는 꺼지지 않는다
#endif
    }
}
