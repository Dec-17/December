using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    public GameObject savePanel; //세이브 패널
    private bool isSavePanelOpen = false; //세이브 패널 활성화 여부
    public float saveLength = 3f; //세이브 가능 범위
    public Text saveTrue; //세이브 가능한 범위에 들어왔는지 표시
    private bool isInRange = false; //세이브 가능 범위인지 확인

    private bool isPaused = false; //게임이 일시정지 되었는지

    void Start()
    {

    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player 태그를 가진 오브젝트 찾기

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //플레이어와 현재 스크립트를 가진 오브젝트 사이의 거리측정

            if (distance <= saveLength) //거리가 saveLength 이내에 있다면
            {
                isInRange = true; //세이브 가능 범위에 들어옴
                break;
            }
            else
            {
                isInRange = false; //세이브 가능 거리 밖임
            }
        }

        if (isInRange) //만약 범위 안에 있다면
        {
            Debug.Log("세이브가능");
            //saveTrue 오브젝트의 알파값을 1로 설정
            Color color = saveTrue.color;
            color.a = 1f;
            saveTrue.color = color;
        }
        else
        {
            Debug.Log("세이브불가능");
            //범위 밖에 있다면 saveTrue 오브젝트의 알파값을 0으로 설정
            Color color = saveTrue.color;
            color.a = 0f;
            saveTrue.color = color;
        }

        saveStart();
    }

    public void saveStart()
    {
        if (isInRange == true) //세이브 가능 범위안에 들어왔을때
        {
            if (Input.GetKeyDown(KeyCode.Space)) //스페이스 입력 시
            {
                if (isSavePanelOpen == false) //세이브창이 닫혀있다면
                {
                    OpenSavePanel(); //세이브창 열기
                }
                else //세이브창이 열려있다면
                {
                    CloseSavePanel(); //세이브창 닫기
                }
            }
        }
    }

    public void OpenSavePanel() //세이브창 열기
    {
        if (!isPaused)
        {
            PauseGame();
            savePanel.SetActive(true);
            isSavePanelOpen = true;
        }
    }

    public void CloseSavePanel() //세이브창 닫기
    {
        if (isPaused)
        {
            ResumeGame();
            savePanel.SetActive(false);
            isSavePanelOpen = false;
        }
    }

    void PauseGame() //게임 일시정지
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    void ResumeGame() //게임 재개
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}