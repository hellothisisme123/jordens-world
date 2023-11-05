using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour
{
    private bool canPause;
    public GameObject shiftPauseLock;
    public float pauseDelay;
    public GameObject pressToStart;
    [SerializeField] bool screenLocked; 
    public GameObject notLockMenuItems; // the menu items hideable with shift (everything besides the lock)
    public GameObject pauseMenu;
    public GameObject deathScreen;
    private bool alive; // tied to healthbar.cs
    public GameObject player;
    public SceneManager sceneManager;
    public GameObject nolansShop;
    public GameObject mainPauseMenu; // the pause menu that isn't nolans shop

    void Start()
    {
        canPause = true;
        screenLocked = true;
    }
    
    // called at the end of explosion animation in healthbar.cs
    public void playerDeath() {
        deathScreen.SetActive(true);

        canPause = false;
        StartCoroutine(setCanPause(false));
        screenLocked = false;
    }

    public void unpauseGame() {
        screenLocked = false;
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToShop() {
        nolansShop.SetActive(true);
        mainPauseMenu.SetActive(false);
    }

    public void leaveTheShop() {
        nolansShop.SetActive(false);
        mainPauseMenu.SetActive(true);
    }

    void Update()
    {
        alive = player.GetComponent<healthbar>().alive;

        if (Input.GetButton("Exit") && canPause) {
            // exit delay
            StartCoroutine(setCanPause(true));
            canPause = false;

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

    private IEnumerator setCanPause(bool val) 
    {
        yield return new WaitForSecondsRealtime(pauseDelay);
        canPause = val;
    }

    private IEnumerator setPauseMenuActive(bool val) 
    {
        yield return new WaitForSecondsRealtime(pauseDelay);
        pauseMenu.SetActive(val);
    }
}
