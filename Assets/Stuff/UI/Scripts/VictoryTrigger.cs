using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    [Tooltip("The name of the Victory Scene to load.")]
    public string victorySceneName = "Victory";

    
    private void OnTriggerEnter(Collider other)
    {
        
        //replace "Player" with the actual tag of your player GameObject.
        if (other.CompareTag("Player"))
        {
       
            Time.timeScale = 1f;

            // Load the victory scene
            LoadVictoryScene();
        }
    }

    private void LoadVictoryScene()
    {
        
        if (Application.CanStreamedLevelBeLoaded(victorySceneName))
        {
            SceneManager.LoadScene(victorySceneName);
        }
        else
        {
            Debug.LogError($"Scene '{victorySceneName}' not found in Build Settings! Please add it.");
        }
    }
}