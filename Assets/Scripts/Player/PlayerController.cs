using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 _direction;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("moveX",horizontal);
        animator.SetFloat("moveY", vertical);
        animator.SetBool("isMoving", _direction.magnitude > 0);

        _direction = new Vector2 (horizontal, vertical);

        _direction = _direction * speed;
    }

    void FixedUpdate()
    {
        rb.velocity = _direction; //to make movement less choppy
    }
}
