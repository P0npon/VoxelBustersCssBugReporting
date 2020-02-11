using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavigation
{
    void Next();
    void Previous();
    void PlayPause();
    Action<NavBarButton, bool> setEnableNavbarButton { get; set; }

}

public abstract class NavigationParent :MonoBehaviour, INavigation
{
    public abstract Action<NavBarButton, bool> setEnableNavbarButton { get; set; }

    public abstract void Next();

    public abstract void PlayPause();

    public abstract void Previous();
}
