using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//특정 종류의 적을 일정 간격으로 생성한다
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      //생성할 적 프리팝 레퍼런스
    public Transform[] enmeySpwanPosition;

    public static int enemyCount = 0;       //적 개수 스테틱 변수
    public const int MAX_ENEMY_COUNT = 10;  //contant = 상수

    //적을 생성하는 함수
    IEnumerator Spawn()
    {
        for (int i = 0; i < MAX_ENEMY_COUNT; i++)
        {
            Instantiate(enemyPrefab, enmeySpwanPosition[i].position, transform.rotation);
            yield return new WaitForSeconds(1f);
            EnemySpawner.enemyCount++;
        }  
    }
    void Start()
    {
        StartCoroutine(Spawn());        
    }
}
