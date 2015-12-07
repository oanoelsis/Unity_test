using UnityEngine;
using System.Collections;

public class FirehoseText : MonoBehaviour {
    public GameObject text3d;
    public string datatext = "Press 'P' to pick it up";
	// Use this for initialization
	void Start () {
        text3d = new GameObject();
        text3d.AddComponent<MeshRenderer>();
        text3d.AddComponent<TextMesh>();
        text3d.GetComponent<TextMesh>().text = datatext;
        text3d.transform.position = this.transform.position;
        text3d.transform.Translate(new Vector3(-0.7f, -0.5f));
        text3d.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
