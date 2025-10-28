using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    
    [SerializeField]
    private UIDocument uiDocument;

    private VisualElement root;
    private Button buttonContinuar;
    private Button buttonOpciones;
    private Button buttonSalir;


    private bool isPaused = false;

    private void OnEnable()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument is not assigned. Please assign it in the Inspector.");
            return;
        }

        root = uiDocument.rootVisualElement;

        // Hide the pause menu on start.
        root.style.display = DisplayStyle.None;

    
        buttonContinuar = root.Q<Button>("continuar-button");
        buttonOpciones = root.Q<Button>("opciones-button");
        buttonSalir = root.Q<Button>("salir-button");

      
        if (buttonContinuar != null)
        {
            buttonContinuar.clicked += OnClickContinuar;
        }
        if (buttonOpciones != null)
        {
            buttonOpciones.clicked += OnClickOpciones;
        }
        if (buttonSalir != null)
        {
            buttonSalir.clicked += OnClickSalir;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

   
    private void TogglePauseMenu()
    {
        if (isPaused)
        {
            // Resume the game.
            Time.timeScale = 1f;
            root.style.display = DisplayStyle.None;
            isPaused = false;
        }
        else
        {
            // Pause the game.
            Time.timeScale = 0f;
            root.style.display = DisplayStyle.Flex;
            isPaused = true;
        }
    }

    private void OnClickContinuar()
    {
        Debug.Log("Continuing game...");
       
        TogglePauseMenu();
    }

    private void OnClickOpciones()
    {
        Debug.Log("Opening options menu...");
      
    }

    private void OnClickSalir()
    {
        Debug.Log("Quitting game...");
       
        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnDisable()
    {
       
        if (buttonContinuar != null)
        {
            buttonContinuar.clicked -= OnClickContinuar;
        }
        if (buttonOpciones != null)
        {
            buttonOpciones.clicked -= OnClickOpciones;
        }
        if (buttonSalir != null)
        {
            buttonSalir.clicked -= OnClickSalir;
        }
    }
}