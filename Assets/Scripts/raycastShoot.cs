using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class raycastShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public int shootDamage = 10;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public Transform shootPoint;


    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private LineRenderer laserLine;
    private float nextFire;
    Vector3 rayOrigin;
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        fpsCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") & Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, shootPoint.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                Enemy health = hit.collider.GetComponent<Enemy>();

                if (health != null)
                {
                    health.Damage(shootDamage);
                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
