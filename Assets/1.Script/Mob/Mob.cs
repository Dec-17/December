using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mob : MonoBehaviour
{
    public float mobHP; //�� ������Ʈ�� ü��
    public GameObject[] NomalItem; //�Ϲ� ������ �迭
    public GameObject[] EpicItem; //��� ������ �迭
    public float NomalItemProbability; //�Ϲ� ������ ��� Ȯ��
    public float EpicItemProbability; //��� ������ ��� Ȯ��    // Ȯ�� 1 = 100%

    public float mobSpeed; //���� �̵� �ӵ�
    public float detectionRange; //���Ͱ� �÷��̾ ������ ����     //20���� �ϸ� ȭ�鿡 ���϶��� �����

    public float damageColorDuration = 0.2f; //�ǰ� �� ��������Ʈ ������ ����Ǵ� �ð�
    public Color damageColor = new Color(1f, 0.5f, 0.5f); //�ǰ� �� ����� ��������Ʈ�� ����
    public Color originalColor = new Color(1f, 1f, 1f); //���� �⺻ ��������Ʈ ����

    PlayerController playerController;
    public Rigidbody2D mobRigidbody;
    public Animation mobAnimation;
    SpriteRenderer mobRenderer;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        mobRigidbody = GetComponent<Rigidbody2D>();
        mobAnimation = GetComponent<Animation>();
        mobRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        
    }
  
    virtual public void Testvirtual()
    {
        Debug.Log("�������̵� �׽�Ʈ");
    }

    void Update()
    {
        FindPlayer();
    }

    public void FindPlayer() //�÷��̾� ����
    {
        PlayerController player = FindObjectOfType<PlayerController>(); //������ �÷��̾� ã��

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRange) //�÷��̾ ���� ���� ���� ���� �� �̵�
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mobSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) //�浹ó��
    {
        if (other.CompareTag("Arrow")) //�浹�� �Ͼ ������Ʈ�� �±װ� "Arrow"�ϋ�
        {
            mobHP--; //playerController.playerATK; //HP����



            if (mobHP <= 0) //ü���� 0���ϰ� �ȴٸ�
            {
                StartCoroutine(DestroyWithFade()); //������ ���������� �ı�
            }
            else //ü���� 0���ϰ� �ƴ϶��
            {
                StartCoroutine(FlashDamageColor()); //�ǰ� �� �Ͻ������� ��������Ʈ ���� ����
            }
        }
    }

    IEnumerator FlashDamageColor()
    {
        mobRenderer.color = damageColor; //���� ����
        yield return new WaitForSeconds(damageColorDuration); //damageColorDuration�ð� ���� ���
        mobRenderer.color = originalColor; //���� �������� �ǵ���
    }

    IEnumerator DestroyWithFade()
    {
        float fadeDuration = 0.5f; //������ ������� �ð�
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float startAlpha = renderer.color.a;

        //���İ� ������ ����
        for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime)
        {
            Color newColor = renderer.color;
            newColor.a = Mathf.Lerp(startAlpha, 0.0f, t / fadeDuration);
            renderer.color = newColor;
            yield return null;
        }
        Destroy(gameObject); //������Ʈ �ı�
        DropNomalItems(); //�Ϲ� ������ ��� �޼��� ����
        DropEpicItems(); //��� ������ ��� �޼��� ����
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
