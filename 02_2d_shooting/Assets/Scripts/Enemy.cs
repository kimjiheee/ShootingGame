using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject explosion;
    public int score = 5;

    void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime); //계속 왼쪽으로 날라가는거라 -transform.right
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameManager.Inst.Score += score;
        }

        explosion.transform.parent = null;  //explosion은 부모가 없다고 선언해줌.
                                            //이거 안하면 부모(enemy)가 죽으면 자식(explosion)도 죽기 때문에 폭발이 안 나옴
        explosion.SetActive(true);

        Destroy(this.gameObject);
    }


}
