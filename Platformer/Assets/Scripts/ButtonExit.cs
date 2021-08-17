using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button btnExit;

    public void Exit()
    {
        Application.Quit();
    }
}
