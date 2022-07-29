using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 3.0f; //총알의 수명

    Rigidbody2D rigid;
    public GameObject hit;

    bool isOnDestroy = false;   //OnCollisionEnter 중복실행 방지용

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

     void Start()  //awake나 start는 시작할 때 딱 한 번만 실행
    {
        rigid.velocity = transform.right * speed; //시작되면 계속 오른쪽으로 나아감

        Destroy(this.gameObject, lifeTime); //lifetime초 후에 게임 오브젝트 삭제
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOnDestroy)
        {
            isOnDestroy = true;

            if(collision.gameObject.CompareTag("Enemy"))
            {
                hit.transform.parent = null;
                hit.transform.position = collision.contacts[0].point;
                hit.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
}
