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
    public float cooldownTime_heal = 20;
    private float nextFireTime;
    private float nextHealTime;

    public PlayerHealth healthbar;
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.time > nextHealTime)
            {
                healthbar.onHeal(30);
                nextHealTime = Time.time + cooldownTime_heal;
            }
        }
    }
    void cooldownDisplay()
    {
        var timeLeft_fb = nextFireTime - Time.time;
        var timeLeft_heal = nextHealTime - Time.time;
        timeLeft_heal = timeLeft_heal < 0 ? 0 : timeLeft_heal;
        timeLeft_fb = timeLeft_fb < 0 ? 0 : timeLeft_fb; //tertiary operator, way of doing a 1 line if statement. If timeLeft < 0 return 0 otherwise return timeLeft.
        var cooldownUI_fb = GameObject.FindGameObjectsWithTag("fbIconCD").FirstOrDefault();
        var cooldownUI_heal = GameObject.FindGameObjectsWithTag("healIconCD").FirstOrDefault();
        var cooldownTXT_fb = cooldownUI_fb.GetComponent<TextMeshProUGUI>();
        var cooldownTXT_heal = cooldownUI_heal.GetComponent<TextMeshProUGUI>();
        cooldownTXT_fb.SetText(timeLeft_fb.ToString("0.00"));
        cooldownTXT_heal.SetText(timeLeft_heal.ToString("0.00"));
    }
}
