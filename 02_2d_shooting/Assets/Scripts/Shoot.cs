using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 3.0f; //�Ѿ��� ����

    Rigidbody2D rigid;
    public GameObject hit;

    bool isOnDestroy = false;   //OnCollisionEnter �ߺ����� ������

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

     void Start()  //awake�� start�� ������ �� �� �� ���� ����
    {
        rigid.velocity = transform.right * speed; //���۵Ǹ� ��� ���������� ���ư�

        Destroy(this.gameObject, lifeTime); //lifetime�� �Ŀ� ���� ������Ʈ ����
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
