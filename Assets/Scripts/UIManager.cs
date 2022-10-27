using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           // Unity-UI�� ����ϱ� ���� ������ ���ӽ����̽�
using UnityEngine.Events;       // UnityEvent ���� API�� ����ϱ� ���� ������ ���ӽ����̽�

public class UIManager : MonoBehaviour
{
    // ��ư�� ������ ����
    public Button startBtn;
    public Button optionBtn;
    public Button shopBtn;

    private UnityAction action;

    private void Start()
    {
        // UnityAction�� ����� �̺�Ʈ ���� ���
        action = () => OnButtonClick(startBtn.name);
        startBtn.onClick.AddListener(action);

        // ���� �޼��带 Ȱ���� �̺�Ʈ ���� ���
        optionBtn.onClick.AddListener(delegate { OnButtonClick(optionBtn.name); });

        // ���ٽ��� Ȱ���� �̺�Ʈ ���� ���
        shopBtn.onClick.AddListener(() => OnButtonClick(shopBtn.name));
    }
    public void OnButtonClick(string msg)
    {
        Debug.Log($"Click Button : {msg}");
    }
}
