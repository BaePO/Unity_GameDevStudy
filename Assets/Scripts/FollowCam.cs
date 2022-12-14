using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라가야 할 대상을 연결할 변수
    public Transform targetTr;
    // Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;

    // 따라갈 대상으로부터 떨어질 거리
    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    // Y축으로 이동할 높이
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    // 반응 속도
    public float damping = 10.0f;

    // 카메라 LookAt의 Offset 값 : 오프셋 변수와 LookAt 함수 조정
    public float targetOffset = 2.0f;

    // Lerp를 잘 사용하기 위한 방법
    public Transform startPosition;
    public Transform endPosition;

    // SmoothDamp에서 사용할 변수 -> SmoothDamp는 주로 Follw Cam에 사용된다.
    private Vector3 veloctiy = Vector3.zero;

    float lerpTime = 0.5f;
    float currentTime = 0;

    void Start()
    {
        // Main Camera 자신의 Transform 컴포넌트를 추출
        camTr = this.gameObject.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // 추적해야 할 대상의 뒤쪽으로 distance만큼 이동
        // 높이를 height만큼 이동
        Vector3 pos = targetTr.position
                      + (-targetTr.forward * distance)
                      + (Vector3.up * height);

        // ※ 보간법 사용 시 주의점
        // Lerp, Slerp 등 보간 함수에 인자를 넣을 때, 시작 좌표가 매번 달라지고 보간값이 고정이라면 선형적으로 보간되지 않는다. (처음에 빠르게 올라오다가 나중에 느려진다.)
        // ① Start와 End를 좌표를 유니티에서 Object로 만들어 사용한다.
        // ② 보간값을 시간에 따라서 변하도록 설정한다.
        currentTime += Time.deltaTime;

        if (currentTime >= lerpTime)
        {
            currentTime = lerpTime;
        }

        // camTr.position = Vector3.Lerp(startPosition.position, endPosition.position, currentTime / lerpTime);
        // 이렇게 하면 우리가 원하는 선형적으로 보간하여 변하도록 설정할 수 있다.

        // 구면 선형 보간 함수(Slerp)를 사용해 부드럽게 위치를 변경
        // camTr.position = Vector3.Slerp(camTr.position,                  // 시작 위치
        //                                pos,                             // 목표 위치
        //                                Time.deltaTime * damping);       // 시간 t

        // SmoothDamp을 이용한 위치 보간
        camTr.position = Vector3.SmoothDamp(camTr.position,                 // 시작 위치
                                         pos,                            // 목표 위치
                                         ref veloctiy,                   // 현재 속도
                                         damping);                       // 목표 위치까지 도달할 시간

        // Camera를 피벗 좌표를 향해 회전
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
