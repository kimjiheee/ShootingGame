using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_small : MonoBehaviour
{
    public float speed = 3.0f;
    public int score = 1;

    void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, 4);
        renderer.flipX = ((rand & 0b_01) != 0);
        renderer.flipY = ((rand & 0b_10) != 0);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Borderline"))
        //{
        //    Debug.Log("���༺ ����");
        //}

        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameManager.Inst.Score += score;
            
        }
        Destroy(this.gameObject);
    }
}

//��ó�� ƨ��� �ϰ������.. bounce ���� �ص� �ȵż� �����ؾ����� �𸣰���
