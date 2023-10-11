using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Mathematics;

public class ui_manager : MonoBehaviour
{
    private bool canExit;
    public GameObject shiftPauseLock;
    public float pauseDelay;
    public GameObject pressToStart;
    [SerializeField] bool screenLocked; 
    public GameObject notLockMenuItems; // the menu items hideable with shift (everything besides the lock)
    public GameObject pauseMenu;

    void Start()
    {
        canExit = true;
        screenLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Exit") && canExit) {
            // exit delay
            canExit = false;
            StartCoroutine(setExit());

            // locks screen
            screenLocked = !screenLocked;
        }

        // pauses the game and displays pause menu
        if (screenLocked) {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);

        } else { // unpauses and hides pause menu
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        // display only the lock when paused with shift held
        if (screenLocked && !Input.GetButton("hidePauseMenu")) {
            notLockMenuItems.SetActive(true);
            shiftPauseLock.SetActive(false);
        } else {
            shiftPauseLock.SetActive(true);
            notLockMenuItems.SetActive(false);
        }


        if (Input.GetMouseButtonDown(0)) {
            pressToStart.SetActive(false);
        }
    }

    private IEnumerator setExit() 
    {
        yield return new WaitForSecondsRealtime(pauseDelay);
        canExit = true;
    }
}
