using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject rocketObj;
    

    private void Start() {
        rocketObj = GameObject.Find("Rocket");
        //rocketStatScript.Instance.maxFuel += 100;
    }
}
