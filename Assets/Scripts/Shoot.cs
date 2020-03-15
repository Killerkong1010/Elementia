using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject prefab;
    public Transform shootPoint;

    public float speed = 10;
    public float damage = 50;
    public float death_rate_fireball = 5;
    public Rigidbody Fireball;
    public float cooldownTime_fb = 5;
    private float nextFireTime;
    void Start()
    {
        prefab = Resources.Load("Fireball") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        cooldownDisplay();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.time > nextFireTime)
            {
                GameObject projectile = Instantiate(prefab) as GameObject;
                //var pos = transform.position + Camera.main.transform.forward * 2;
                //pos.y += 0.5f;
                //pos.x += 1.0f;
                projectile.transform.position = shootPoint.transform.position;
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * speed;
                Destroy(projectile, death_rate_fireball);
                nextFireTime = Time.time + cooldownTime_fb;

            }
        }
    }
    void cooldownDisplay()
    {
        var timeLeft = nextFireTime - Time.time;
        timeLeft = timeLeft < 0 ? 0 : timeLeft; //tertiary operator, way of doing a 1 line if statement. If timeLeft < 0 return 0 otherwise return timeLeft.
        var cooldownUI = GameObject.FindGameObjectsWithTag("fbIconCD").FirstOrDefault();
        var cooldownTXT = cooldownUI.GetComponent<TextMeshProUGUI>();
        cooldownTXT.SetText(timeLeft.ToString("0.00"));
    }
}
