using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float mobHP = 1f; //�� ������Ʈ�� ü��
    public GameObject[] NomalItem; //�Ϲ� ������ �迭
    public GameObject[] EpicItem; //��� ������ �迭
    public float NomalItemProbability = 0.85f; //�Ϲ� ������ ��� Ȯ��
    public float EpicItemProbability = 0.15f; //��� ������ ��� Ȯ��

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) //�浹ó��
    {
        if (other.CompareTag("Arrow")) //�浹�� �Ͼ ������Ʈ�� �±װ� "Arrow"�ϋ�
        {
            mobHP--; //������Ʈ�� ü�� -1

            if (mobHP <= 0) //ü���� 0���ϰ� �ȴٸ�
            {
                Destroy(gameObject); //������Ʈ �ı�
                DropNomalItems(); //�Ϲ� ������ ��� �޼��� ����
                DropEpicItems(); //��� ������ ��� �޼��� ����
            }
        }
    }

    void DropNomalItems() //�Ϲ� ������ ��� �޼���
    {
        foreach (GameObject item in NomalItem) //NomalItem �迭��ŭ �ݺ�
        {
            if (Random.value < NomalItemProbability) //NomalItemProbability Ȯ���� �������� ���
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f), //������ ������ġ ��������
                                                    transform.position.y + Random.Range(-1f, 1f),
                                                    transform.position.z);
                Instantiate(item, spawnPosition, Quaternion.identity); //�������� ����
            }
        }
    }
    void DropEpicItems() //��� ������ ��� �޼���
    {
        foreach (GameObject item in EpicItem) //EpicItem �迭��ŭ �ݺ�
        {
            if (Random.value < EpicItemProbability) //EpicItemProbability Ȯ���� �������� ���
            {
                Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f), //������ ������ġ ��������
                                                    transform.position.y + Random.Range(-1f, 1f),
                                                    transform.position.z);
                Instantiate(item, spawnPosition, Quaternion.identity); //�������� ����
            }
        }
    }
}
