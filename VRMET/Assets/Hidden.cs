using UnityEngine;
using System.Collections;

public class Hidden : MonoBehaviour {
    public GameObject p;
	// Use this for initialization
	void Start () {
        p = this.gameObject;
        p.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
