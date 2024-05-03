using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("��ȭ")]
    public int goldInt = 0;
    public Text goldText;

    [Header("�г�")]
    public GameObject settingPanel;
    public GameObject inventoryPanel;

    [Header("��������Ʈ")]
    public GameObject aimPoint;

    private bool isPaused = false; //������ �Ͻ����� �Ǿ�����
    private bool isSettingPanelOpen = false; //���� �г� Ȱ��ȭ ����
    private bool isInventoryPanelOpen = false; //�κ��丮�� ���� �ִ���

    void Start()
    {
        //Cursor.visible = false; //���� ���� �� ���콺�� ����
        //UpdateAimPointPosition(); //aimPoint�� �ʱ� ��ġ ����
    }

    void Update()
    {
        UpdateGoldText(); //��� ������ ������Ʈ

        if (Input.GetKeyDown(KeyCode.Escape)) //�����г� �Ǵ� �κ��丮 ���ݱ�
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

        if (Input.GetKeyDown(KeyCode.I)) //�κ��丮 ���ݱ�
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

        UpdateGoldText(); //��� ������ ������Ʈ

        //UpdateAimPointPosition(); //���콺 ��ġ�� ���� aimPoint �̵�
    }

    public void OpenInventory() //�κ��丮 ����
    {
        PauseGame();
        inventoryPanel.SetActive(true);
        isInventoryPanelOpen = true;
    }

    void CloseInventoryPanel() //�κ��丮 �ݱ�
    {
        ResumeGame();
        inventoryPanel.SetActive(false);
        isInventoryPanelOpen = false;
    }

    public void OpenSettingPanel() //����â ����
    {
        if (!isPaused)
        {
            PauseGame();
            settingPanel.SetActive(true);
            isSettingPanelOpen = true;
        }
    }

    void CloseSettingPanel() //����â �ݱ�
    {
        if (isPaused)
        {
            ResumeGame();
            settingPanel.SetActive(false);
            isSettingPanelOpen = false;
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
    void PauseGame() //���� �Ͻ�����
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    void ResumeGame() //���� �簳
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    void UpdateGoldText() //��� ������ �ؽ�Ʈ ������Ʈ
    {
        goldText.text = "Gold: " + goldInt.ToString();
    }

    void UpdateAimPointPosition() //��������Ʈ
    {
        Vector3 mousePosition = Input.mousePosition; //���� ���콺 ��ġ ��������
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition.z = 0f; //aimPoint�� z�� �� ����

        aimPoint.transform.position = mousePosition; //aimPoint�� ���콺 ��ġ�� �̵�
    }
}
