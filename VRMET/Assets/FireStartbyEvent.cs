using UnityEngine;
using System.Collections;

public class FireStartbyEvent : MonoBehaviour {
    public int fire_begin_time = 0;
    public float fireSpread_radius = 2.0F;
    private int fire_begin = 0;
    private Vector3 spPos = Vector3.zero; 
	// Use this for initialization
	void Start () {
        spPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // when elapsed time is bigger than some threshold fire begin
        //Debug.Log(Time.time.ToString());
        if (Time.time > fire_begin_time && (fire_begin==0))
        {
            Debug.Log("FireEventOccur!");
            var InRange = Physics.OverlapSphere(spPos, fireSpread_radius);
            Debug.Log(InRange[0]);
            Debug.Log(spPos);
            Debug.Log(transform.position);
            foreach (Collider all in InRange)
            {
                if (all.GetComponent("FirePoint"))
                {
                    //Debug.Log("FirePoint detected.");
                    all.SendMessage("startFire");
                } 
            }
            fire_begin = 1;



            /*
            Debug.Log(this.GetComponent("FirePoint").ToString());
            if (this.GetComponent("FirePoint"))
            {
                this.SendMessage("startFire");
                fire_begin = 1;
            }
            */
        }
	}
}
