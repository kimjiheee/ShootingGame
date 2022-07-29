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
            GameObject obj = Instantiate(enemy);    //적 생성
            obj.transform.position = this.transform.position;   //적 초기 위치 설정
            obj.transform.Translate(Vector3.up * Random.Range(0.0f, randomRange));  //적을 랜덤한 높이만큼 올리기

            //도착지점 랜덤으로 정하기
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
