using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class InicilizationAds : MonoBehaviour
{
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
