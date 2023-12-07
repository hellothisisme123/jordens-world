using System;
using TMPro;
using UnityEngine;

public class powerUp : MonoBehaviour {
    public string name;
    public MonoBehaviour script;
    public bool enabled;

    public enum powerUpType {
        bonzaiBuddy = 0,
        chargeShot = 1,
        mine = 2,
        multishot = 3
    }

    public powerUp(string n, MonoBehaviour s, bool e) {
        name = n;
        script = s;
        enabled = e;
    }
}