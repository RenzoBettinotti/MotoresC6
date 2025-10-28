using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private UIDocument deathScreenUI;

    private void OnEnable()
    {
        VisualElement root = deathScreenUI.rootVisualElement;

        Button mainMenuButton = root.Q<Button>("MainMenuButton");
        if (mainMenuButton != null)
        {
            mainMenuButton.clicked += LoadMainMenu;
        }
    }

    private void LoadMainMenu()
    {
       
        SceneManager.LoadScene("MainMenu");
    }
}