using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f; //�÷��̾� �̵� �ӵ�
    public float playerRunSpeed = 10; //�÷��̾� �޸��� �ӵ�

    public float maxPlayerHP = 10; //�÷��̾� �ִ� ü��
    public float playerHP = 10; //�÷��̾� ü��
    public Slider healthSlider; //�÷��̾� ü�� �����̴�

    public float maxPlayerSP = 10; //�÷��̾� �ִ� ���׹̳�
    public float playerSP = 10; //�÷��̾� ���׹̳�
    public Slider staminaSlider; //�÷��̾� ���׹̳� �����̴�

    Animator animator;
    SpriteRenderer spriteRenderer;
    public float moveHorizontal;
    public float moveVertical;
    Bow bow;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //�÷��̾� �̵� ��
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        //�÷��̾� �̵� �ӵ� ����
        float speed = playerSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))//����Ʈ�� ������ �ִٸ�
        {
            //�������� �ƴ� ��쿡�� �޸� �� �ֵ��� ����
            speed = playerRunSpeed; //�÷��̾� �ӵ��� RunSpeed�� ����
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob")) //���Ϳ� �浹 ��
        {
            Debug.Log("���Ϳ� �浹");
            playerHP--;
            UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
        }
    }

    public IEnumerator StaminaAutoHeal() //���׹̳� �ڵ� ȸ��
    {
        yield return new WaitForSeconds(0.01f); //0.01�ʸ���
        playerSP += 0.05f; //0.05�� ���¹̳� ȸ��
        playerSP = Mathf.Min(playerSP, maxPlayerSP); //�ִ� ���׹̳ʸ� ���� �ʵ��� ����
        UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
    }
}
