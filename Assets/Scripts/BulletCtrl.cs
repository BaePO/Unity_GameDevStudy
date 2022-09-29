using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    // 총알의 파괴력
    public float damage = 20.0f;
    // 총알 발사 힘
    public float force = 150.0f;

    private Rigidbody rb;

    void Start()
    {
        // Rigidbody 컴포넌트 추출
        rb = this.gameObject.GetComponent<Rigidbody>();

        // 총알의 전진 방향으로 힘(Force)을 가한다.
        rb.AddForce(transform.forward * force);
        // rb.AddRelativeForce(Vector3.forward * force); 와 동일하다. AddForce -> Global Coordinate / AddRelativeForce -> Object의 Local Coordinate
    }

    void Update()
    {
        
    }
}
