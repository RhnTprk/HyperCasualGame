using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI txtStart;
    [SerializeField] private GameObject txtPaused, btnMenu, menu,leaderboard, runkingSlider;
    
    private bool beginGame = false, paused = false;

    #endregion
    void Update()
    {

    }

    public void btnStart_Click()
    {
        btnMenu.SetActive(true);
        leaderboard.SetActive(true);

        txtStart.text = "Resume";
        beginGame = true;
        menu.SetActive(false);
        paused = false;
    }

    public void btnQuit_Click()
    {
        Application.Quit();
    }

    public void btnMenu_Click()
    {
        leaderboard.SetActive(!leaderboard.activeInHierarchy);
        txtPaused.SetActive(!txtPaused.activeInHierarchy);
        btnMenu.SetActive(!btnMenu.activeInHierarchy);
        menu.SetActive(true);
        paused = true;
    }

    public bool isBeginGame { get { return beginGame; } }

    public bool isPaused { get { return paused; } set { paused = value; } }
}
