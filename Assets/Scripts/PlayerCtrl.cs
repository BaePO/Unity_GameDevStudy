using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    public float moveSpeed = 10.0f;

    void Start()
    {
        tr = this.gameObject.GetComponent<Transform>(); // Generic : 데이터 타입(형)을 미리 정해놓는 것 <Transform>으로 Parameter()를 받는다.
        // 마찬가지로 this.gameObject는 생략가능하지만 TypeScript 개발에서는 불가능하므로 사용하는 습관을 기르자.
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");
        
        Debug.Log("h = " + h); // h = -1.0f ~ 1.0f
        Debug.Log("v = " + v); // v = -1.0f ~ 1.0f

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // forward -> z axis
        // right -> x axis
        // 두 Vector3를 더하여 x, z Axis에서의 입력값(전진/후진 변수)를 설정해주고 Translate에서 방향 * 전진/후진 변수로 사용한다.

        this.tr.Translate(moveDir.normalized * this.moveSpeed * Time.deltaTime, Space.Self); // Space.self : 로컬 좌표로 설정 <-> Space.world : 글로벌 좌표로 설정
        // this는 생략가능하지만 가능하면 쓰는 습관을 기르자. (TypeScript에서는 필수적으로 사용하므로 - 제페토)
        // Vector3.forward, back, right, left... => 방향 벡터 (단위 벡터)이므로 크기는 항상 1이다.
        // this.tr.Translate((방향 + 전진/후진 변수) * 속도 * Time.deltaTime);
    }
}
