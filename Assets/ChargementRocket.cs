using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargementRocket : MonoBehaviour {

    public Image charge;
    public float Value
    {
        get
        {
            return charge.fillAmount;
        }
    }
    
	
	// Update is called once per frame
	void Update () {
        charge.fillAmount = Mathf.PingPong(Time.time, 1);
    }
}
