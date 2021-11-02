using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState          //적의 상태를 나타내기 위한 열거형 정의
    {
        TRACE,
        ATTACK,
        DIE
    }

    public EnemyState STATE = EnemyState.TRACE;        //상태를 저장할 변수
    private Transform playerTransform;                  //플레이어의 위치를 저장할 변수
    public float traceDist = 10f;                       //추적 사정거리
    public float attackDist = 3f;                       //공격 사정거리

    //상태에 따라 명령을 내리기 위해 각 스크립트의 레퍼런스를 가져옴
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        //각 컴포넌트 초기화
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();

        //플레이어의 transform을 찾아서 저장
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    //적 캐릭터의 상태를 검사하는 코루틴 함수
    //※Update에 넣어도 되지만, 매 프레임마다 검사하는 건 비용이 크기 떄문에 코루틴으로 일정 시간 간격마다 실행해줌
    private IEnumerator CheckState()
    {
        //적이 죽기 전까지는 계속 도는 while 루프
        while(!enemyHealth.isDead)
        {
            //상태가 사망이면 코루틴 함수를 종료
            if (STATE == EnemyState.DIE)
                yield break;

            //플레이어와 적 캐릭터의 거리를 계산
            float dist = Vector3.Distance(playerTransform.position, transform.position);

            //공격 사정거리 이내면 공격 상태로 가고
            if(dist <= attackDist)
            {
                STATE = EnemyState.ATTACK;
            }
            //추적 사정거리 이내면 추적 상태로 가고
            else if(dist <= traceDist)
            {
                STATE = EnemyState.TRACE;
            }
            //0.3초 대기
            yield return new WaitForSeconds(0.3f);
        }
    }

    //상태에 따라 적 캐릭터의 행동을 처리하는 코루틴 함수
    private IEnumerator Action()
    {

        while (!enemyHealth.isDead)
        {
            //0.3초 대기
            yield return new WaitForSeconds(0.3f);

            //상태에 따라 분기 처리
            switch (STATE)
            {
                case EnemyState.TRACE:
                    enemyMovement.TraceTargetPos = playerTransform.position;        //플레이어의 위치를 따라가라

                    break;
                case EnemyState.ATTACK:


                    break;
                case EnemyState.DIE:
                    enemyMovement.StopMove();       //이동 멈춰라
                    break;
            }
        }

        
    }
    //선택하면 Gizmo를 그려주는 함수
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, traceDist);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, attackDist);
    }
}
