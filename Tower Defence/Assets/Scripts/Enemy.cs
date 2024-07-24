using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount; // 이동 경로 개수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private Movement2D movement2D; // 오브젝트 이동 제어

    public void Setup(Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();

        //적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적의 위치를 첫번째 wayPoints 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        //적 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }
    
    private IEnumerator OnMove()
    {
        NextMoveTo();

        while(true)
        {
            //적 오브젝트 회전
            transform.Rotate(Vector3.forward * 10);

            /*적의 현재 위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed보다 적을 때 if 조건문 실행
             movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문에
            if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다.*/
        }
    }

    private void NextMoveTo()
    {

    }
}
