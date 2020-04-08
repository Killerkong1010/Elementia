using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSFX : MonoBehaviour
{
    public AudioSource fireSound;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireSound.Play();
        }
    }
}
