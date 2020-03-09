using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject prefab;

    public float speed = 40;
    public float damage = 15;
    public float death_rate = 5;
    public Rigidbody projectile;
    void Start()
    {
        prefab = Resources.Load("Projectile") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject projectile = Instantiate(prefab) as GameObject;
            var pos = transform.position + Camera.main.transform.forward * 2;
            pos.y += 0.5f;
            pos.x += 1.0f;
            projectile.transform.position = pos;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * speed;
            Destroy(projectile, death_rate);
        }
    }
    
}
