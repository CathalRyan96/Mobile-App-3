using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpPower;
    private float moveInput;

    public Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask grounded;
    private int doubleJump;
    public int doubleJumpValue;

    

    void Start()
    {
        doubleJump = doubleJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        if(isGrounded == true)
        {
            doubleJump = doubleJumpValue;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, grounded);
        //player movement
        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //jumping
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetKeyDown(KeyCode.Space) && doubleJump > 0)
        {
            rb.velocity = Vector2.up * jumpPower;
            doubleJump--;
        }else if(Input.GetKeyDown(KeyCode.Space) && doubleJump == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpPower;


        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "spike")
        {
            Scene scene;
            scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
