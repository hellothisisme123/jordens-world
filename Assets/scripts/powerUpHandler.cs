using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class powerUpHandler : MonoBehaviour
{
    public int coins;
    public TextMeshProUGUI coinHudUI;
    public TextMeshProUGUI coinShopUI;

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
