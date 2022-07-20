using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowPlat : MonoBehaviour
{
	private bool touchPlayer = false;
	private GameObject P;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void OnCollisionEnter(Collision other)
	{
		Debug.Log("touch");
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("touch");
			touchPlayer = true;
			P = other.gameObject;
			P.transform.SetParent(this.transform);
		}
	}

	void OnCollisionExit(Collision other)
	{
		Debug.Log("untouch");
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("untouch");
			touchPlayer = false;
			P.transform.SetParent(null);
			P = null;
		}
	}
}
