using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

    public void RestartToMainMenu()
    {
        SceneManager.LoadScene(0);

    }
}
