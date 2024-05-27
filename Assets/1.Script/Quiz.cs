using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProUGUI ����� ���� �߰�

public class Quiz : MonoBehaviour
{
    [Header("����")]
    public TextMeshProUGUI talkTrue; //��ȭ ������ ������ ���Դ��� ǥ���� �ؽ�Ʈ
    public float talkLength = 1f; //��ȭ ���� ����
    private bool isInRange = false; //��ȭ ���� �������� Ȯ��

    void Start()
    {
        // �ʱ� ���� ����
        SetAlpha(talkTrue, 0f); // ó������ �����ϰ� ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player �±׸� ���� ������Ʈ�� �浹���� ��
        {
            Debug.Log("��ȭ����");
            // talkTrue �ؽ�Ʈ�� ���İ��� 1�� ����
            SetAlpha(talkTrue, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player �±׸� ���� ������Ʈ�� �ݶ��̴��� ����� ��
        {
            Debug.Log("��ȭ�Ұ���");
            // talkTrue �ؽ�Ʈ�� ���İ��� 0���� ����
            SetAlpha(talkTrue, 0f);
        }
    }

    // TextMeshProUGUI ������Ʈ�� ���İ��� �����ϴ� �޼���
    void SetAlpha(TextMeshProUGUI textMeshPro, float alpha)
    {
        Color color = textMeshPro.color;
        color.a = alpha;
        textMeshPro.color = color;
    }
}
