using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject prefab;
    GameObject healVFX;
    GameObject frostWave;
    public Transform healPoint;
    public Transform shootPoint;
    public Transform frostWavePoint;
    public Rigidbody Fireball;

    public float speed = 10;
    public float frostWave_Speed = 10;
    public float blinkSpeed;
    public float damage = 50;

    public float death_rate_fireball = 5;
    public float death_rate_frostWave = 5;

    public float cooldownTime_fb = 5;
    public float cooldownTime_heal = 20;
    public float cooldownTime_Blink = 1;
    public float cooldownTime_Frost = 2;

    private float nextFireTime;
    private float nextHealTime;
    private float nextBlinkTime;
    private float nextFrostTime;


    public PlayerHealth healthbar;
    public PlayerHealth energybar;
    void Start()
    {
        prefab = Resources.Load("Fireball") as GameObject;
        healVFX = Resources.Load("healEffect") as GameObject;
        frostWave = Resources.Load("frostWave") as GameObject;

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
                GameObject healEffect = Instantiate(healVFX) as GameObject;
                healEffect.transform.position = healPoint.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Time.time > nextFrostTime)
            {
                GameObject frostWaveObj = Instantiate(frostWave) as GameObject;
                frostWaveObj.transform.position = shootPoint.transform.position;
                Rigidbody rb = frostWaveObj.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * frostWave_Speed;
                Destroy(frostWaveObj, death_rate_frostWave);
                nextFrostTime = Time.time + cooldownTime_Frost;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    if (Time.time > nextBlinkTime)
        //    {

        //    }
        //}
    }
    void cooldownDisplay()
    {
        var timeLeft_fb = nextFireTime - Time.time;
        var timeLeft_heal = nextHealTime - Time.time;
        var timeLeft_frost = nextFrostTime - Time.time;

        timeLeft_heal = timeLeft_heal < 0 ? 0 : timeLeft_heal;
        timeLeft_fb = timeLeft_fb < 0 ? 0 : timeLeft_fb; //tertiary operator, way of doing a 1 line if statement. If timeLeft < 0 return 0 otherwise return timeLeft.
        timeLeft_frost = timeLeft_frost < 0 ? 0 : timeLeft_frost;

        var cooldownUI_fb = GameObject.FindGameObjectsWithTag("fbIconCD").FirstOrDefault();
        var cooldownUI_heal = GameObject.FindGameObjectsWithTag("healIconCD").FirstOrDefault();
        var cooldownUI_frost = GameObject.FindGameObjectsWithTag("frostIconCD").FirstOrDefault();

        var cooldownTXT_fb = cooldownUI_fb.GetComponent<TextMeshProUGUI>();
        var cooldownTXT_heal = cooldownUI_heal.GetComponent<TextMeshProUGUI>();
        var cooldownTXT_frost = cooldownUI_frost.GetComponent<TextMeshProUGUI>();

        cooldownTXT_fb.SetText(timeLeft_fb.ToString("0.00"));
        cooldownTXT_heal.SetText(timeLeft_heal.ToString("0.00"));
        cooldownTXT_frost.SetText(timeLeft_frost.ToString("0.00"));
    }
}
