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
#if UNITY_EDITOR //��ũ�� ���� �����Ϸ��� �����Ϳ��� ���������� Ȯ��
        UnityEditor.EditorApplication.isPlaying = false;// �����Ϳ��� ���� ����
#else
        Application.Quit(); // ���� ����- ������ ������ ������, ����Ƽ�� ������ �ʴ´�
#endif
    }
}
