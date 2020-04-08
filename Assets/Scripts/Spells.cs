using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    // Assigning variables that are used within this script.
    // Assigning gameobject variables that will be instantiated (spawned) into the game world.
    GameObject prefab;
    GameObject healVFX;
    GameObject frostWave;
    // Retrieving the transform (x,y,z) of the desired gameobject. There are where the GameObjects will be instantiated from.
    public Transform healPoint;
    public Transform shootPoint;
    public Transform frostWavePoint;
    //Rertrieving the rigidbody component of the Fireball gameobject so that force can be applied.
    public Rigidbody Fireball;
    //floats that represent the speed and damage of the different spells as well as their death rate(time within the game world).
    public float speed = 10;
    public float frostWave_Speed = 10;
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

    public AudioSource fireSound; // Retrieves the fire sound effect.




    //Obtaining the resource bars for health and energy so they can be manipulated within the code.
    public PlayerHealth healthbar;
    public PlayerEnergy energybar;
    //Start function called on the inital frame - At the beginning of the runtime.
    void Start()
    {
        prefab = Resources.Load("Fireball") as GameObject;//Retrieving the "Fireball" gameobject from the resources folder and storing it as a gameobject.
        healVFX = Resources.Load("healEffect") as GameObject;//Retrieving the "healEffect" gameobject from the resources folder and storing it as a gameobject.
        frostWave = Resources.Load("frostWave") as GameObject;//Retrieving the "frostWave" gameobject from the resources folder and storing it as a gameobject.

    }

    // Update is called once per frame
    void Update()
    {
        cooldownDisplay();//Calling the cooldownDisplay function.
        if (Input.GetKeyDown(KeyCode.Alpha1)) //If logic = If the key with keycode Alpha1 (1 key) is pressed then
        {
            if (Time.time > nextFireTime) // If the Time.time (time since the program began running) is greater than the nextFireTime (cooldown of the spell) then
            {
                GameObject projectile = Instantiate(prefab) as GameObject; // Instantiating the prefab Gameobject (fireball) into the game under the projectile name.
                //var pos = transform.position + Camera.main.transform.forward * 2;
                //pos.y += 0.5f;
                //pos.x += 1.0f;

                projectile.transform.position = shootPoint.transform.position; //Setting the transform (x,y,z) of the projectile to be the transform of the shootPoint (where it shoots from)
                Rigidbody rb = projectile.GetComponent<Rigidbody>(); //Getting the rigidbody component of the projectile - Allows force to be added.
                rb.velocity = Camera.main.transform.forward * speed;// Adding force to the projectile causing it to move forward dictated by the camera transform and the speed value
                fireSound.Play(); //Plays the fire sound effect
                Destroy(projectile, death_rate_fireball); // Destroys the projectile after a certain number of seconds have passed.
                nextFireTime = Time.time + cooldownTime_fb; //Calculating the next fire time = The cooldown of the ability.
                

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//If logic = If the key with keycode Alpha2 (2 key) is pressed then
        {
            if (Time.time > nextHealTime) // If the Time.time (time since the program began running) is greater than the nextHealTime (cooldown of the spell) then
            {
                healthbar.onHeal(30); //calling the onHeal function from another scripts with the integer 30 being passed in.
                energybar.OnEnergyHeal(50); // Calling the onEnergyHeal function from another script with the integer 50 being passed in.
                nextHealTime = Time.time + cooldownTime_heal; // Calculating the next heal time using the time elapsed from the start and the heal cooldown.
                GameObject healEffect = Instantiate(healVFX) as GameObject; // Instantiating the gameobject healEffect into the game world
                healEffect.transform.position = healPoint.transform.position; // Setting the transform of the newly instantiated healEffect to be the healPoint.
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//If logic = If the key with keycode Alpha3 (3 key) is pressed then
        {
            if (Time.time > nextFrostTime)// If the Time.time (time since the program began running) is greater than the nextFrostTime (cooldown of the spell) then
            {
                GameObject frostWaveObj = Instantiate(frostWave) as GameObject; // Instantiating the frostWave gameobject into the game world as frostWaveObj
                frostWaveObj.transform.position = shootPoint.transform.position; // Setting the transform (x,y,z) to be shootPoint.
                Rigidbody rb = frostWaveObj.GetComponent<Rigidbody>(); //Aquiring the rigidbody component of the gameobject which allows force to be applied.
                rb.velocity = Camera.main.transform.forward * frostWave_Speed; // Adds force to the frostwaveObject making it move dictated by the camera transform and speed value
                Destroy(frostWaveObj, death_rate_frostWave); //Destroys the frostWaveObject after a number of seconds.
                nextFrostTime = Time.time + cooldownTime_Frost; // Calculates the cooldown of the frostWave ability.
            }
        }
    }
    void cooldownDisplay() //Function that displays the cooldowns of the different spells into the UI. Called in the Update() function.
    {
        //Calculating the time left on the different spell cooldowns and storing them in variables.
        var timeLeft_fb = nextFireTime - Time.time; 
        var timeLeft_heal = nextHealTime - Time.time;
        var timeLeft_frost = nextFrostTime - Time.time;
        //Uses a tertiary operator to determine whether timeleft < 0. If it is, a 0 is returned, otherwise the timeLeft value is returned.
        // Tertiary operator is a way of doing a 1 line if statement.
        timeLeft_heal = timeLeft_heal < 0 ? 0 : timeLeft_heal;
        timeLeft_fb = timeLeft_fb < 0 ? 0 : timeLeft_fb; 
        timeLeft_frost = timeLeft_frost < 0 ? 0 : timeLeft_frost;
        // Finding the UI components with the determining tags. These will be used to access the text components that will be edited.
        var cooldownUI_fb = GameObject.FindGameObjectsWithTag("fbIconCD").FirstOrDefault(); //FirstOrDefault selects the first tag that is returned.
        var cooldownUI_heal = GameObject.FindGameObjectsWithTag("healIconCD").FirstOrDefault();
        var cooldownUI_frost = GameObject.FindGameObjectsWithTag("frostIconCD").FirstOrDefault();
        // Accessing the UI text components that will be changed when the time left on the cooldown changes.
        var cooldownTXT_fb = cooldownUI_fb.GetComponent<TextMeshProUGUI>();
        var cooldownTXT_heal = cooldownUI_heal.GetComponent<TextMeshProUGUI>();
        var cooldownTXT_frost = cooldownUI_frost.GetComponent<TextMeshProUGUI>();
        // Setting the text components to be the timeLeft variable (which holds the remaining cooldown of a spell) and changing it to a strings so that it can be displayed.
        cooldownTXT_fb.SetText(timeLeft_fb.ToString("0.00")); //displaying the cooldown to 2 decimal places.
        cooldownTXT_heal.SetText(timeLeft_heal.ToString("0.00"));//displaying the cooldown to 2 decimal places.
        cooldownTXT_frost.SetText(timeLeft_frost.ToString("0.00"));//displaying the cooldown to 2 decimal places.
    }
}
