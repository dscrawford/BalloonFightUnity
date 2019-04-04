using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_LoopEffect : MonoBehaviour
{
    float leftXPos;
    // Start is called before the first frame update
    void Start()
    {
        leftXPos = GameObject.Find("LeftWall").GetComponent<BoxCollider2D>().bounds.max.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MoveableEntity")
        {
            if (collision.gameObject.GetComponent<Player_Move>().getMoveX() > 0)
            {
                Debug.Log("Changing pos");
                collision.gameObject.GetComponent<Player_Move>().ChangePosition(new Vector3(leftXPos, collision.transform.position.y, collision.transform.position.z));
            }
        }
        //Debug.Log("Ive been hit!");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("He left :(");
    }
}
