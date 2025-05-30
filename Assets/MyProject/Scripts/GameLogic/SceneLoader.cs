using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel(int scene) => 
        SceneManager.LoadScene(scene);

    public void RestartLevel() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
