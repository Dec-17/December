using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject talkTrue; //��ȭ ���� ������ ���Դ��� ǥ��
    public bool isInRange = false; //��ȭ ���� ������ ���Դ��� Ȯ���ϴ� ��
    public float talkLength = 3; //��ȭ ���� ����

    void Start()
    {

    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");  //Player �±׸� ���� ������Ʈ ã��

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //�÷��̾�� ���� ��ũ��Ʈ�� ���� ������Ʈ ������ �Ÿ�����

            if (distance <= talkLength) //�Ÿ��� talkLength �̳��� �ִٸ�
            {
                isInRange = true; //��ȭ ���� ������ ����
                break;
            }
            else
            {
                isInRange = false; //��ȭ ���� �Ÿ� ����
            }
        }
        
        if (isInRange) //���� ���� �ȿ� �ִٸ�
        {
            Debug.Log("��ȭ����");
            //talkTrue ������Ʈ�� ���İ��� 1�� ����
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 1f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            Debug.Log("��ȭ�Ұ���");
            //���� �ۿ� �ִٸ� talkTrue ������Ʈ�� ���İ��� 0���� ����
            Color color = talkTrue.GetComponent<Renderer>().material.color;
            color.a = 0f;
            talkTrue.GetComponent<Renderer>().material.color = color;
        }
    }
}
