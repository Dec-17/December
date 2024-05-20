using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject statue01;
    public GameObject statue02;
    public GameObject statue03;
    public GameObject statue04;

    private int currentStatueIndex = 0; // ���� �浹�� ����Ǵ� ������ �ε���
    private GameObject[] statues; // ���� ������Ʈ �迭

    void Start()
    {
        // ���� ������Ʈ �迭 �ʱ�ȭ
        statues = new GameObject[] { statue01, statue02, statue03, statue04 };
    }

    void Update()
    {
        // �ʿ� �� ������Ʈ ���� �߰�
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            if (currentStatueIndex < statues.Length && statues[currentStatueIndex] == other.gameObject)
            {
                currentStatueIndex++;
                if (currentStatueIndex == statues.Length)
                {
                    Debug.Log("��ȣ:1234");
                    // ��ȣ�� �ùٸ��� �ԷµǾ��� �� �߰� �۾� ����
                }
            }
            else
            {
                // ������ �߸��Ǿ��� �� �ʱ�ȭ
                currentStatueIndex = 0;
                Debug.Log("������ �߸��Ǿ����ϴ�. �ٽ� �õ��Ͻʽÿ�.");
            }
        }
    }
}
