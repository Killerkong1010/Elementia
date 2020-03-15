using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionController : MonoBehaviour
{
    public PlayerHealth healthbar;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            healthbar.onTakeDamage(10);

            
        }         
        if (collision.gameObject.tag == "Turtle")
        {
            healthbar.onTakeDamage(30);
        }

        if (collision.gameObject.tag == "Golem")
        {
            healthbar.onTakeDamage(50);
        }
    }
}
