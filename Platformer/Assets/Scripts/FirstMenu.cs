using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenu : MonoBehaviour
{
    public void OpenSceneCafe()
    {
        SceneManager.LoadScene(1);
    }
}
