using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    // �Ѿ��� �ı���
    public float damage = 20.0f;
    // �Ѿ� �߻� ��
    public float force = 150.0f;

    private Rigidbody rb;

    void Start()
    {
        // Rigidbody ������Ʈ ����
        rb = this.gameObject.GetComponent<Rigidbody>();

        // �Ѿ��� ���� �������� ��(Force)�� ���Ѵ�.
        rb.AddForce(transform.forward * force);
        // rb.AddRelativeForce(Vector3.forward * force); �� �����ϴ�. AddForce -> Global Coordinate / AddRelativeForce -> Object�� Local Coordinate
    }

    void Update()
    {
        
    }
}
