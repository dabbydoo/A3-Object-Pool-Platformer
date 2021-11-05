using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
    public float walkSpeed;
    public bool isPatrol;
    private bool isFlip;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public Transform killCheckPos;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        isPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(killCheckPos.position, 0.1f, playerLayer))
        {
            SceneManager.LoadScene("Level1");
        }
        if (isPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (isPatrol)
        {
            isFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.07f, groundLayer);
        }
    }

    void Patrol()
    {
        if (isFlip)
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
    }
}