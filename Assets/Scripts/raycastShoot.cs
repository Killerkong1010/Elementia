using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class raycastShoot : MonoBehaviour
{
    // Defines int and float variables that hold the rate of fire, damage and range.
    public int shootDamage = 10;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public Transform shootPoint; //Finds the x,y,z of the shootPoint.


    private Camera fpsCam; //Retrieves the camera/
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f); //Unity class that is used to suspends the coroutine execution for a given amount of seconds.
    private LineRenderer laserLine; //Linerenderer component 
    private float nextFire; //Float that determines the next fire time.
    Vector3 rayOrigin;

    public AudioSource Laser; //Retrieves the audio source.
    void Start() //Called at the beginning of runtime.
    {
        laserLine = GetComponent<LineRenderer>(); // Gets the linerenderer component.
        fpsCam = FindObjectOfType<Camera>(); //Finds objects that are of the type Camera
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") & Time.time > nextFire) //If the mouse button 1 is pressed and the current time is greater than the next fire time.
        {
            nextFire = Time.time + fireRate; //Calculates the next fire time.
            StartCoroutine(ShotEffect()); //Starts a coroutine with the ShotEffect function. Returns upon the first yield return.
            rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); // Uses the unity class ViewportToWorldPoint to calculate the direction the laser will shoot.
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, shootPoint.position); //Sets the position of the laserline to be the position of the shootpoint
            Laser.Play(); //Plays the laser sound effect.

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) // If the raycast ray hits an enemy within the weapon range then
            {
                laserLine.SetPosition(1, hit.point); // Sets the end position of the laserline to be the hitpoint.

                Enemy health = hit.collider.GetComponent<Enemy>(); //Gets the collider hit component of the enemy. 

                if (health != null) // If the enemy health is not null
                {
                    health.Damage(shootDamage); //Deals the shootDamage to the enemy.
                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange)); //Sets the end position of the laserLine if it doesnt hit anything.
            }
        }
    }

    private IEnumerator ShotEffect() //Used with the coroutine
    {
        laserLine.enabled = true; //Sets the laserline.enabled boolean to be true
        yield return shotDuration; //Yield returns the shot duration to the coroutine 
        laserLine.enabled = false; //disables the laserline.enabled booelaean.
    }
}
