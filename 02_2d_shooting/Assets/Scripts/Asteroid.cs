using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int splitCount = 2;      //쪼개질 개수
    public int hitPoint = 3;        //hp(총알 버티는 수
    public float moveSpeed = 1.0f;  //이동속도
    public int score = 10;

    public Vector3 targetDir = Vector3.zero;
    public GameObject small;        //쪼개질 때 생길 작은 운석

    void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, 4);
    }

    void Update()
    {
        //1초에 30도씩 돌리기(반시게방향). 시계방향은 -30
        //transform.rotation *= Quaternion.Euler(0,0,30.0f * Time.deltaTime);  //회전은 덧셈x 뺄셈으루
        transform.Rotate(0, 0, 30.0f*Time.deltaTime);
        transform.Translate(targetDir * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Borderline"))
        {
            Debug.Log("닿음");
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
        float angle = 360.0f / (float)splitCount; //사이 각도 구하기

        for (int i = 0; i < splitCount; i++) //쪼개질 개수만큼 반복
        {
            GameObject obj = Instantiate(small); //작은 운석 생성
            obj.transform.position = transform.position; //기준 위치(큰 운석)로 이동
            obj.transform.Rotate(0, 0, angle * i); //사이 각도만큼 회전
        }
        Destroy(this.gameObject); //큰 운석 없앰
    }    
}

//보더라인에 닿으면 튕겨나가도록..