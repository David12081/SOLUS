using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoad : MonoBehaviour
{
    public string scene;
    
    private void OnEnable()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}