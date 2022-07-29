using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Destroy(collision.gameObject);
    }
}

//고쳐야할거  : 운석이 보더라인에 닿으면 작은운석으로 되는게 아니라 그냥 튕기게 하고싶음
//              보더라인에 rigidbody 적용하니까 운석 맞을 때마다 사방으로 움직임...ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠ -> rigidbody 를 static설정함