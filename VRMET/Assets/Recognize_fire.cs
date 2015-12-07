using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Recognize_fire : MonoBehaviour {

	public PhysicsRaycaster raycaster;
	int h_division = 5;
	int v_division = 5;
	bool flag = false;
	Vector3[,] samples;
	Vector3 center;
	float cur;
	GameObject[] text3d_list;
    GameObject text3d;
	// Use this for initialization
	void Start () {
		Debug.Log ("I am alive!");
		samples = new Vector3[h_division,v_division];
		flag = false;
		cur = Time.time;
		text3d = new GameObject();
	}

	// Update is called once per frame
	void Update () {
		//to draw a cross-hair of rectangular shape
		center = Camera.main.transform.position + (float)2.0*Camera.main.transform.forward;
		Vector3[] corner = {center + (float)0.2*Camera.main.transform.up - (float)0.2*Camera.main.transform.right, center + (float)0.2*Camera.main.transform.up + (float)0.2*Camera.main.transform.right, center - (float)0.2*Camera.main.transform.up + (float)0.2*Camera.main.transform.right, center - (float)0.2*Camera.main.transform.up - (float)0.2*Camera.main.transform.right};

		Debug.DrawLine (corner[0], corner[1], Color.green);
		Debug.DrawLine (corner[1], corner[2], Color.green);
		Debug.DrawLine (corner[2], corner[3], Color.green);
		Debug.DrawLine (corner[3], corner[0], Color.green);

		//to draw a grid of camera's sight
		for (int i=1; i<h_division; i++) {
			for(int j=1; j<v_division; j++) {
				Vector3 from1 = Camera.main.ScreenToWorldPoint (new Vector3(i*(Camera.main.pixelWidth/h_division), Camera.main.pixelHeight, Camera.main.nearClipPlane+(float)0.1));
				Vector3 to1 = Camera.main.ScreenToWorldPoint (new Vector3(i*(Camera.main.pixelWidth/h_division), 0, Camera.main.nearClipPlane+(float)0.1));
				Vector3 from2 = Camera.main.ScreenToWorldPoint (new Vector3(0, j*(Camera.main.pixelHeight/v_division), Camera.main.nearClipPlane+(float)0.1));
				Vector3 to2 = Camera.main.ScreenToWorldPoint (new Vector3(Camera.main.pixelWidth, j*(Camera.main.pixelHeight/v_division), Camera.main.nearClipPlane+(float)0.1));

				Debug.DrawLine (from1, to1, Color.green);
				Debug.DrawLine (from2, to2, Color.green);
			}
		}

        //sample for each section of the grid
        int emergency = 0;

		flag = true;
		float now = Time.time;
		if (now - cur > 0.5f) {
			for (int i=0; i<h_division; i++) {
				for (int j=0; j<v_division; j++) {
					
                    //samples [i, j] = Camera.main.ScreenToWorldPoint (new Vector3 ((i + Random.value) * Camera.main.pixelWidth / h_division, (j + Random.value) * Camera.main.pixelHeight / v_division, Camera.main.nearClipPlane + (float)0.1));
					samples [i, j] = Camera.main.transform.InverseTransformVector(Camera.main.ScreenToWorldPoint (new Vector3 ((i + Random.value) * Camera.main.pixelWidth / h_division, (j + Random.value) * Camera.main.pixelHeight / v_division, Camera.main.nearClipPlane + (float)0.1)) - Camera.main.transform.position);


                    //to cast a ray through the center of a camera
                    // ray의 작동원리에 대해 물어보고 제대로 수정해야함.
                    //var ray = Camera.main.ScreenPointToRay(new Vector3((i + Random.value) * Camera.main.pixelWidth / h_division, (j + Random.value) * Camera.main.pixelHeight / v_division, Camera.main.nearClipPlane + (float)0.1));
                   
                    var ray = Camera.main.ScreenPointToRay(samples[i ,j]);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        //print("I am looking at" + hit.transform.name);
                        //Detect firepoint in hit
                        FirePoint[] hitchilds = hit.transform.GetComponentsInChildren<FirePoint>();
                        //int emergency = 0;
                        foreach (FirePoint fp in hitchilds)
                        {
                            //Debug.Log(fp.name + " fire started? " + fp.fireStarted);
                            if (fp.fireStarted)
                            {
                                emergency = 1;
                                Debug.Log("fire recognize" + hit.transform.name + "  " + fp.transform.name);
                                Destroy(text3d);
                                text3d = new GameObject();
                                text3d.AddComponent<MeshRenderer>();
                                text3d.AddComponent<TextMesh>();
                                text3d.GetComponent<TextMesh>().text = hit.transform.name;
                                text3d.transform.position = fp.transform.position - ray.direction * 0.1f;
                                text3d.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                                text3d.transform.LookAt(Camera.main.transform.position);
                                text3d.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                                break;

                            }
                        }
                        
                        //
                        /*
                        Destroy(text3d);
                        text3d = new GameObject();
                        text3d.AddComponent<MeshRenderer>();
                        text3d.AddComponent<TextMesh>();
                        text3d.GetComponent<TextMesh>().text = hit.transform.name;
                        text3d.transform.position = hit.point - ray.direction * 0.5f;
                        text3d.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        text3d.transform.LookAt(Camera.main.transform.position);
                        text3d.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                        */
                        //GameObject.Find(hit.transform.name).AddComponent<MeshRenderer>();
                        //(GameObject.Find(hit.transform.name).GetComponent<Renderer>()).material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        Destroy(text3d);
                        print("I am looking at nothing!");
                    }
                }
			}
			cur = now;
		}


	}

	void OnDrawGizmos() {
		//Gizmos.DrawSphere (center, 0.01f);
		if (flag) {
			for (int i=0; i<h_division; i++) {
				for (int j=0; j<v_division; j++) {
					//Gizmos.DrawSphere (samples [i, j], 0.01f);
					Gizmos.DrawSphere (Camera.main.transform.TransformVector(samples [i, j]) + Camera.main.transform.position, 0.004f);
				}
			}
		}
	}
}
