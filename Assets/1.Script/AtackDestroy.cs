using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackDestroy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.name.Contains("Energy")) //�̸��� Energy�� ���ٸ�
        {
            Destroy(gameObject, 5f); //*�� �� ������Ʈ �ı�
        }
        if (gameObject.name.Contains("Razer")) //Razer�� ���ٸ�
        {
            Destroy(gameObject, 2f); //*�� �� ������Ʈ �ı�
        }
        if (gameObject.name.Contains("warning")) //warning�� ���ٸ�
        {
            Destroy(gameObject, 0.5f); //*�� �� ������Ʈ �ı�
        }
    }
}
