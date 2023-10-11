using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ui_manager : MonoBehaviour
{
    private bool canExit;
    public Image exitLockImage;
    public float pauseDelay;

    void Start()
    {
        canExit = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Exit") && canExit) {
            canExit = false;
            StartCoroutine(setExit());

            if (Time.timeScale == 0) {
                Time.timeScale = 1;
                exitLockImage.fillAmount = 0;
            } else if (Time.timeScale == 1) {
                Time.timeScale = 0;
                exitLockImage.fillAmount = 1;
            }
        }
    }

    private IEnumerator setExit() 
    {
        yield return new WaitForSecondsRealtime(pauseDelay);
        canExit = true;
    }
}
