using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject prefab;
    public Transform shootPoint;

    public float speed = 10;
    public float damage = 50;
    public float death_rate_fireball = 5;
    public Rigidbody Fireball;
    void Start()
    {
        prefab = Resources.Load("Fireball") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            GameObject projectile = Instantiate(prefab) as GameObject;
            //var pos = transform.position + Camera.main.transform.forward * 2;
            //pos.y += 0.5f;
            //pos.x += 1.0f;
            projectile.transform.position = shootPoint.transform.position;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * speed;
            Destroy(projectile, death_rate_fireball);
        }
    }
}
