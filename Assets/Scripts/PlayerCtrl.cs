using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수
    private Animation anim;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;

    IEnumerator Start()
    {
        // 컴포넌트를 추출하여 변수에 대입
        tr = this.gameObject.GetComponent<Transform>(); // Generic : 데이터 타입(형)을 미리 정해놓는 것 <Transform>으로 Parameter()를 받는다.
        // 마찬가지로 this.gameObject는 생략가능하지만 TypeScript 개발에서는 불가능하므로 사용하는 습관을 기르자.
        anim = this.gameObject.GetComponent<Animation>();

        // 인스펙터 창에서 Play Automatically를 해제하고 스크립트에서 실행
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X"); // Mouse X는 마우스를 왼쪽으로 움직이면 음수 값, 오른쪽으로 움직이면 양수 값
        
        // Debug.Log("h = " + h); // h = -1.0f ~ 1.0f
        // Debug.Log("v = " + v); // v = -1.0f ~ 1.0f

        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // forward -> z axis
        // right -> x axis
        // 두 Vector3를 더하여 x, z Axis에서의 입력값(전진/후진 변수)를 설정해주고 Translate에서 방향 * 전진/후진 변수로 사용한다.
        
        // Translate(이동 방향 * 속력 * Time.deltaTime)
        this.tr.Translate(moveDir.normalized * this.moveSpeed * Time.deltaTime, Space.Self); // Space.self : 로컬 좌표로 설정 <-> Space.world : 글로벌 좌표로 설정
        // this는 생략가능하지만 가능하면 쓰는 습관을 기르자. (TypeScript에서는 필수적으로 사용하므로 - 제페토)
        // Vector3.forward, back, right, left... => 방향 벡터 (단위 벡터)이므로 크기는 항상 1이다.
        // this.tr.Translate((방향 + 전진/후진 변수) * 속도 * Time.deltaTime);

        // Vector3.up 축을 기준으로 turnSpeed 만큼의 속도로 회전
        this.tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
        //      Rotate(회전좌표축 * 회전속도 * Time.deltaTime * 변위입력 값);

        // 주인공 캐릭터의 애니메이션 설정
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        // 키보드 입력값을 기준을 동작할 애니메이션 수행

        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); // 전진 애니메이션 실행
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);
        }
    }
}
