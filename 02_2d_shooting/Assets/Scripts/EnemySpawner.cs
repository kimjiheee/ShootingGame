using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;            //������ ��
    public float spawnInterval = 1.0f;  //���� ����
    public float randomRange = 8.0f;    //���� ���� ����
    public Color myGizmoColor = Color.white;

    protected WaitForSeconds waitSecond;          //�ڷ�ƾ���� ����� ���� �ð� ���

    void Start()
    {
        waitSecond = new WaitForSeconds(spawnInterval);
        StartCoroutine(Spawn());
    }

    protected virtual IEnumerator Spawn()
    {
        while(true)
        {
            yield return waitSecond;    //������ �ð���ŭ ���
            GameObject obj = Instantiate(enemy);        //�� ����
            obj.transform.position = this.transform.position;   //�� �ʱ� ��ġ ����
            obj.transform.Translate(Vector3.up * Random.Range(0.0f, randomRange));  //���� ������ ���̸�ŭ �ö�..
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = myGizmoColor;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * randomRange);
    }
}
