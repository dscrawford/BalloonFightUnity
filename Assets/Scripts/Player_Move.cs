using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Animator anim;

    public int playerSpeed = 10;
    public int playerJumpPower = 20;
    public float maxYVelocity = 5;

    private float moveX, moveY;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    public float getMoveX()
    {
        return moveX;
    }

    void OnCollisionEnter()
    {
        //anim.SetBool("Ground", true);
    }

    void OnCollisionExit()
    {
        //anim.SetBool("Ground", false);
    }


    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = GetComponent<Rigidbody2D>().velocity.y;
        //Controls
        if (Input.GetButtonDown("Jump")) { 
            Jump();
        }
        //Animation
        if(Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
        }
        else
            anim.SetBool("Jump", false);
        if (moveY != 0.0f) //NOT on ground Needs correction due to breif frame shifting from uplift to falling
        {
            anim.SetBool("Ground", false);
        }
        else
            anim.SetBool("Ground", true);
        if (moveX != 0.0f)
        {
            anim.SetBool("Move", true);
        }
        else
            anim.SetBool("Move", false);

        //Player Direction
        if (moveX < 0.0f && facingRight)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && !facingRight)
        {
            FlipPlayer();
        }

        //Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.y,-maxYVelocity, maxYVelocity));
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }

    float GetYMovementSpeed()
    {
        if (moveY > maxYVelocity)
        {
            return maxYVelocity;
        }
        return moveY;
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void ChangePosition(Vector3 pos)
    {
        this.transform.position = pos;
    }

    public void ChangeVelocity(Vector3 pos)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = pos;
    }
}
