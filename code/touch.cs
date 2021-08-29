using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour {

    GameObject chan;
    Animator anim;
    public AudioSource a;
	// Use this for initialization
	void Start () {
        chan = GameObject.Find("unitychan");
        anim = chan.GetComponent<Animator>();
        a = chan.GetComponent<AudioSource>();
        
	}
     void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("Run", true);
        a.Play();
    }
     void OnCollisionExit(Collision collision)
    {
        anim.SetBool("Run", false);
    }
   
}
