using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject bossObj;
    public float bossLength = 10f;
    public GameObject wall;

    void Start()
    {
        
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player �±׸� ���� ������Ʈ ã��

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //�÷��̾�� ���� ��ũ��Ʈ�� ���� ������Ʈ ������ �Ÿ�����

            if (distance <= bossLength) //�Ÿ��� bossLength �̳��� �ִٸ�
            {
                Debug.Log("������ ����");
                bossObj.SetActive(true);
                wall.SetActive(true);
            }
        }
    }
}
