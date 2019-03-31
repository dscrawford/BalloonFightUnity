using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy1 : MonoBehaviour
{

    public float EnemySpeedMAX = 5;
    public float EnemySpeedMIN = 0;
    public float EnemyCurrentSpeed = 0;
    public float speedIncrement = 0.1f;
    public float XMoveDirection;
    public float YMoveDirection;
    public GameObject Player;
    private Vector2 PlayerPos;
    private bool facingRight = true;
    public int enemyJumpPower = 550;
    private bool jump = false;
    uint jumpFactor = 0;
    Vector2 lastPos;

    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastPos = gameObject.GetComponent<Rigidbody2D>().position;
    }
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > 3)
        {
            if (EnemyCurrentSpeed < EnemySpeedMAX)
                EnemyCurrentSpeed += speedIncrement;
        }
        else
        {
            if (EnemyCurrentSpeed < EnemySpeedMIN)
                EnemyCurrentSpeed -= speedIncrement;
        }

        //ENEMY DIRECTION
        SwitchedDir();

        lastPos = gameObject.GetComponent<Rigidbody2D>().position; //for direction purposes

        if (Vector2.Distance(transform.position, target.position) > 3.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, EnemyCurrentSpeed * Time.deltaTime);
            
        }
        else if(Vector2.Distance(transform.position, target.position) < 3.5)
        {
            Vector2 targetDest = new Vector2(target.position.x, target.position.y);
            targetDest.y += 1.5f;
            transform.position = Vector2.MoveTowards(transform.position, targetDest, EnemyCurrentSpeed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, target.position) < 2)
        {

        }


    }

    void SwitchedDir()
    {
        if (lastPos.x < gameObject.GetComponent<Rigidbody2D>().position.x && !facingRight)
            FlipPlayer();
        else if (lastPos.x > gameObject.GetComponent<Rigidbody2D>().position.x && facingRight)
            FlipPlayer();

        
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Update is called once per frame
    /*void Update()
    {
        //ENEMY MOVEMENT
        jumpFactor++;
        CalculateMovement(GetPlayerLocation());
        if (jump && jumpFactor > 20)
        {
            Jump();
            jumpFactor = 0;
        }
        //ANIME


        //ENEMY DIRECTION
        if (XMoveDirection < 0.0f && !facingRight)
        {
            FlipPlayer();
        }
        else if (XMoveDirection > 0.0f && facingRight)
        {
            FlipPlayer();
        }
        //ENEMY PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
    }

    Vector2 GetPlayerLocation()
    {
        return Player.GetComponent<Rigidbody2D>().position;
    }

    void CalculateMovement(Vector2 playerPos)
    {
        float posx = gameObject.GetComponent<Rigidbody2D>().position.x;
        float posy = gameObject.GetComponent<Rigidbody2D>().position.y;
        Vector2 newDir = new Vector2(0,0);

        //if(System.Math.Abs(playerPos.y - posy) > 4 && System.Math.Abs(playerPos.x - posx) > 3)
        //{

        //}

        //X direction
        if (playerPos.x < posx)
            newDir.x = -1;
        else if (playerPos.x > posx)
            newDir.x = 1;

        if (posy - playerPos.y < 2)
            jump = true;
        else if (playerPos.y > posy) //if player is above enemy..
            if (System.Math.Abs(playerPos.x - posx) < 3) //Do not jump if the player is nearby
                jump = false;
            else
                jump = true;

        this.XMoveDirection = newDir.x;
        

    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * enemyJumpPower);
    }

    void CheckCollisions()
    {

    }*/
}
