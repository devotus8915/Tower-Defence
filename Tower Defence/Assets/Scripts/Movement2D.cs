using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed; //moveSpeed 변수의 프로퍼티(property) (Get 가능)
    /*프로퍼티는 변숫값을 읽거나 쓰는 과정에서 유연한 처리를 삽입할 수 있는 클래스 멤버
     변수처럼 보이지만 변수가 아닌 특수한 형태의 메서드*/

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
