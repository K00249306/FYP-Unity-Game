using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void Singleplayer() {
        SceneManager.LoadScene(2);
    }

    public void Multiplayer() {
        SceneManager.LoadScene(1);
    }
}
