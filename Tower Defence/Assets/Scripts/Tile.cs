using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //타일에 타워가 건설되어 있는지 검사하는 변수
    public bool IsBuildTower { set; get; }
    /*자동 구현 프로퍼티
     프로퍼티의 set, get에 추가적인 내용이 없는 기본형일 때 코드 길이를 줄이기 위해 사용
    변수를 따로 선언할 필요 없으며, set, get의 기본형이 자동으로 작성됨*/

    private void Awake()
    {
        IsBuildTower = false;
    }
}
