using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수
    private Animation anim;

    // 이동 속력 변수 (public으로 선언돼 인스펙터 뷰에 노출됨)
    public float moveSpeed = 10.0f;
    // 회전 속도 변수
    public float turnSpeed = 80.0f;

    // 초기 생명 값
    private readonly float initHp = 100.0f;
    // 현재 생명 값
    public float currHp;
    // Hpbar를 연결할 변수
    private Image hpBar;

    // 델리게이트 선언
    public delegate void PlayerDieHandler();
    // 이벤트 선언
    public static event PlayerDieHandler OnPlayerDie;

    // Start는 코루틴으로 실행 가능하다 -> 0.3f 대기하여 turnSpeed를 적용함으로써 처음 시작할 때 넘어온 마우스의 불규칙한 값(노이즈, 쓰레깃값)을 적용하지 않고 안정적인 값이 넘어올 때까지 대기했다가 로직 실행
    IEnumerator Start()
    {
        // Hpbar 연결
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        // HP 초기화
        currHp = initHp;
        DisplayHealth();
         
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

    // 충돌한 Collider의 IsTrigger 옵션이 체크됐을 때 발생
    private void OnTriggerEnter(Collider coll)
    {
        // 충돌한 Collider가 몬스터의 PUNCH이면 Player의 HP 차감
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            DisplayHealth();

            Debug.Log($"Player hp = {currHp / initHp}");

            // Player의 생명이 0 이하이면 사망 처리
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    // Player 사망 처리
    void PlayerDie()
    {
        Debug.Log("Player Die !");

        /*
        // MONSTER 태그를 가진 모든 게임오브젝트를 찾아옴
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        // 모든 몬스터의 OnPlayerDie 함수를 순차적으로 호출
        foreach(GameObject monster in monsters)
        {
            // monster 오브젝트에 있는 OnPlayerDie라는 함수를 호출, 없더라도 없다는 메시지를 반환받지 않겠음.
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
        
        => foreach로 순회하면서 SendMessage를 하면 메모리 사용 측면이나 구동 속도 측면에서 ↓
        */

        // 주인공 사망 이벤트 호출(발생)
        OnPlayerDie();
    }

    void DisplayHealth()
    {
        hpBar.fillAmount = currHp / initHp;
    }
}
