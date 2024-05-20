using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class TreeBoss : MonoBehaviour
{
    [Header("ü��")]
    //ü��
    public float bossHP = 50f; //ü��
    public float bossLeftHandHP = 20f; //�޼� ü��
    public float bossRightHandHP = 20f; //������ ü��

    [Header("������ ����")]
    //������ ����
    public GameObject razerPrefab; //������ ������
    public GameObject razerWarningPrefab; //������ ��� ������
    public GameObject bossEffect; //������ ������ ����Ʈ
    public float razerDuration = 3.5f; //������ ���� ���ӽð�

    [Header("����ȯ ����")]
    //����ȯ ����
    public GameObject spawnMonsterPrefab; //��ȯ�� ������
    public int minSpawnCount = 1; //�ּ� ��ȯ ���� ��
    public int maxSpawnCount = 3; //�ִ� ��ȯ ���� ��
    public float mobSpawnDuration = 15; //����ȯ ���� ���ӽð�

    [Header("ź�� ����")]
    //ź�� ����
    public GameObject energyBombPrefab; //ź�� ������
    public GameObject energyBombMiniPrefab; //�̴�ź�� ������
    public GameObject energyChargePrefab; //ź�� ��¡ ��� ������
    public float energyBombSpeed = 20f; //ź�� �ӵ�
    public float energyBombDuration = 10f; //ź�� ���� ���ӽð�

    [Header("���� ����")]
    //���� ����
    public GameObject rockShotPrefab; //���� ������
    public float rockShotDuration = 10; //���� ���� ���ӽð�


    [Header("��Ÿ")]
    public bool isPatternRunning = false;
    public float patternRunningTIme = 9f;

    public CameraMovement cameraMovement;
    Collider bossColliders;
    Animation bossAnimation;
    Sprite leftHandSprite;
    Sprite rightHandSprite;
    Animator bossAnimator;

    void Start()
    {
        bossColliders = GetComponent<Collider>();
        bossAnimation = GetComponent<Animation>();
        bossAnimator = GetComponent<Animator>();
        leftHandSprite = GetComponent<Sprite>();
        rightHandSprite = GetComponent<Sprite>();
        cameraMovement = GetComponent<CameraMovement>();
        cameraMovement = FindObjectOfType<CameraMovement>();


        StartCoroutine(RandomPatternCaller()); //*�ʸ��� ���� ȣ���� ����
    }

    void Update()
    {

    }


    IEnumerator RandomPatternCaller() //*�ʸ��� ������ �������� ȣ���ϴ� �ڷ�ƾ
    {
        while (true)
        {
            //bossAnimation.SetTrigger(""); //���� �⺻ �ִϸ��̼�
            yield return new WaitForSeconds(patternRunningTIme); //patternRunningTIme��ŭ ���

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

    IEnumerator RazerPattern() //1-1.������ ����
    {
        Debug.Log("������ ���� ����");
        isPatternRunning = true;

        bossEffect.SetActive(true); //bossEffect�� Ȱ��ȭ
        bossAnimator.SetBool("Razer", true); //�ִϸ��̼� ����

        yield return new WaitForSeconds(1f); //bossEffect�� *�� �Ŀ� ��Ȱ��ȭ
        bossEffect.SetActive(false);

        yield return new WaitForSeconds(3.5f); //********************

        bossAnimator.SetBool("Razer", false); //�ִϸ��̼� ����
        isPatternRunning = false;
    }

    public IEnumerator RazerEvent() //1-2.������ ����
    {
        float patternEndTime = Time.time + razerDuration; //razerDuration ���� ���� ����

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
    }

    IEnumerator MobSpawnPattern() //2.����ȯ ����
    {
        Debug.Log("����ȯ ���� ����");
        isPatternRunning = true; //���� ������

        bossAnimator.SetBool("MobSpawn", true); //�ִϸ��̼� ����

        yield return new WaitForSeconds(1);

        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1); //���� �� ����

        for (int i = 0; i < spawnCount; i++) //���� ���� ��ġ�� ����
        {
            float randomX = Random.Range(50f, 100f); //x�� ���� ��

            float randomY = Random.Range(1.5f, -35f); //y�� ���� ��

            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f); //���� ��ġ ����

            Instantiate(spawnMonsterPrefab, spawnPosition, Quaternion.identity); //�� ����
        }
        yield return new WaitForSeconds(0.4f);
        bossAnimator.SetBool("MobSpawn", false); //�ִϸ��̼� ����
        yield return new WaitForSeconds(mobSpawnDuration); //mobSpawnDuration ��ŭ ���

        Debug.Log("��������");
        isPatternRunning = false; //���� ����

    }

    IEnumerator EnergyBombPattern() //3.ź�� ����
    {
        isPatternRunning = true;
        Debug.Log("ź�� ���� ����");

        bossEffect.SetActive(true); //bossEffect�� Ȱ��ȭ
        bossAnimator.SetBool("EnergyBomb", true); //�ִϸ��̼� ����

        yield return new WaitForSeconds(1f); //bossEffect�� *�� �Ŀ� ��Ȱ��ȭ
        bossEffect.SetActive(false); //bossEffect�� ��Ȱ��ȭ
        energyChargePrefab.SetActive(true); //��������¡ ������Ʈ Ȱ��ȭ

        yield return new WaitForSeconds(1f); // *�ʴ��

        float startTime = Time.time;
        while (Time.time < startTime + energyBombDuration) // energyBombDuration ���� �ݺ�
        {
            // energyBombPrefab ���� �� ����
            GameObject energyBomb = Instantiate(energyBombPrefab, transform.position, Quaternion.identity);
            Rigidbody2D energyBombRb = energyBomb.GetComponent<Rigidbody2D>();
            energyBombRb.velocity = new Vector2(Random.Range(-10f, 10f), -10f); // ������ X�� �ӵ��� Y�� �ӵ� ����

            // energyBombMiniPrefab ���� �� ����
            for (int i = 0; i < 3; i++)
            {
                GameObject energyBombMini = Instantiate(energyBombMiniPrefab, transform.position, Quaternion.identity);
                Rigidbody2D energyBombMiniRb = energyBombMini.GetComponent<Rigidbody2D>();
                energyBombMiniRb.velocity = new Vector2(Random.Range(-10f, 10f), -10f); // ������ X�� �ӵ��� Y�� �ӵ� ����
            }

            yield return new WaitForSeconds(1f); // *�ʸ��� ���
        }



        energyChargePrefab.SetActive(false); //��������¡ ������Ʈ ��Ȱ��ȭ
        bossAnimator.SetBool("EnergyBomb", false); //�ִϸ��̼� ����

        isPatternRunning = false;
    }

    IEnumerator RockShotPattern() //4.���� ����
    {
        Debug.Log("���� ���� ����");
        isPatternRunning = true;

        cameraMovement.earthQuakeStart();//ȭ�� ��鸲ȿ�� ����

        bossAnimator.SetBool("RockShot", true); //�ִϸ��̼� ����

        //Instantiate(rockShotPrefab, transform.position, Quaternion.identity); //��������

        yield return new WaitForSeconds(rockShotDuration); //*�ʵ��� ����

        bossAnimator.SetBool("RockShot", false); //�ִϸ��̼� ����
        isPatternRunning = false;
    }

    void OnTriggerEnter2D(Collider2D other) //���� ��� �ִϸ��̼�
    {
        if (other.CompareTag("Arrow")) //�浹�� �Ͼ ������Ʈ�� �±װ� "Arrow"�ϋ�
        {
            bossHP--; //playerController.playerATK; //HP����

            if (bossHP <= 0) //ü���� 0���ϰ� �ȴٸ�
            {
                Debug.Log("�������");
                isPatternRunning = true; //������ �������� ���ϵ��� ���Ͻ����� ���·� ����

                StartCoroutine(BossDead());
            }
        }
    }
    IEnumerator BossDead() //���� ������Ʈ �ı�
    {
        bossAnimator.SetBool("BossDead", true); //���� ��� �ִϸ��̼� ���
        yield return new WaitForSeconds(1f);
        cameraMovement.earthQuakeStart2();//ȭ�� ��鸲ȿ�� ����
        yield return new WaitForSeconds(7f);
        Destroy(gameObject); //������Ʈ �ı�
    }
}