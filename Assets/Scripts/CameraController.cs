using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타겟을 일정 거리를 유지하면서 부드럽게 따라감
public class CameraController : MonoBehaviour
{
    public Transform target;    //타겟 레퍼런스
    public Vector3 offset;      //타겟과의 거리터 벡터
    public float damping;       //카메라가 부드럽게 이동하는 정도 (damping)

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 => 카메라와의 거리벡터 구함
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCamPos = target.position + offset;

        //#Todo - 목표와 일정 거리 유지하면서 부드럽게 카메라가 따라가도록
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Time.deltaTime * damping);
        
    }
}
