using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rigid;
    private Animator animator;      //애니메이터 컴포넌트 레퍼런스
    private bool isCast = false;
    private AudioSource audioSource;

    public float speed;         //이동 속력

    public LayerMask targetLayer;       //Ray와 충돌을 체크할 레이어마스크

    // Start() 이전, 컴포넌트 초기화
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Rigidbody로 이동할 거라 FixedUpdate로 해줌
    void FixedUpdate()
    {
        //인풋을 받고
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);         //이동 처리
        Turn();             //캐릭터가 마우스 커서 바라보도록
        Animate(h, v);      //애니메이션 처리
    }

    private void Move(float h, float v)
    {
        //한 프레임 당 움직이는 정도
        Vector3 movement = new Vector3(h, 0, v);

        //이동 속도 설정 
        //벡터를 정규화(normalize)해서 대각선으로도 같은 속도로 가도록 해줌
        movement = movement.normalized * speed * Time.fixedDeltaTime;

        //Rigidbody를 통해 이동
        rigid.MovePosition(rigid.position + movement);
    }

    private void Turn()
    {
        //스크린의 마우스 위치에서 수직으로 발사되는 Ray를 저장
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;     //레이 히트 정보를 저장할 변수

        //camRay를 쏴서 Floor레이어와 부딪히는지를 체크, 부딪히면 값은 RaycastHit hit에 저장
        if(Physics.Raycast(camRay, out hit, 1000f, targetLayer))
        {
            //Ray가 어디 부딪혔을 때 실행하는 코드

            //플레이어 => 마우스히트지점 을 향하는 벡터
            Vector3 playerToMouse = hit.point - rigid.position;
            
            //캐릭터가 playerToMouse 벡터 방향을 향하도록 해줌
            rigid.MoveRotation(Quaternion.LookRotation(playerToMouse));

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Casting();
            }
        }
    }

    private void Animate(float h, float v)
    {
        //가로나 세로 이동 인풋값이 있으면
        bool walking = false;

        if (h != 0 || v != 0)
        {
            walking = true;
            audioSource.Play();
        }
        else if (h == 0 && v == 0) audioSource.Pause();
        //이동 애니메이션을 재생해라
        animator.SetBool("IsWalking", walking);
    }

    private void Update()
    {
        if (isCast)
        {
            animator.SetBool("Casting", true);
            isCast = false;
        }
        else
        {
            animator.SetBool("Casting", false);
        }
    }

    private void Casting()
    {
        isCast = true;
    }
}
