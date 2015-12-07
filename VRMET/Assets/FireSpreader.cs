using UnityEngine;
using System.Collections;

public class FireSpreader : MonoBehaviour {

    public Transform prefab;
    public Vector3 fire_position;
    public float nextFire;
    private int FireOn = 0;
    //public float fireRate = 10.0F;
    //public float Region_length = 0.0F;
    //private float nextFire = 10.0F;
    //private int i = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire && FireOn==0) {
            Instantiate(prefab, fire_position, Quaternion.identity);
            FireOn = 1;
        }
        /*
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Transform clone = Instantiate(prefab, new Vector3(i * 1.0F, 0, 0), Quaternion.identity) as Transform;
            i += 1;
            System.Console.WriteLine("Fire instantiated");
        }
        //System.Console.WriteLine(Time.time);
        */
    }
}
