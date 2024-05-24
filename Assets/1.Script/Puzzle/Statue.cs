using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
   Puzzle puzzle;

    void Start()
    {
        //puzzle = GetComponent<Puzzle>();
        puzzle = GetComponentInParent<Puzzle>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow")) //충돌한 오브젝트의 태그가 "Arrow"일 때
        {
            if (gameObject.name == "Statue01") //이 스크립트를 가진 오브젝트의 이름이 "Statue01"일 때
            {
                puzzle.password += "1"; 
            }
            else if(gameObject.name == "Statue02")
            {
                puzzle.password += "2";
            }
            else if (gameObject.name == "Statue03")
            {
                puzzle.password += "3";
            }
            else
            {
                puzzle.password += "4";
                Debug.Log(puzzle.password);
            }
        }
    }
}
