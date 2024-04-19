using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float followSpeed = 10f; //�÷��̾ ���󰡴� �ӵ�
    public float detectionRange = 5f; //�������� �÷��̾ ������ ����

    void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>(); // ������ �÷��̾� ã��

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRange) // �÷��̾ ���� ���� ���� ���� �� �̵�
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed * Time.deltaTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) //������ ȹ�� �� �ı�
    {
        if (other.gameObject.CompareTag("Player")) //Player�� �浹�� �Ͼ�ٸ�
        {
            //������ ȹ�� ��
            Destroy(gameObject); //�������� ȹ�������Ƿ� �ش� ������Ʈ �ı�
        }
    }
}
