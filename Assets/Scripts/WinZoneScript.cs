using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DeathZoneScript.IsLevelOver = true;
    }
}
