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
    public float bossLeftHandHP = 30f; //�޼� ü��
    public float bossRightHandHP = 30f; //������ ü��

    [Header("������ ����")]
    //������ ����
    public GameObject razerPrefab; //������ ������
    public GameObject razerWarningPrefab; //������ ��� ������
    public GameObject bossEffect; //������ ������ ����Ʈ
    public float razerDuration = 10; //������ ���� ���ӽð�

    [Header("����ȯ ����")]
    //����ȯ ����
    public GameObject spawnMonsterPrefab; //��ȯ�� ������
    public int minSpawnCount = 1; //�ּ� ��ȯ ���� ��
    public int maxSpawnCount = 3; //�ִ� ��ȯ ���� ��
    public float mobSpawnDuration = 20; //����ȯ ���� ���ӽð�

    [Header("ź�� ����")]
    //ź�� ����
    public GameObject energyBombPrefab; //ź�� ������
    public GameObject energyChargePrefab; //ź�� ��¡ ��� ������
    public float energyBombSpeed = 20f; //ź�� �ӵ�
    public float energyBombDuration = 10; //ź�� ���� ���ӽð�

    [Header("���� ����")]
    //���� ����
    public GameObject rockShotPrefab; //���� ������
    public float rockShotDuration = 10; //���� ���� ���ӽð�


    //����ġ ����
    bool isPatternRunning = false;

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
        Debug.Log("������ ���� ����");
        isPatternRunning = true;

        //bossEffect.SetActive(true); //bossEffect�� Ȱ��ȭ
        bossAnimator.SetBool("Razer", true); //�ִϸ��̼� ����


        //yield return new WaitForSeconds(1f); //bossEffect�� *�� �Ŀ� ��Ȱ��ȭ
        bossEffect.SetActive(false);

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

        bossAnimator.SetBool("Razer", false); //�ִϸ��̼� ����
        isPatternRunning = false;
    }

    IEnumerator MobSpawnPattern() //2.����ȯ ����
    {
        Debug.Log("����ȯ ���� ����");
        isPatternRunning = true; //���� ������

        bossAnimator.SetBool("MobSpawn", true); //�ִϸ��̼� ����

        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1); //���� �� ����

        for (int i = 0; i < spawnCount; i++) //���� ���� ��ġ�� ����
        {
            float randomX = Random.Range(50f, 100f); //x�� ���� ��

            float randomY = Random.Range(1.5f, -35f); //y�� ���� ��

            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f); //���� ��ġ ����

            Instantiate(spawnMonsterPrefab, spawnPosition, Quaternion.identity); //�� ����
        }

        yield return new WaitForSeconds(mobSpawnDuration); //mobSpawnDuration ��ŭ ���

        bossAnimator.SetBool("MobSpawn", false); //�ִϸ��̼� ����
        isPatternRunning = false; //���� ����

    }

    IEnumerator EnergyBombPattern() //3.ź�� ����
    {
        isPatternRunning = true;
        Debug.Log("ź�� ���� ����");

        bossAnimator.SetBool("EnergyBomb", true); //�ִϸ��̼� ����

        yield return new WaitForSeconds(1f); //*�ʵ��� ����

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

    void OnTriggerEnter2D(Collider2D other) //�浹ó��
    {
        if (other.CompareTag("Arrow")) //�浹�� �Ͼ ������Ʈ�� �±װ� "Arrow"�ϋ�
        {
            bossHP--; //playerController.playerATK; //HP����

            if (bossHP <= 0) //ü���� 0���ϰ� �ȴٸ�
            {
                Destroy(gameObject);
            }
        }
    }
}
//�ִϸ��̼ǿ� ���缭 �������� �ٵ��

//���������� ���� �� �ִϸ��̼Ǹ� ���
//�����ϴ� �κ��� �ż���� ����
//�ִϸ��̼� �̺�Ʈ�� ����
//�ִϸ��̼��� ������ ���ݵ� ����