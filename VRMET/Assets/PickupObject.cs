using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
    GameObject mainCamera;
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;
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
        if (Input.GetKeyDown(KeyCode.P))
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
                    FirehoseText t = hit.collider.GetComponent<FirehoseText>();
                    Destroy(t.text3d);
                    this.gameObject.SetActive(false); // 호스를 집었으니 사라지도록
                    mainCamera.transform.FindChild("fire_hose_player").gameObject.SetActive(true);
                    
                    /*********************************************************/
                    //this.transform.parent = mainCamera.transform; // 집은 물건을 maincamera의 transfrom child로 등록 <- 이거 취소
                    //Debug.Log("carriedObject: " + carriedObject);
                    //carriedObject.transform.FindChild("SteamSpray").gameObject.SetActive(true); // Steamspray를 활성화.
                    //var steamspray =  carriedObject.transform.FindChild("SteamSpray");
                    //steamspray.gameObject.SetActive(true);
                    //Debug.Log("Hidden: " + steamspray.gameObject);
                    /***********************************************************/
                }
            }
        }
    }

    void checkDrop()
    {

    }

    void dropObject()
    {

    }
}
