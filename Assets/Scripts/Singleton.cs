using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//제너릭 싱글톤 클래스를 만듬 - 이 클래스를 상속하는 타입만 사용 가능
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance;

    //시작 시 싱글톤 인스턴스 초기화
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = (T)this;
        else
            Destroy(this.gameObject);
    }

    //싱글톤 인스턴스가 사라졌을 때
    protected virtual void OnDestroy()
    {
        //유일한 Instance가 자신이면, Instance값을 비워줌 (다른 인스턴스가 들어갈 수 있도록)
        if (Instance == this)
            Instance = null;
    }
}