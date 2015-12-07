using UnityEngine;
using System.Collections;

public class PickupExtinguisher : MonoBehaviour {
    GameObject mainCamera;
    bool carrying;
    GameObject carriedObject;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (carrying)
        {
            return;
        }
        else
        {
            pickup();
        }
	}

    void pickup()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                Debug.Log("pick: " + p);
                if(p != null)
                {
                    carrying = true;
                    carriedObject = p.gameObject;
                    ExtinguisherText t = hit.collider.GetComponent<ExtinguisherText>();
                    Destroy(t.text3d);
                    this.gameObject.SetActive(false);
                    //var d = mainCamera.transform.FindChild("Fire_Extinguisher_player");
                    var d = mainCamera.transform.GetChild(0);
                                       
                    Debug.Log(d);
                    mainCamera.transform.FindChild("Fire_Extinguisher_player").gameObject.SetActive(true);
                }
            }
        }
    }
}
