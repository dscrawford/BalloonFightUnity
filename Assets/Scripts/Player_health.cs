using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_health : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -6)
        {
            Debug.Log("Player deaded");
            Die();
        }
        

    }

    void Die ()
    {
        SceneManager.LoadScene("Prototype_1");
    }
}
