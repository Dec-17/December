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
    private bool isPaused = false; //������ �Ͻ����� �Ǿ�����
    private bool isSettingPanelOpen = false; //���� �г��� ���� �ִ���

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

    void UpdateGoldText() //��� �ؽ�Ʈ ������Ʈ
    {
        goldText.text = "Gold: " + goldInt.ToString();
    }

    public void Setting() //����â ����
    {
        if (!isPaused)
        {
            PauseGame();
            settingPanel.SetActive(true);
            isSettingPanelOpen = true;
        }
    }

    public void BackGame() //�������� ���ư���
    {
        if (isPaused)
        {
            ResumeGame();
            settingPanel.SetActive(false);
            isSettingPanelOpen = false;
        }
    }

    public void Sound() //���� ����â ����
    {

    }

    public void Title() //Ÿ��Ʋ ȭ������
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Exit() //��������
    {
        Application.Quit();
    }
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; //���� �Ͻ�����
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; //���� �簳
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
