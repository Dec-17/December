using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject intro;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameStart() //게임시작
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Story()
    {
        intro.SetActive(true);
    }

    public void closed()
    {
        intro.SetActive(false);
    }

    public void Exit() //게임종료
    {
        Application.Quit();
    }
}
