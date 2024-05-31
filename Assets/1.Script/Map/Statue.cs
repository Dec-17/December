using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
   Puzzle puzzle;
    public GameObject statue01;
    public GameObject statue02;
    public GameObject statue03;
    public GameObject statue04;

    void Start()
    {
        puzzle = GetComponentInParent<Puzzle>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow")) //�浹�� ������Ʈ�� �±װ� "Arrow"�� ��
        {
            if (gameObject.name == "Statue01") //�� ��ũ��Ʈ�� ���� ������Ʈ�� �̸��� "Statue01"�� ��
            {
                puzzle.password += "1";
                statue01.SetActive(true);
                gameObject.SetActive(false);
            }
            else if(gameObject.name == "Statue02")
            {
                puzzle.password += "2";
                statue02.SetActive(true);
                gameObject.SetActive(false);
            }
            else if (gameObject.name == "Statue03")
            {
                puzzle.password += "3";
                statue03.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                puzzle.password += "4";
                statue04.SetActive(true);
                gameObject.SetActive(false);
                Debug.Log(puzzle.password);
            }
        }
    }
}
