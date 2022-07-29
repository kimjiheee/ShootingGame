using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : EnemySpawner
{
    public Transform target;
    public float targetLength = 10.0f;
    private Color gizmoColor;

    private void Awake()
    {
        target = transform.Find("Target");
    }

    protected override IEnumerator Spawn()
    {
        while(true)
        {
            yield return waitSecond;
            GameObject obj = Instantiate(enemy);    //�� ����
            obj.transform.position = this.transform.position;   //�� �ʱ� ��ġ ����
            obj.transform.Translate(Vector3.up * Random.Range(0.0f, randomRange));  //���� ������ ���̸�ŭ �ø���

            //�������� �������� ���ϱ�
            Vector3 toPosition = target.transform.position;// + Vector3.up * Random.Range(0.0f, targetLength);
            Asteroid asteroid = obj.GetComponent<Asteroid>();
            asteroid.targetDir = toPosition - obj.transform.position.normalized;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(target.position, target.position + Vector3.up * targetLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * randomRange);
    }
}
