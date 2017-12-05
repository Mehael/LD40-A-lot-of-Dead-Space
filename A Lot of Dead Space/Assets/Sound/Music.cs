using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource ALODS;
    public void ALODSPlay()
    {
        if (ALODS != null)
            ALODS.Play();
    }

    public AudioSource Win;
    public void WinPlay()
    {
        if (Win != null)
            Win.Play();
    }

    public AudioSource Place;
    public void PlacePlay()
    {
        if (Place != null)
            Place.Play();
    }

    public AudioSource Money;
    public void MoneyPlay()
    {
        if (Money != null)
            Money.Play();
    }

    public AudioSource Completed;
    public void CompletedPlay()
    {
        if (Completed != null)
            Completed.Play();
    }
}
