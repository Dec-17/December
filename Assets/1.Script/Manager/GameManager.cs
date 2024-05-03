using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("재화")]
    public int goldInt = 0;
    public Text goldText;

    [Header("패널")]
    public GameObject settingPanel;
    public GameObject inventoryPanel;

    [Header("에임포인트")]
    public GameObject aimPoint;

    private bool isPaused = false; //게임이 일시정지 되었는지
    private bool isSettingPanelOpen = false; //설정 패널 활성화 여부
    private bool isInventoryPanelOpen = false; //인벤토리가 열려 있는지

    void Start()
    {
        //Cursor.visible = false; //게임 시작 시 마우스를 숨김
        //UpdateAimPointPosition(); //aimPoint의 초기 위치 설정
    }

    void Update()
    {
        UpdateGoldText(); //골드 소지량 업데이트

        if (Input.GetKeyDown(KeyCode.Escape)) //설정패널 또는 인벤토리 여닫기
        {
            if (isSettingPanelOpen)
            {
                CloseSettingPanel();
            }
            else if (isInventoryPanelOpen)
            {
                CloseInventoryPanel();
            }
            else
            {
                OpenSettingPanel();
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) //인벤토리 여닫기
        {
            if (isInventoryPanelOpen)
            {
                CloseInventoryPanel();
            }
            else
            {
                OpenInventory();
            }
        }

        UpdateGoldText(); //골드 소지량 업데이트

        //UpdateAimPointPosition(); //마우스 위치에 따라 aimPoint 이동
    }

    public void OpenInventory() //인벤토리 열기
    {
        PauseGame();
        inventoryPanel.SetActive(true);
        isInventoryPanelOpen = true;
    }

    void CloseInventoryPanel() //인벤토리 닫기
    {
        ResumeGame();
        inventoryPanel.SetActive(false);
        isInventoryPanelOpen = false;
    }

    public void OpenSettingPanel() //설정창 열기
    {
        if (!isPaused)
        {
            PauseGame();
            settingPanel.SetActive(true);
            isSettingPanelOpen = true;
        }
    }

    void CloseSettingPanel() //설정창 닫기
    {
        if (isPaused)
        {
            ResumeGame();
            settingPanel.SetActive(false);
            isSettingPanelOpen = false;
        }
    }

    public void BackGame() //게임으로 돌아가기
    {
        if (isPaused)
        {
            ResumeGame();
            settingPanel.SetActive(false);
            isSettingPanelOpen = false;
        }
    }

    public void Sound() //사운드 설정창 열기
    {

    }

    public void Title() //타이틀 화면으로
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Exit() //게임종료
    {
        Application.Quit();
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

    void UpdateGoldText() //골드 소지량 텍스트 업데이트
    {
        goldText.text = "Gold: " + goldInt.ToString();
    }

    void UpdateAimPointPosition() //에임포인트
    {
        Vector3 mousePosition = Input.mousePosition; //현재 마우스 위치 가져오기
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; //aimPoint의 z축 값 설정

        aimPoint.transform.position = mousePosition; //aimPoint를 마우스 위치로 이동
    }
}
