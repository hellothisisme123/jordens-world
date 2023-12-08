using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class powerUp : MonoBehaviour {
    public powerUpHandler powerUpHandler;

    
    public GameObject toggleSwitch;
    public MonoBehaviour powerUpScript;
    public bool purchased = false;
    public int purchaseCost;
    public int upgradesUnlocked;

    // stores the data for each upgradable value
    public List<string> upgradeNames = new List<string>();  // the names of each upgrade
    public List<Vector2> upgradeMinMaxs = new List<Vector2>(); // min and max values for the upgrade 
    public List<int> upgradeLevels = new List<int>(); // current level of each upgrade
    public List<int> upgradeCosts = new List<int>(); // the current cost of each upgrade
    public abstract List<int> upgradeCostFuncs(); // the functions responsible for increasing the cost with each upgrade

    public void costIncrement(int upgrade) {
        
    } 

    public void buyNewUpgrade() {
        upgradesUnlocked--;
        
    }
}