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

//���ľ��Ұ�  : ��� �������ο� ������ ��������� �Ǵ°� �ƴ϶� �׳� ƨ��� �ϰ����
//              �������ο� rigidbody �����ϴϱ� � ���� ������ ������� ������...�ФФФФФФФФФФФФФФФФ� -> rigidbody �� static������