using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public UIDocument mainMenuUI;

    private void OnEnable()
    {
        
        VisualElement root = mainMenuUI.rootVisualElement;

        
        Button startButton = root.Q<Button>("StartButton");
        Button optionsButton = root.Q<Button>("OptionsButton");
        Button quitButton = root.Q<Button>("QuitButton");

        if (startButton != null)
        {
            startButton.clicked += StartGame;
        }

        if (optionsButton != null)
        {
            optionsButton.clicked += OpenOptions;
        }

        if (quitButton != null)
        {
            quitButton.clicked += QuitGame;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("scene1"); 
    }

    private void OpenOptions()
    {
       
        Debug.Log("Opciones button clicked! (Not yet implemented)");
    }

    private void QuitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
#else
        Application.Quit();
#endif
    }
}