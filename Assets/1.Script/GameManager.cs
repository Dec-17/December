using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
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

    [Header("플레이어")]
    public GameObject playerOriginal; //플레이어
    public Vector3 respawnPosition; //플레이어 리스폰 위치
    public GameObject youDied;
    public GameObject Respawn;

    private bool isPaused = false; //게임이 일시정지 되었는지
    private bool isSettingPanelOpen = false; //설정 패널 활성화 여부
    private bool isInventoryPanelOpen = false; //인벤토리가 열려 있는지
    PlayerController playerController;
    MapManager mapManager;

    void Start()
    {
        Cursor.visible = false; //게임 시작 시 마우스를 숨김***
        UpdateAimPointPosition(); //aimPoint의 초기 위치 설정***
        playerController = GetComponent<PlayerController>();
        playerController = FindObjectOfType<PlayerController>();
        mapManager = FindAnyObjectByType<MapManager>();
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

        UpdateAimPointPosition(); //마우스 위치에 따라 aimPoint 이동***.

        RespawnPlayer(); //플레이어 사망 시
    }

    void RespawnPlayer() //플레이어 리스폰
    {
        if (!playerOriginal.activeSelf)
        {
            StartCoroutine(RespawnCor());
        }
    }

    IEnumerator RespawnCor()
    {
        youDied.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Respawn.SetActive(true);
        playerController.playerColor();
        playerController.playerHP = playerController.maxPlayerHP;
        playerController.playerSP = playerController.maxPlayerSP;
        playerOriginal.transform.position = respawnPosition;
        mapManager.respawnLight();
        playerOriginal.SetActive(true);
        youDied.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        Respawn.SetActive(false);
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

    public void PauseGame() //게임 일시정지
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void ResumeGame() //게임 재개
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void UpdateGoldText() //골드 소지량 텍스트 업데이트
    {
        goldText.text = goldInt.ToString() + " Gold";
    }

    void UpdateAimPointPosition() //에임포인트
    {
        Vector3 mousePosition = Input.mousePosition; //현재 마우스 위치 가져오기
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //마우스 위치를 월드 좌표로 변환
        mousePosition.z = 0f; //aimPoint의 z축 값 설정

        aimPoint.transform.position = mousePosition; //aimPoint를 마우스 위치로 이동
    }
}
