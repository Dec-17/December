using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int goldInt = 0;
    public Text goldText;
    public GameObject settingPanel;
    private bool isPaused = false; //게임이 일시정지 되었는지
    private bool isSettingPanelOpen = false; //설정 패널이 열려 있는지

    void Start()
    {

    }

    void Update()
    {
        UpdateGoldText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingPanelOpen)
            {
                CloseSettingPanel();
            }
            else
            {
                Setting();
            }
        }
    }

    void UpdateGoldText() //골드 텍스트 업데이트
    {
        goldText.text = "Gold: " + goldInt.ToString();
    }

    public void Setting() //설정창 열기
    {
        if (!isPaused)
        {
            PauseGame();
            settingPanel.SetActive(true);
            isSettingPanelOpen = true;
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
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; //게임 일시정지
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; //게임 재개
    }
    void CloseSettingPanel()
    {
        if (isPaused)
        {
            ResumeGame();
            settingPanel.SetActive(false);
            isSettingPanelOpen = false;
        }
    }
}
