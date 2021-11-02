using UnityEngine;
using System.Collections;
using DG.Tweening;          //DOTween 을 쓰기 위해 필요

public class Basics : MonoBehaviour
{
	public Transform redCube, greenCube, blueCube, purpleCube;

	IEnumerator Start()
	{
		// Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
		yield return new WaitForSeconds(1);

        // 0, 4, 0 으로 2초 동안 트윈
        redCube.DOMove(new Vector3(0, 4, 0), 2);

		// 0, 4, 0 에서 2초 동안 현재 위치로 트윈
		greenCube.DOMove(new Vector3(0,4,0), 2).From();
		
		// 0, 4, 0 만큼 2초 동안 트윈
		blueCube.DOMove(new Vector3(0,4,0), 2).SetRelative();

		// 보라 큐브를 2초 동안 6, 0, 0 만큼 이동하고
		// 색을 노란색으로 바꿔준다
		// 컬러를 바꾸려면, 매터리얼을 타겟으로 줘야 한다. (Transform 이 아니라)
		purpleCube.DOMove(new Vector3(2,0,0), 1).SetRelative().SetLoops(-1, LoopType.Incremental);
		// 그 트윈이 계속 루프되도록 해준다.
		purpleCube.GetComponent<Renderer>().material.DOColor(Color.yellow, 2).SetLoops(-1, LoopType.Restart);
	}
}