using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dianji : MonoBehaviour {
  
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseDown()
    {
       
        Vendor.Instance.DisplaySwitch();
    }
}
