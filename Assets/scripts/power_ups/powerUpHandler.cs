using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class powerUpHandler : MonoBehaviour
{
    public int coins;
    public TextMeshProUGUI coinHudUI;
    public TextMeshProUGUI coinShopUI;

    public bonzaiBuddy bonzaiBuddy;
    public chargeShot chargeShot;
    public mine mine;
    public multishot multishot;

    public GameObject toggle_bonzaiBuddy;
    public GameObject toggle_chargeShot;
    public GameObject toggle_mine;
    public GameObject toggle_multishot;
    
    public void multishotCount(string s) {
        uiSetSetting(ref multishot.multiShotCount, s);
    }

    public void multishotSpread(string s) {
        uiSetSetting(ref multishot.multiShotSpread, s);
    }

    public void multishotDelay(string s) {
        uiSetSetting(ref multishot.shootDelay, s);
    }
    
    public void multishotRecoil(string s) {
        uiSetSetting(ref multishot.knockbackForce, s);
    }

    public void multishotSpeed(string s) {
        uiSetSetting(ref multishot.projspeed, s);
    }

    public void bonzaiSpeed(string s) {
        uiSetSetting(ref bonzaiBuddy.projspeed, s);
    }

    public void bonzaiDelay(string s) {
        uiSetSetting(ref bonzaiBuddy.shootDelay, s);
    }

    public void chargeshotMinChargeTime(string s) {
        uiSetSetting(ref chargeShot.minChargeMult, s);
    }
    
    public void chargeshotChargeTime(string s) {
        uiSetSetting(ref chargeShot.chargeDuration, s);
    }

    public void chargeshotSpeed(string s) {
        uiSetSetting(ref chargeShot.projspeed, s);
    }

    public void uiSetSetting(ref int i, string v) {
        int v2 = 0;
        try {
            v2 = int.Parse(v);
        } catch {
            Debug.Log($"powerUpHandler.uiSetSetting(ref int i, string v) could not convert string to int: {v}");
        }
        i = v2;
    }

    public void uiSetSetting(ref float f, string v) {
        float v2 = 0;
        try {
            v2 = float.Parse(v);
        } catch {
            Debug.Log($"powerUpHandler.uiSetSetting(ref float i, string v) could not convert string to float: {v}");
        }
        f = v2;
    }

    public void uiSetSetting(ref bool b, string v) {
        bool v2 = false;
        try {
            v2 = bool.Parse(v);
        } catch {
            Debug.Log($"powerUpHandler.uiSetSetting(ref bool i, string v) could not convert string to bool: {v}");
        }
        b = v2;
    }

    


    public string simplifyNum(float n) {
        string res = $"{n}";
        if (n > 1000) {
            res = $"{Mathf.Floor(n/100) / 10}k";
        }
        if (n > 1000000) {
            res = $"{Mathf.Floor(n/100000) / 10}m";
        }
        return res;
    }

    public void incrementCoins(int c) {
        coins += c;
        coinHudUI.text = $"Coins: {simplifyNum(coins)}";
        coinShopUI.text = $"Coins: {coins}";
    }
    
    public powerUpType currentActivePowerUp = powerUpType.none;

    public enum powerUpType {
        none = 0,
        bonzaiBuddy = 1,
        chargeShot = 2,
        mine = 3,
        multishot = 4
    }

    public void setPowerUp(string p) {
        // string p is converted to the poweruptype enum
        // if the powerup being set is currently active
        // it sets the currently active powerup to none instead of what it is already
        powerUpType tmp = (powerUpType)Enum.Parse(typeof(powerUpType), p);
        if (currentActivePowerUp == tmp) tmp = powerUpType.none;
        
        currentActivePowerUp = tmp;
        enablePowerUps();
    }

    public void enablePowerUps() {
        bonzaiBuddy.resetShootDelay();
        // mine.resetShootDelay();
        multishot.resetShootDelay();

        if (currentActivePowerUp == powerUpType.none) {
            bonzaiBuddy.enabled = false;
            chargeShot.enabled = false;
            // mine.enabled = false;
            multishot.enabled = false;


            toggle_bonzaiBuddy.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_chargeShot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            // toggle_mine.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_multishot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);

        } else if (currentActivePowerUp == powerUpType.mine) {
            bonzaiBuddy.enabled = false;
            chargeShot.enabled = false;
            // mine.enabled = true;
            multishot.enabled = false;

            toggle_bonzaiBuddy.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_chargeShot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            // toggle_mine.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
            toggle_multishot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        } else if (currentActivePowerUp == powerUpType.chargeShot) {
            bonzaiBuddy.enabled = false;
            chargeShot.enabled = true;
            // mine.enabled = false;
            multishot.enabled = false;

            toggle_bonzaiBuddy.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_chargeShot.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
            // toggle_mine.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_multishot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        } else if (currentActivePowerUp == powerUpType.multishot) {
            bonzaiBuddy.enabled = false;
            chargeShot.enabled = false;
            // mine.enabled = false;
            multishot.enabled = true;

            toggle_bonzaiBuddy.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_chargeShot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            // toggle_mine.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_multishot.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
        } else if (currentActivePowerUp == powerUpType.bonzaiBuddy) {
            bonzaiBuddy.enabled = true;
            chargeShot.enabled = false;
            // mine.enabled = false;
            multishot.enabled = false;

            toggle_bonzaiBuddy.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
            toggle_chargeShot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            // toggle_mine.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            toggle_multishot.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        } 
    } 

    void Start()
    {
        enablePowerUps();
    }
}
