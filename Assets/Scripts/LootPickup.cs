using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LootPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public float gold = 0;
    public Text txt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goldUpdate()
    {
        txt.text = gold.ToString();
    }
}
