using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    public float moveSpeed = 10.0f;

    void Start()
    {
        tr = this.gameObject.GetComponent<Transform>(); // Generic : ������ Ÿ��(��)�� �̸� ���س��� �� <Transform>���� Parameter()�� �޴´�.
        // ���������� this.gameObject�� �������������� TypeScript ���߿����� �Ұ����ϹǷ� ����ϴ� ������ �⸣��.
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
        // �� Vector3�� ���Ͽ� x, z Axis������ �Է°�(����/���� ����)�� �������ְ� Translate���� ���� * ����/���� ������ ����Ѵ�.

        this.tr.Translate(moveDir.normalized * this.moveSpeed * Time.deltaTime, Space.Self); // Space.self : ���� ��ǥ�� ���� <-> Space.world : �۷ι� ��ǥ�� ����
        // this�� �������������� �����ϸ� ���� ������ �⸣��. (TypeScript������ �ʼ������� ����ϹǷ� - ������)
        // Vector3.forward, back, right, left... => ���� ���� (���� ����)�̹Ƿ� ũ��� �׻� 1�̴�.
        // this.tr.Translate((���� + ����/���� ����) * �ӵ� * Time.deltaTime);
    }
}
