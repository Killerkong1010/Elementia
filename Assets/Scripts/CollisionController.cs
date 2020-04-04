using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionController : MonoBehaviour
{
    public PlayerHealth healthbar;
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Slime":
                healthbar.onTakeDamage(10);
                break;
            case "Turtle":
                healthbar.onTakeDamage(30);
                break;
            case "Golem":
                healthbar.onTakeDamage(50);
                break;
        }
        
    }
}
