using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Count : MonoBehaviour
{
    public Text text1;
    public GameObject dragon;
    public Animator changeCa;
    public static int dragonCount=0;

    void Update()
    {        
        text1.text=": "+EnemySpawner.enemyCount;
        if (Count.dragonCount == 10)
        {
            dragon.SetActive(true);
            changeCa.SetTrigger("Change");
        }
    }
}
