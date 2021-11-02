using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       //Navigation 컴포넌트 불러줌

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;          //애니메이션 제어용
    private NavMeshAgent navAgent;      //이동 위해 레퍼런스 가져옴
    private Transform target;           //쫓아갈 목표
    private EnemyHealth enemyHealth;

    public float targetStopDistance;        //이 거리 안이면 멈춤

    //추적에 대한 로직을 구현
    public float traceSpeed = 6f;

    private Vector3 _traceTargetPos;                //쫓아갈 대상의 위치를 저장

    public Vector3 TraceTargetPos
    {
        get
        {
            return _traceTargetPos;
        }
        set
        {
            _traceTargetPos = value;
            navAgent.speed = traceSpeed;            //빨리 이동하도록

            if (navAgent.enabled == false)          //NavMeshAgent까 꺼져있으면 다시 켜줌
                navAgent.enabled = true;

            navAgent.destination = _traceTargetPos; //NavMeshAgent가 목표 지점으로 이동하도록
            navAgent.isStopped = false;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        //플레이어 레퍼런스 찾아옴
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //NavMeshAgent를 꺼주는 함수 - SendMessage로 호출될 것 => EnemyAI에서 호출될 것
    public void StopMove()
    {
        navAgent.enabled = false;
    }
}

/*
 *using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       //Navigation 컴포넌트 불러줌

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;          //애니메이션 제어용
    private NavMeshAgent navAgent;      //이동 위해 레퍼런스 가져옴
    private Transform target;           //쫓아갈 목표
    private EnemyHealth enemyHealth;

    public float targetStopDistance;        //이 거리 안이면 멈춤

    
    public List<Transform> waypoints;       //순찰 지점들을 저장하기 위한 List
    public int nextIdx;                     //다음 순찰 지점의 index

    //순찰 밑 추적에 대한 로직을 구현
    public float patrolSpeed = 2f;
    public float traceSpeed = 6f;

    private bool _patrolling;

    public bool Patrolling                  //패트롤 상태를 on/off 해주는 프로퍼티
    {
        get
        {
            return _patrolling;
        }
        set
        {
            _patrolling = value;
            if(_patrolling)
            {
                navAgent.speed = patrolSpeed;       //천천히 정찰하도록
                MoveToWaypoint();                   //다음 웨이포인트를 찾아가라
            }
        }
    }

    private Vector3 _traceTargetPos;                //쫓아갈 대상의 위치를 저장

    public Vector3 TraceTargetPos
    {
        get
        {
            return _traceTargetPos;
        }
        set
        {
            _traceTargetPos = value;
            navAgent.speed = traceSpeed;            //빨리 이동하도록

            if (navAgent.enabled == false)          //NavMeshAgent까 꺼져있으면 다시 켜줌
                navAgent.enabled = true;

            navAgent.destination = _traceTargetPos; //NavMeshAgent가 목표 지점으로 이동하도록
            navAgent.isStopped = false;
        }
    }

    //가장 가까운 웨이포인트 인덱스를 찾는 함수
    private int GetNearestWaypointIndex()
    {
        int nearestPointIndex = -1;

        float dist = Mathf.Infinity;

        //waypoint 리스트를 순환하면서
        for (int i = 0; i < waypoints.Count; i++)
        {
            //각 waypoint와의 거리를 계산
            float d = Vector3.Distance(transform.position, waypoints[i].position);

            //그 거리가 이전 waypoint와의 거리보다 짧으면
            if (d < dist)
            {
                //그 waypoint의 인덱스 값을 저장
                dist = d;
                nearestPointIndex = i;
            }
        }

        print("[GetNearestWaypointIndex] " + gameObject.name + " : " + nearestPointIndex);

        return nearestPointIndex;
    }
    
    public string waypointGroupName;            //내가 이동할 웨이포인트를 가지고 있는 게임오브젝트명

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        //플레이어 레퍼런스 찾아옴
        target = GameObject.FindGameObjectWithTag("Player").transform;

     
        //이동할 웨이포인트를 가지고 있는 게임오브젝트를 찾아서
        Transform waypointGroup = GameObject.Find(waypointGroupName).transform;
        
        //그 자식을 다 waypoint 리스트에 더해줌
        for (int i = 0; i < waypointGroup.childCount; i++)
            waypoints.Add(waypointGroup.GetChild(i));
    }

    private void Start()
    {
        //MoveToWaypoint();       //첫 목적지까지 가라
        nextIdx = GetNearestWaypointIndex();        //가장 가까운 웨이포인트의 인덱스를 찾고
        Patrolling = true;      //패트롤링 상태로 가라
    }

    // Update is called once per frame
    void Update()
    {
        //살아있으면
        if(enemyHealth.isDead == false)
        {
            //순찰 모드가 아닐 경우, 이후 로직을 수행하지 않음
            if (Patrolling == false)
                return;

            //다음 목적지까지 도착하면
            if(navAgent.remainingDistance < 0.5f)
            {
                //다음 목적지의 인덱스를 계산 (순환하도록)
                nextIdx = nextIdx + 1;
                nextIdx %= waypoints.Count;

                //목적지로 이동
                MoveToWaypoint();
            }
        }
    }

    //NavMeshAgent를 꺼주는 함수 - SendMessage로 호출될 것 => EnemyAI에서 호출될 것
    public void StopMove()
    {
        navAgent.enabled = false;
        Patrolling = false;
    }

    //웨이포인트의 다음 목적지까지 이동 명령을 내리는 함수
    private void MoveToWaypoint()
    {
        //최적의 경로가 아니면 리턴
        if (navAgent.isPathStale)
            return;

        //waypoints 배열에서 추출한 위치로 다음 목적지를 지정
        navAgent.destination = waypoints[nextIdx].position;

        //NavMeshAgent가 다시 이동하도록
        navAgent.isStopped = false;
    }
}
*/