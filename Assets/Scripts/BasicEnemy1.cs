using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicEnemy1 : MonoBehaviour
{

    public float EnemySpeedMAX = .6f;
    public float EnemySpeedMIN = .2f;
    public float EnemyCurrentSpeed = .31f;
    public float speedIncrement = 0.01f;
    private bool facingRight = false;
    Vector2 lastPos;
    int movePath = 0 ;
    int moveCounter = 1;
    System.Random rand = new System.Random();
    int randChoice;
    bool isBlowingUpBalloon = true;
    bool isChasing = false;
    public int enemyJumpPower = 20;
    

    //The actual player
    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //SET THE PLAYER'S TAG TO "Player" IN THE INSPECTOR FOR THIS TO WORK.
        lastPos = gameObject.GetComponent<Rigidbody2D>().position;
        gameObject.SetActive(false);
    }
    void Update()
    {
        //SPEED
        //increase enemy speed if it's farther away from the enemy.
        if(Vector2.Distance(transform.position, target.position) > .33)
        {
            if (EnemyCurrentSpeed < EnemySpeedMAX)
                EnemyCurrentSpeed += speedIncrement;
        }
        else
        {
            if (EnemyCurrentSpeed > EnemySpeedMIN)
                EnemyCurrentSpeed -= speedIncrement;
        }

        

        //ENEMY FACING DIRECTION
        SwitchedDir();
        lastPos = gameObject.GetComponent<Rigidbody2D>().position; //for direction purposes

        //ANIMATION
        

        //PHYSICS

        if (movePath != 0 && !isBlowingUpBalloon)
        {

            //Patrolling
            if (Vector2.Distance(transform.position, target.position) > .7 && !isChasing)
            {

                if (movePath == 1) // idle
                {

                }
                else if (movePath == 2) //move right
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * EnemyCurrentSpeed, 0);
                }
                else if (movePath == 3) //move left
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * EnemyCurrentSpeed, 0);
                }

                //prevent falling off the map
                if(gameObject.GetComponent<Rigidbody2D>().position.y < -.78)
                {
                    //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 1);
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * enemyJumpPower);
                }
            }
            else
            {
                isChasing = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (Vector2.Distance(transform.position, target.position) > .7) //move towards character
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, EnemyCurrentSpeed * Time.deltaTime);

                }
                else if (Vector2.Distance(transform.position, target.position) < .7) //move towards the balloons
                {
                    Vector2 targetDest = new Vector2(target.position.x, target.position.y);
                    targetDest.y += .2f;
                    transform.position = Vector2.MoveTowards(transform.position, targetDest, EnemyCurrentSpeed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, target.position) < .3) //if player dodges enemy
                {

                }
            }
        }


        updateMovePath();
        

    }

    void updateMovePath()
    {
        //UPDATE MOVEPATH
        if (moveCounter % 100 == 0)
        {

            isBlowingUpBalloon = false;

            randChoice = rand.Next(101); //Chooses a # between 0 and 100.
            //Debug.Log(randChoice);
            if (randChoice > 90) //10% chance
            {
                movePath = 4;
            }
            else if (randChoice > 60) //30% chance
            {
                movePath = 3;
            }
            else if (randChoice > 30) //30%
            {
                movePath = 2;
            }
            else //31% (0-30)
            {
                movePath = 1;
            }
            //Debug.Log(movePath);
        }

        moveCounter++;
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
