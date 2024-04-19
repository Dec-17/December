using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public   bool bowVisible = false; //Ȱ�� ���̴��� ���θ� ��Ÿ���� ����
    public GameObject BowObj; //Ȱ ������Ʈ�� ���� ����
    public Animator bowAnimator; //Ȱ�� �ִϸ�����
    public GameObject ArrowObj; //ȭ�� ������Ʈ�� ���� ����
    public Transform ArrowSpawnPoint; //ȭ���� ������ ��ġ
    public float arrowInterval = 0.7f; //ȭ�� ���� ����

    public GameObject PlayerObj; //�÷��̾� ������Ʈ�� ���� ����
    public Animator PlayerAnimator; //�÷��̾��� �ִϸ�����
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    public bool IsPlayerX = false;
    public bool IsPlayerDown = false;
    public bool IsPlayerUp = false;


    void Start()
    {
        bowAnimator = BowObj.GetComponent<Animator>();
        playerController = PlayerObj.GetComponent<PlayerController>();
        spriteRenderer = PlayerObj.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        //���콺 ��ġ�� ���� Ȱ ȸ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        float bowRotationZ = transform.rotation.z;
        Vector3 playerToMouse = mousePos - transform.position;

        //�÷��̾� �̵� ���⿡ ���� �ִϸ��̼� ����

        if (playerController.moveHorizontal < 0f) //�÷��̾� ���� �̵�
        {
            IsPlayerX = true;
            spriteRenderer.flipX = true; //�¿� �ø�
            if (playerToMouse.x > 0 && Input.GetMouseButton(0)) //�������� Ȱ�� ��ٸ�
            {
                spriteRenderer.flipX = false;
            }
            else if (playerToMouse.x < 0 && Input.GetMouseButton(0)) //�������� Ȱ�� ��ٸ�
            {
                spriteRenderer.flipX = true;
            }
        }
        else if(playerController.moveHorizontal == 0f)
        {
            IsPlayerX = false;
        }

        if (playerController.moveHorizontal > 0f) //�÷��̾� ���� �̵�
        {

            IsPlayerX = true;
            spriteRenderer.flipX = false; //�¿� �ø� ����
            if (playerToMouse.x > 0 && Input.GetMouseButton(0)) //�������� Ȱ�� ��ٸ�
            {
                spriteRenderer.flipX = false;
            }
            else if (playerToMouse.x < 0 && Input.GetMouseButton(0)) //�������� Ȱ�� ��ٸ�
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (playerController.moveHorizontal == 0f)
        {
            IsPlayerX = false;
        }

        if (playerController.moveVertical < 0f) //�÷��̾� �ϴ� �̵�
        {
            IsPlayerDown = true;
        }
        else
        {
            IsPlayerDown = false;
        }

        if (playerController.moveVertical > 0f) //�÷��̾� ��� �̵�
        {
            IsPlayerUp = true;
        }
        else
        {
            IsPlayerUp = false;
        }


        if (Input.GetMouseButton(0))
        {
            if (!bowVisible)
            {
                SetBowAlpha(1f); //���� ���� 1�� �����Ͽ� ���̰� ��
                bowAnimator.SetBool("Attack", true); //ȭ�� �߻� �ִϸ��̼� ���
                bowVisible = true; //Ȱ�� ���̴� ���·� ����    
                InvokeRepeating(nameof(CreateArrow), 0f, arrowInterval);//���� �������� ȭ�� �����ϴ� �Լ� ȣ��
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.flipX = false; //Ȱ�� ��� ���� �����Ƿ� �ø� ��Ȱ��ȭ

            StartCoroutine(HideBowAfterDelay(0.1f)); //x�� �Ŀ� Ȱ�� ����� �ڷ�ƾ ����

            CancelInvoke(nameof(CreateArrow));//ȭ�� ���� ���� �Լ� ȣ�� ����
        }

        PlayerAnimator.SetBool("PlayerX", IsPlayerX); //�¿��̵� �ִϸ��̼� ����
        PlayerAnimator.SetBool("PlayerDown", IsPlayerDown); //�ϴ��̵� �ִϸ��̼� ����
        PlayerAnimator.SetBool("PlayerUp", IsPlayerUp); //����̵� �ִϸ��̼� ����

    }

    void CreateArrow()
    {
        // ȭ�� ���� �� ȸ�� ����
        GameObject arrow = Instantiate(ArrowObj, ArrowSpawnPoint.position, Quaternion.identity);
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z));
    }

    void SetBowAlpha(float alpha) //Ȱ�� ���� ���� ����
    {
        SpriteRenderer bowSpriteRenderer = BowObj.GetComponent<SpriteRenderer>();
        if (bowSpriteRenderer != null)
        {
            Color bowColor = bowSpriteRenderer.color;
            bowColor.a = alpha;
            bowSpriteRenderer.color = bowColor;
        }
    }

    IEnumerator HideBowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //delay�� ���� ���

        SetBowAlpha(0f); //Ȱ�� ���� ���� 0���� �����Ͽ� ����
        bowAnimator.SetBool("Attack", false); //ȭ�� �߻� �ִϸ��̼� ����
        bowVisible = false; //Ȱ�� ������ ���·� ����
    }
}
