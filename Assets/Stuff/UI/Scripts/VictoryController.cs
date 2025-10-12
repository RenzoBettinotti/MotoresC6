using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{
    [SerializeField] private UIDocument VictoryUI;

    private void OnEnable()
    {
        VisualElement root = VictoryUI.rootVisualElement;

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