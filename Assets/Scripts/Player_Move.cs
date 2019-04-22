using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Move : MonoBehaviour
{
    public Animator anim;
    public int playerSpeed = 10;
    public int playerJumpPower = 20;
    public float maxYVelocity = 5;
    private int playerHP = 2;

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
        checkLife();
        PlayerMove();
    }

    public float getMoveX()
    {
        return moveX;
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

    public bool isOnEnemy()
    {
        float modelWidth = GetComponent<BoxCollider2D>().bounds.max.x - GetComponent<BoxCollider2D>().bounds.min.x;
        Debug.DrawRay(new Vector3(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - 0.03f, 0), Vector3.right * modelWidth);

        RaycastHit2D right = Physics2D.Raycast(new Vector2(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - 0.03f), Vector2.right, modelWidth);
        RaycastHit2D left = Physics2D.Raycast(new Vector2(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - 0.03f), Vector2.left, modelWidth);

        //Check if the right collider hit something
        if (right.collider != null)
            if (right.collider.tag == "Enemy")
                return true;

        //Check if the left collider hit something
        else if (left.collider != null)
            return left.collider.tag == "Enemy";

        //If both null, then no collision.
        return false;
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

    public void DecrementPlayerHealth()
    {
        playerHP--;
    }

    private void checkLife()
    {
        if (playerHP <= 0 || transform.position.y < -1.2f)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("Main_menu");
    }
}
