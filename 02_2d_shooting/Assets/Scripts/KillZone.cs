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

//壱団醤拝暗  : 錘汐戚 左希虞昔拭 願生檎 拙精錘汐生稽 鞠澗惟 焼艦虞 益撹 逃奄惟 馬壱粛製
//              左希虞昔拭 rigidbody 旋遂馬艦猿 錘汐 限聖 凶原陥 紫号生稽 崇送績...ばばばばばばばばばばばばばばばばば -> rigidbody 研 static竺舛敗