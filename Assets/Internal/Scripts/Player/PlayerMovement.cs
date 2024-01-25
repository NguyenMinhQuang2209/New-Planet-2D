using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform character;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runSpeed = 2f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Animator>(out animator))
            {
                character = child;
                break;
            }
        }
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Movement(new Vector2(horizontal, vertical).normalized);
    }
    private void Movement(Vector2 input)
    {
        float speed = 0f;
        if (input.sqrMagnitude >= 0.1f)
        {
            speed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            character.localRotation = Quaternion.Euler(0f, input.x < 0 ? 0f : 180f, 0f);
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * input);
            int dir = input.y < 0 ? 0 : input.y > 0 ? 1 : 2;
            animator.SetFloat("Dir", dir);
        }
        float inputX = speed == moveSpeed ? 0.5f * input.x : input.x;
        float inputY = speed == moveSpeed ? 0.5f * input.y : input.y;
        animator.SetFloat("Horizontal", inputX);
        animator.SetFloat("Vertical", inputY);
        animator.SetFloat("Speed", speed);
    }
}
