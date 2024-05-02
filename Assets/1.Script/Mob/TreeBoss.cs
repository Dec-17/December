using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TreeBoss : MonoBehaviour
{
    //ü��
    public float bossHP = 50f; //ü��
    public float bossLeftHandHP = 30f; //�޼� ü��
    public float bossRightHandHP = 30f; //������ ü��

    //���� ���ӽð�
    public float patenTime = 3f;

    //������ ����
    public GameObject razerPrefab; //������ ������
    public GameObject razerWarningPrefab; //������ ��� ������

    //����ȯ ����
    public GameObject spawnMonsterPrefab; //��ȯ�� ������
    public int minSpawnCount = 1; //�ּ� ��ȯ ���� ��
    public int maxSpawnCount = 3; //�ִ� ��ȯ ���� ��

    //ź�� ����
    public GameObject energyBombPrefab; //ź�� ������
    public float energyBombSpeed = 20f; //ź�� �ӵ�

    //���� ����
    public GameObject rockShotPrefab; //���� ������

    //����ġ ����
    bool isPatternRunning = false;

    Collider bossColliders;
    Animation bossAnimation;
    Sprite leftHandSprite;
    Sprite rightHandSprite;

    void Start()
    {
        bossColliders = GetComponent<Collider>();
        bossAnimation = GetComponent<Animation>();
        leftHandSprite = GetComponent<Sprite>();
        rightHandSprite = GetComponent<Sprite>();

        // 5�ʸ��� ���� ȣ���� ����
        StartCoroutine(RandomPatternCaller());
    }

    void Update()
    {

    }

    // 5�ʸ��� ������ �������� ȣ���ϴ� �ڷ�ƾ
    IEnumerator RandomPatternCaller()
    {
        while (true)
        {
            //bossAnimation.SetTrigger(""); //���� �⺻ �ִϸ��̼�
            yield return new WaitForSeconds(0f); //*�� ���

            if (!isPatternRunning) // ������ ���� ���� �ƴ� ���� ������ ȣ��
            {
                int patternNumber = Random.Range(1, 5); // 1���� 4���� ������ ���� ��ȣ ����

                switch (patternNumber)
                {
                    case 1:
                        StartCoroutine(RazerPattern());
                        break;
                    case 2:
                        StartCoroutine(MobSpawnPattern());
                        break;
                    case 3:
                        StartCoroutine(EnergyBombPattern());
                        break;
                    case 4:
                        StartCoroutine(RockShotPattern());
                        break;
                    default:
                        Debug.LogWarning("Invalid pattern number");
                        break;
                }
            }
        }
    }

    IEnumerator RazerPattern() //1.������ ����
    {
        Debug.Log("1��");
        isPatternRunning = true;
        
        float patternEndTime = Time.time + 17f; //*�� ���� ���� ����

        while (Time.time < patternEndTime)
        {
            
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; //�÷��̾��� ���� ��ġ ��������

            Vector3 spawnPosition = playerPosition; //razerPrefab�� �߽��� �÷��̾� ��ġ�� ���� ����
            
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)); //rotation Z�� ��������

           // yield return new WaitForSeconds(0.3f); //*�� ������

            GameObject razerWarning = Instantiate(razerWarningPrefab, spawnPosition, randomRotation); //������ ��� ������Ʈ ����
            yield return new WaitForSeconds(0.5f); //*�� ������

            Destroy(razerWarning); //*�� �Ŀ� ������ ��� �ı�

            GameObject lazer = Instantiate(razerPrefab, spawnPosition, randomRotation); //������ ������Ʈ ����
            
            Destroy(lazer, 2f); //*�� �Ŀ� ������ �ı�

            yield return new WaitForSeconds(0.1f); //*�ʸ��� ����
        }

        // ������ ���� �ִϸ��̼� ����
        isPatternRunning = false;
    }


    IEnumerator MobSpawnPattern() //2.����ȯ ����
    {
        Debug.Log("2��");
        isPatternRunning = true; //���� ������

        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1); //���� �� ����
        
        for (int i = 0; i < spawnCount; i++) //���� ���� ��ġ�� ����
        {
            float randomX = Random.Range(50f, 100f); //x�� ���� ��
            
            float randomY = Random.Range(1.5f, -35f); //y�� ���� ��
           
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f); //���� ��ġ ����
            
            Instantiate(spawnMonsterPrefab, spawnPosition, Quaternion.identity); //�� ����
        }

        yield return new WaitForSeconds(10f); //*�� ��ŭ ���

        isPatternRunning = false; //���� ����
    }

    IEnumerator EnergyBombPattern() //3.ź�� ����
    {
        //bossAnimation.SetTrigger(""); //ź�� ���� �ִϸ��̼�
        isPatternRunning = true;
        Debug.Log("3��");
        yield return new WaitForSeconds(patenTime);
        isPatternRunning = false;
    }

    IEnumerator RockShotPattern() //4.���� ����
    {
        //bossAnimation.SetTrigger(""); //���� ���� �ִϸ��̼�
        isPatternRunning = true;
        Debug.Log("4��");
        yield return new WaitForSeconds(patenTime);
        isPatternRunning = false;
    }

    void Die()
    {
        if(bossHP >= 0)
        {
            //bossAnimation.SetTrigger(""); //���� ��� �ִϸ��̼�
        }
    }
}
