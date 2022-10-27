using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           // Unity-UI를 사용하기 위해 선언한 네임스페이스
using UnityEngine.Events;       // UnityEvent 관련 API를 사용하기 위해 선언한 네임스페이스

public class UIManager : MonoBehaviour
{
    // 버튼을 연결할 변수
    public Button startBtn;
    public Button optionBtn;
    public Button shopBtn;

    private UnityAction action;

    private void Start()
    {
        // UnityAction을 사용한 이벤트 연결 방식
        action = () => OnButtonClick(startBtn.name);
        startBtn.onClick.AddListener(action);

        // 무명 메서드를 활용한 이벤트 연결 방식
        optionBtn.onClick.AddListener(delegate { OnButtonClick(optionBtn.name); });

        // 람다식을 활용한 이벤트 연결 방식
        shopBtn.onClick.AddListener(() => OnButtonClick(shopBtn.name));
    }
    public void OnButtonClick(string msg)
    {
        Debug.Log($"Click Button : {msg}");
    }
}
