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
        transform.Translate(-transform.right * speed * Time.deltaTime); //��� �������� ���󰡴°Ŷ� -transform.right
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameManager.Inst.Score += score;
        }

        explosion.transform.parent = null;  //explosion�� �θ� ���ٰ� ��������.
                                            //�̰� ���ϸ� �θ�(enemy)�� ������ �ڽ�(explosion)�� �ױ� ������ ������ �� ����
        explosion.SetActive(true);

        Destroy(this.gameObject);
    }


}
