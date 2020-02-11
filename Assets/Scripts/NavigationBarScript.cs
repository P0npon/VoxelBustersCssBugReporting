using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBarScript : MonoBehaviour
{
    #region Variables Declaration
    //Navbar Button
    [SerializeField]
    private Button prevButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button nextButton;
    // Script that handle Navbar event
    [SerializeField]
    private NavigationParent navigationScript;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        prevButton.onClick.AddListener(navigationScript.Previous);
        nextButton.onClick.AddListener(navigationScript.Next);
        playButton.onClick.AddListener(navigationScript.PlayPause);
        navigationScript.setEnableNavbarButton = SetEnableNavbarButton;
    }
    /// <summary>
    /// Enable or disable a specific button of the navigation bar
    /// </summary>
    /// <param name="btn">The target button</param>
    /// <param name="enable">True to enable the button False to disable it</param>
    void SetEnableNavbarButton(NavBarButton btn,bool enable)
    {
        switch(btn)
        {
            case (NavBarButton.Previous):
                prevButton.enabled = enable;
                break;
            case (NavBarButton.PlayPause):
                playButton.enabled = enable;
                break;
            case (NavBarButton.Next):
                nextButton.enabled = enable;
                break;
        }
    }
}

public enum NavBarButton
{
    Previous,
    PlayPause,
    Next
}
