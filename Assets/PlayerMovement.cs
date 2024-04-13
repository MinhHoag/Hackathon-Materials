using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5.0f;
    float jumpforce = 10f;
    public Rigidbody2D rb;
    //public Animation anim;
    public bool isJumping;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        
        // Flipping Character

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(target.transform.localScale.x, transform.localScale.y, -10);
        } else if (horizontalInput<-0.01f) { 
            transform.localScale = new Vector3(-target.transform.localScale.x, transform.localScale.y, -10);
        } 
        

        // Jumping Mechanic

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

        transform.Translate(direction * _speed * Time.deltaTime);

        // Condition for Animation
        //anim.SetBool("Walk",horizontalInput!=0);
        //anim.SetBool("isJumping", isJumping);
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
}
