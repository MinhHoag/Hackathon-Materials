using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _jumpForce = 10f;
    private float _dashPower = 20f;
    private float _dashDuration = 0.2f;
    private float _dashCooldown = 1f;

    public Rigidbody2D rb;
    public Animator anim;
    public bool isJumping;
    public GameObject target;

    private bool _canDash = true;
    private bool _isDashing = false;
    private bool _isRock = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);

        // Flipping Character
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Abs(target.transform.localScale.x), transform.localScale.y, -10);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(target.transform.localScale.x), transform.localScale.y, -10);
        }

        // Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }

        // Dashing Mechanic
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }
       

        transform.Translate(direction * (_isDashing ? _dashPower : _speed) * Time.deltaTime);

        // Condition for Animation
        anim.SetBool("Walk", horizontalInput != 0);
        anim.SetBool("isJumping", isJumping);
    }

    // Single Jump Mechanic
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;
        StartCoroutine(TurnIntoRock());
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
    private IEnumerator TurnIntoRock()
    {
        _isRock = true;
        _speed = 0f;
        _jumpForce = 0f;

        while (_isRock && Input.GetKey(KeyCode.LeftShift))
        {
            yield return null;
        }

        _speed = 5.0f;
        _jumpForce = 10f;
        _isRock = false;
    }
}

