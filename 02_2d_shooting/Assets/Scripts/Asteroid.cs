using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int splitCount = 2;      //�ɰ��� ����
    public int hitPoint = 3;        //hp(�Ѿ� ��Ƽ�� ��
    public float moveSpeed = 1.0f;  //�̵��ӵ�
    public int score = 10;

    public Vector3 targetDir = Vector3.zero;
    public GameObject small;        //�ɰ��� �� ���� ���� �

    void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, 4);
    }

    void Update()
    {
        //1�ʿ� 30���� ������(�ݽðԹ���). �ð������ -30
        //transform.rotation *= Quaternion.Euler(0,0,30.0f * Time.deltaTime);  //ȸ���� ����x ��������
        transform.Rotate(0, 0, 30.0f*Time.deltaTime);
        transform.Translate(targetDir * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Borderline"))
        {
            Debug.Log("����");
        }
            hitPoint--;
        //Sound.instance.PlaySound();
    
        if (hitPoint < 1)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                GameManager.Inst.Score += score;
            }
            Crush();
        }
    }

    void Crush()
    {
        float angle = 360.0f / (float)splitCount; //���� ���� ���ϱ�

        for (int i = 0; i < splitCount; i++) //�ɰ��� ������ŭ �ݺ�
        {
            GameObject obj = Instantiate(small); //���� � ����
            obj.transform.position = transform.position; //���� ��ġ(ū �)�� �̵�
            obj.transform.Rotate(0, 0, angle * i); //���� ������ŭ ȸ��
        }
        Destroy(this.gameObject); //ū � ����
    }    
}

//�������ο� ������ ƨ�ܳ�������..