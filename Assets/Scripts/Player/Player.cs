using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //21-08-2018//
    //John Shone//

    public string username;
    public bool human;

    public HUD hud;

    public Objects selectedObject { get; set; }


	void Start ()
    {
        hud = GetComponentInChildren<HUD>();
	}
	
	void Update ()
    {
		
	}
}
