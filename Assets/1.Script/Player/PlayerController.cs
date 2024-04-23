using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f; //�÷��̾� �̵� �ӵ�
    public float playerRunSpeed = 10; //�÷��̾� �޸��� �ӵ�
    public float dashDistance = 3f; //�÷��̾� �뽬 �Ÿ�

    public float maxPlayerHP = 10; //�÷��̾� �ִ� ü��
    public float playerHP = 10; //�÷��̾� ü��
    public Slider healthSlider; //�÷��̾� ü�� �����̴�

    public float maxPlayerSP = 10; //�÷��̾� �ִ� ���׹̳�
    public float playerSP = 10; //�÷��̾� ���׹̳�
    public Slider staminaSlider; //�÷��̾� ���׹̳� �����̴�

    private bool isAttack = false; //�÷��̾ ���������� Ȯ���ϴ� ����

    Animator animator;
    SpriteRenderer spriteRenderer;
    public float moveHorizontal;
    public float moveVertical;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //�÷��̾� �̵� ��
        moveHorizontal = Input.GetAxis("Horizontal");
       
        moveVertical = Input.GetAxis("Vertical");

        //�÷��̾� �̵� �ӵ� ����
        float speed = playerSpeed;

        //�÷��̾� �޸���
        if (isAttack == false) //�������� �ƴ� ���
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))//����Ʈ�� ������ �ִٸ�
            {

                speed = playerRunSpeed; //�÷��̾� �ӵ��� RunSpeed�� ����
            }
        }

        //�÷��̾� �뽬
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�뽬 �ִϸ��̼� �־����**********
            Vector3 dashDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized; //�뽬 ���� ����
            transform.position += dashDirection * dashDistance; //�뽬 �Ÿ���ŭ �����̵�
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

    private void OnTriggerEnter2D(Collider2D collision) //���Ϳ� �浹 ��
    {
        if (collision.CompareTag("Mob"))
        {
            Debug.Log("���Ϳ� �浹");
            playerHP--;
            UpdatePlayerState(); //�÷��̾� ���� ������Ʈ
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TileMap")
        {
            if (collision.contacts[0].normal.y>0.7f)
            {
                //�÷��̾��� �Է°��� ����
                moveVertical = 0f;

            }
          
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
