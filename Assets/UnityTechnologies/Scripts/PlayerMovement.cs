using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator; // 변수
    private Rigidbody rb;
    private Vector3 movement = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;

    [HeaderAttribute("회전 속도")]
    public float turnSpeed = 20f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // movement.Set : 벡터의 값을 저장
        movement.Set(horizontal, 0f, vertical);
        // movement.Normalize() : 벡터 정규화 (길이를 1로 만듦)
        movement.Normalize();
        // 입력 체크
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        // 애니메이터의 파라미터를 입력
        animator.SetBool("IsWalking", isWalking);
        // desiredFoward : 방향 바라보는 벡터
        // Time.fixedDeltaTime : FixedUpdate 한 프레임당 걸리는 시간
        Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, movement, 
            turnSpeed * Time.fixedDeltaTime, 0f);
        // Quaternion : 방향벡터를 직접 쳐다보게 돌려준다
        rotation = Quaternion.LookRotation(desiredFoward);
    }

    private void OnAnimatorMove()
    {
        // rb.MovePosition : 리짓바디를 통한 위치 이동
        // animator.deltaPosition.magnitude : 애니메이션당 한 발자국
        rb.MovePosition(rb.position + 
            movement * animator.deltaPosition.magnitude);
        // rb.MoveRotation : 리짓바디를 통한 회전
        rb.MoveRotation(rotation);
    }
}
