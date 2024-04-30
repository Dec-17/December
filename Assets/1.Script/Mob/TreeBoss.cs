using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeBoss : MonoBehaviour
{
    //ü��
    public float bossHP = 50f; //ü��
    public float bossLeftHandHP = 30f; //�޼� ü��
    public float bossRightHandHP = 30f; //������ ü��

    private bool isPattern = false; //���� ���࿩��


    //�������� ����
    public GameObject energyBombPrefab; //�������� ������
    public float energyBombSpeed = 20f; //�ӵ�
    private float ShootMin = 0.5f; //�߻� �ּ� ����
    private float ShootMax = 1.5f; //�߻� �ִ� ����
    private float energyCoolTime = 20f; //���� ��Ÿ��
    private float energyDurationTime = 5f; //���� ���ӽð�
    private float nextShootTime; //���� �߻� �ð�

    //�� ��ȯ ����
    public GameObject spawnMonsterPrefab; //��ȯ �� ���� ������
    private float spawnTime = 30f; //���� ��Ÿ��
    public int minSpawnCount = 1; //�ּ� ��ȯ ���� ��
    public int maxSpawnCount = 3; //�ִ� ��ȯ ���� ��

    //�ٴ� �������


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

        nextShootTime = Time.time + Random.Range(ShootMin, ShootMax); //ù �߻� �ð� ����
        StartCoroutine(SpawnMonsterPattern()); //���Ͻ���
        StartCoroutine(EnergyePattern()); //���� ����
    }

    void Update()
    {

    }

    IEnumerator EnergyePattern() //�������� �߻� ����
    {
        while (true)
        {
            yield return new WaitForSeconds(energyCoolTime); //energyCoolTime��ŭ ���

            isPattern = true; //���� ����

            float patternEndTime = Time.time + energyDurationTime; //���� ����
            while (Time.time < patternEndTime)
            {
                if (isPattern && Time.time >= nextShootTime) //���� ���� ���� Ȯ�� �� �߻� �ð��� �Ǹ� �������� �߻�
                {
                    ShootEnergyBomb(); //�������� �߻�
                    nextShootTime = Time.time + Random.Range(ShootMin, ShootMax); //���� �߻� �ð� ����
                }
                yield return null;
            }
            isPattern = false; // ���� ����
        }
    }

    void ShootEnergyBomb() //�������� �߻�
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾��� ��ġ ã��
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position; //�÷��̾��� ��ġ�� Ÿ������ ����
            targetPosition.y -= 1f; //�÷��̾� ��ġ�� y��ǥ�� -1��ŭ ����

            GameObject energyBomb = Instantiate(energyBombPrefab, transform.position, Quaternion.identity); //������ �� ����
            Vector2 direction = (targetPosition - transform.position).normalized; //Ÿ���� ���ϴ� ���� ���
            Rigidbody2D rb = energyBomb.GetComponent<Rigidbody2D>(); //������ ���� ���� ���� �߻�
            rb.velocity = direction * energyBombSpeed;
        }
    }
    IEnumerator SpawnMonsterPattern() //���� ��ȯ ����
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime); //spawnTime��ŭ ���

            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1); //������ ���� �� ���� ����

            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition = transform.position; //���� ��ġ�� �ش� ������Ʈ�� ��ġ�� ����
                spawnPosition.y += Random.Range(-5f, -40f); //Y ��ǥ�� �����ϰ� ����
                spawnPosition.x += Random.Range(-40f, 40f); //X ��ǥ�� �����ϰ� ����

                Instantiate(spawnMonsterPrefab, spawnPosition, Quaternion.identity); //���� ����
            }
        }
    }
}
