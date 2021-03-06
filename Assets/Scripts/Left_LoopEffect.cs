﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_LoopEffect : MonoBehaviour
{
    float rightXPos;
    // Start is called before the first frame update
    void Start()
    {
        rightXPos = GameObject.Find("RightWall").GetComponent<BoxCollider2D>().bounds.min.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player_Move>().getMoveX() < 0) {
                collision.gameObject.GetComponent<Player_Move>().ChangePosition(new Vector3(rightXPos, collision.transform.position.y, collision.transform.position.z));
            }
        }

        if (collision.tag == "MoveableEntity")
        {
            if (!collision.gameObject.GetComponent<BasicEnemy1>().getFacingRight())
            {
                collision.gameObject.GetComponent<BasicEnemy1>().ChangePosition(new Vector3(rightXPos, collision.transform.position.y, collision.transform.position.z));
            }
        }
        //Debug.Log("Ive been hit!");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("He left :(");
    }
}
