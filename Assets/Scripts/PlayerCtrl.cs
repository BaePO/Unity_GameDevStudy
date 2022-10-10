using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // ������Ʈ�� ĳ�� ó���� ����
    private Transform tr;
    // Animation ������Ʈ�� ������ ����
    private Animation anim;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;

    IEnumerator Start()
    {
        // ������Ʈ�� �����Ͽ� ������ ����
        tr = this.gameObject.GetComponent<Transform>(); // Generic : ������ Ÿ��(��)�� �̸� ���س��� �� <Transform>���� Parameter()�� �޴´�.
        // ���������� this.gameObject�� �������������� TypeScript ���߿����� �Ұ����ϹǷ� ����ϴ� ������ �⸣��.
        anim = this.gameObject.GetComponent<Animation>();

        // �ν����� â���� Play Automatically�� �����ϰ� ��ũ��Ʈ���� ����
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X"); // Mouse X�� ���콺�� �������� �����̸� ���� ��, ���������� �����̸� ��� ��
        
        // Debug.Log("h = " + h); // h = -1.0f ~ 1.0f
        // Debug.Log("v = " + v); // v = -1.0f ~ 1.0f

        // �����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // forward -> z axis
        // right -> x axis
        // �� Vector3�� ���Ͽ� x, z Axis������ �Է°�(����/���� ����)�� �������ְ� Translate���� ���� * ����/���� ������ ����Ѵ�.
        
        // Translate(�̵� ���� * �ӷ� * Time.deltaTime)
        this.tr.Translate(moveDir.normalized * this.moveSpeed * Time.deltaTime, Space.Self); // Space.self : ���� ��ǥ�� ���� <-> Space.world : �۷ι� ��ǥ�� ����
        // this�� �������������� �����ϸ� ���� ������ �⸣��. (TypeScript������ �ʼ������� ����ϹǷ� - ������)
        // Vector3.forward, back, right, left... => ���� ���� (���� ����)�̹Ƿ� ũ��� �׻� 1�̴�.
        // this.tr.Translate((���� + ����/���� ����) * �ӵ� * Time.deltaTime);

        // Vector3.up ���� �������� turnSpeed ��ŭ�� �ӵ��� ȸ��
        this.tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
        //      Rotate(ȸ����ǥ�� * ȸ���ӵ� * Time.deltaTime * �����Է� ��);

        // ���ΰ� ĳ������ �ִϸ��̼� ����
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        // Ű���� �Է°��� ������ ������ �ִϸ��̼� ����

        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); // ���� �ִϸ��̼� ����
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
