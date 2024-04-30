using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBomb : MonoBehaviour
{
    public GameObject reazerPrefab; //������
    public GameObject razerLinePrefab; //������ ���ǥ��
    public float razerLineDuration = 1f; //������ ���ǥ�� ���� �ð�
    public float razerDuration = 0.3f; //������ ���� �ð�

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "collider") //�浹�� ������Ʈ�� �±װ� collider�� ��
        {
            //�ı��Ǳ� �� ��ġ�� X���� ������ ���� �״�� ���, Y���� ���� ������Ʈ�� Y������ �����Ͽ� ������ ���ǥ�� ����
            Vector3 spawnPosition = new Vector3(razerLinePrefab.transform.position.x, transform.position.y, 0f);
            GameObject razerLine = Instantiate(razerLinePrefab, spawnPosition, Quaternion.identity);

            Destroy(razerLine, razerLineDuration); //������ �ð� �Ŀ� ������ ���ǥ�� �ı�

            //������ ���� �ڷ�ƾ �ۼ� �ؾ���

            Destroy(gameObject); //������Ʈ �ı�
        }
    }
}
