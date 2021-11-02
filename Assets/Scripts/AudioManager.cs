using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사운드이펙트 / 배경 음악 등 모든 오디오 관련 작업을 처리하는 클래스
public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] clips;       //모든 AudioClip을 저장할 배열

    public AudioSource audioPrefab; //AudioSource를 가지고 있는 프리팝

    //이름으로 플레이할 사운드를 찾아서 재생해주는 함수 ( + 어떻게 재생할지 패러미터 추가)
    public void PlaySound(string clipName, Vector3 pos, Transform parent = null, float pitch = 1f)
    {
        //clips에 있는 모든 AudioClip을 순환하며, 전달받은 clipName과 같은 이름의 클립을 찾아줌
        foreach(AudioClip ac in clips)
        {
            if(ac.name == clipName)
            {
                //찾은 소리를 실제로 재생해달라고 패러미터 넘겨줌
                PlaySound(ac, pos, parent, pitch);
            }
        }
    }

    //여러 사운드 중 랜덤하게 골라 재생하는 PlaySound 오버로딩
    public void PlaySound(string[] clipNames, Vector3 pos, Transform parent = null, float pitch = 1f)
    {
        int randomIndex = Random.Range(0, clipNames.Length);        //랜덤한 인덱스 값 저장

        PlaySound(clipNames[randomIndex], pos, parent, pitch);      //선택된 오디오클립 플레이
    }

    //실제 오디오클립 재생하는 함수
    private void PlaySound(AudioClip audioClip, Vector3 pos, Transform parent = null, float pitch = 1f)
    {
        //오디오 재생용 프리팝 생성
        AudioSource audioInstance = Instantiate(audioPrefab, pos, Quaternion.identity);

        //소리를 내는 사물을 따라가야 하는 경우, 그 사물의 자식으로 넣어줌
        if (parent != null)
            audioInstance.transform.SetParent(parent);

        audioInstance.clip = audioClip;     //클립을 바꿔주고
        audioInstance.pitch = pitch;        //피치값 설정
        audioInstance.Play();               //재생

        Destroy(audioInstance.gameObject, audioClip.length);       //오디오클립을 재생 후 인스턴스 삭제
    }


}
