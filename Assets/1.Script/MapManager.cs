using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    public Transform map01;
    public Transform map02;
    public Transform map03;
    public Transform map04;

    public GameObject cam01;
    public GameObject cam02;
    public GameObject cam03;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Map01")
        {
            Vector3 newPosition = new Vector3(map02.position.x, map02.position.y + 2, map02.position.z);
            transform.position = newPosition;
            cam01.SetActive(false);
            cam02.SetActive(true);
            cam03.SetActive(false);
        }
        if (other.gameObject.name == "Map02")
        {
            Vector3 newPosition = new Vector3(map01.position.x, map01.position.y - 1.6f, map01.position.z);
            transform.position = newPosition;
            cam01.SetActive(true);
            cam02.SetActive(false);
            cam03.SetActive(false);
        }
        if (other.gameObject.name == "Map03")
        {
            Vector3 newPosition = new Vector3(map04.position.x, map04.position.y + 2, map04.position.z);
            transform.position = newPosition;
            cam01.SetActive(false);
            cam02.SetActive(false);
            cam03.SetActive(true);
        }
        if (other.gameObject.name == "Map04")
        {
            Vector3 newPosition = new Vector3(map03.position.x, map03.position.y - 2, map03.position.z);
            transform.position = newPosition;
            cam01.SetActive(false);
            cam02.SetActive(true);
            cam03.SetActive(false);
        }
    }
}