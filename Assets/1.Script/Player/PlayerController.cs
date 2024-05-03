using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("�ӵ�")]
    public float playerSpeed = 5f; //�÷��̾� �̵� �ӵ�
    public float playerRunSpeed = 10; //�÷��̾� �޸��� �ӵ�

    [Header("ü��")]
    public float maxPlayerHP = 10; //�÷��̾� �ִ� ü��
    public float playerHP = 10; //�÷��̾� ü��
    public Slider healthSlider; //�÷��̾� ü�� �����̴�

    [Header("���׹̳�")]
    public float maxPlayerSP = 10; //�÷��̾� �ִ� ���׹̳�
    public float playerSP = 10; //�÷��̾� ���׹̳�
    public Slider staminaSlider; //�÷��̾� ���׹̳� �����̴�

    private bool isAttack = false; //�÷��̾ ���������� Ȯ���ϴ� ����

    [Header("�������ͽ�")]
    //�÷��̾� �������ͽ�
    public float playerATK = 1; //�÷��̾� ���ݷ�
    public float playerDEF = 1; //�÷��̾� ����

    private bool invincible = false; //�÷��̾��� ���� ���� ����
    public float invincibleDuration = 0.5f; //���� ���� ���� �ð�

    [Header("��������Ʈ")]
    public float playerColorDuration = 0.3f; //�ǰ� �� ��������Ʈ ������ ����Ǵ� �ð�
    public Color playerDamageColor = new Color(1f, 0.6f, 0.6f, 1f); //�ǰ� �� ����� ��������Ʈ�� ����
    public Color playerOriginalColor = new Color(1f, 1f, 1f, 1f); //�÷��̾��� �⺻ ��������Ʈ ����

    [Header("�̵� ��")]
    public float moveHorizontal;
    public float moveVertical;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D playerRigidbody;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //�÷��̾� �޸���
        if (isAttack == false && Input.GetKeyDown(KeyCode.LeftShift)) //�������� �ƴϰ� ����Ʈ�� ������ �ִٸ�
        {
            Debug.Log("�޸�����");
            playerSpeed = playerRunSpeed; //�÷��̾� �ӵ��� RunSpeed�� ����
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || isAttack == true) //������ �����߰ų� ����Ʈ �Է��� ����ߴٸ�
        {
            Debug.Log("�޸��� ����");
            playerSpeed = 5f; //�÷��̾� �ӵ��� �ʱ�ȭ
        }

        if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift)) //������ ���������� ����Ʈ�� ������ �ִٸ�
        {
            Debug.Log("�޸�����");
            playerSpeed = playerRunSpeed; //�÷��̾� �ӵ��� RunSpeed�� ����
        }

        //�÷��̾� ���� ���� üŷ
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true; //���콺 Ŭ���� �ϸ� ȭ���� �߻��ϹǷ� ���� ���·� ����
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAttack = false; //�������� �ƴ� ���·� ����
        }
    }

    void FixedUpdate()
    {
        //�÷��̾� �̵� ��
        moveHorizontal = Input.GetAxis("Horizontal");

        moveVertical = Input.GetAxis("Vertical");

        //�÷��̾� �̵� �ӵ� ����
        float speed = playerSpeed;

        //�÷��̾� �̵�   
        Vector3 moveDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime);

        UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
        Dead(); //�÷��̾� ���
    }

    void Dead() //�÷��̾� ���
    {
        if (playerHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerState() //�÷��̾� ���� ������Ʈ
    {
        if (healthSlider != null)
        {
            healthSlider.value = playerHP / maxPlayerHP; //�÷��̾��� ü�� �ۼ�Ʈ��ŭ value�� ����
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = playerSP / maxPlayerSP; //�÷��̾��� ���׹̳� �ۼ�Ʈ��ŭ value�� ����
        }
    }

    private void OnCollisionStay2D(Collision2D collision) //�浹 ó��
    {
        if (!invincible && collision.gameObject.CompareTag("Mob")) //�÷��̾ ���� ���°� �ƴϰ�, ���Ϳ� �浹�ߴٸ�
        {
            Debug.Log("���Ϳ� �浹");
            playerHP--;
            StartCoroutine(DamageColorChange()); // �ǰ� �� ��������Ʈ ���� ����
            StartCoroutine(InvincibleMode()); //���� ���·� ��ȯ
            UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible && collision.gameObject.CompareTag("Mob")) //�÷��̾ ���� ���°� �ƴϰ�, ���Ϳ� �浹�ߴٸ�
        {
            Debug.Log("���Ϳ� �浹");
            playerHP--;
            StartCoroutine(DamageColorChange()); // �ǰ� �� ��������Ʈ ���� ����
            StartCoroutine(InvincibleMode()); //���� ���·� ��ȯ
            UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
        }
    }

    IEnumerator DamageColorChange() //�ǰ� �� ��������Ʈ ���� �����ϴ� �ڷ�ƾ
    {
        spriteRenderer.color = playerDamageColor; //�ǰ� �� ���� ����
        yield return new WaitForSeconds(playerColorDuration); //���� �ð� ���� ���
        spriteRenderer.color = playerOriginalColor; //���� �ð��� ���� �� ���� �������� ����
    }


    IEnumerator InvincibleMode() //���� ���·� ��ȯ�ϴ� �ڷ�ƾ
    {
        invincible = true; //�÷��̾ ���� ���·� ����
        Debug.Log("��������");
        yield return new WaitForSeconds(invincibleDuration); //���� �ð� ���� ���
        invincible = false; //���� �ð��� ���� �� ���� ���� ����
        Debug.Log("�������� ����");
    }

    public IEnumerator StaminaAutoHeal() //���׹̳� �ڵ� ȸ��
    {
        yield return new WaitForSeconds(0.01f); //0.01�ʸ���
        playerSP += 0.05f; //0.05�� ���¹̳� ȸ��
        playerSP = Mathf.Min(playerSP, maxPlayerSP); //�ִ� ���׹̳ʸ� ���� �ʵ��� ����
        UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
    }
}