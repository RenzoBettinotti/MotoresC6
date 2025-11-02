using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; 

public class RescuesHUDManager : MonoBehaviour
{

    [SerializeField]
    private int rescuesGoal = 7; 


    private const string CivilianCountLabelName = "CivilianCountLabel";

    private Label civilianCountLabel;
    private int rescuesCount = 0;

    private void OnEnable()
    {

        CivilianRescuer.OnCivilianRescued += IncrementRescuesCount;
    }

    private void OnDisable()
    {
        CivilianRescuer.OnCivilianRescued -= IncrementRescuesCount;
    }

    private void Start()
    {

        var uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("RescuesHUDManager requires a UIDocument component on the same GameObject.");
            return;
        }


        VisualElement root = uiDocument.rootVisualElement;
        civilianCountLabel = root.Q<Label>(CivilianCountLabelName);

        if (civilianCountLabel == null)
        {
            Debug.LogError($"Could not find a Label named '{CivilianCountLabelName}' in the UI Document.");
            return;
        }


        UpdateCounterDisplay();
    }


    private void IncrementRescuesCount()
    {
        rescuesCount++;
        UpdateCounterDisplay();

        if (rescuesCount >= rescuesGoal)
        {
            Debug.Log("¡Todos los civiles rescatados! Pasando a escena Victory.");

            // CAMBIO 2: Lógica para cargar la escena
            LoadVictoryScene();
        }
    }

    private void UpdateCounterDisplay()
    {
        if (civilianCountLabel != null)
        {
            civilianCountLabel.text = $"{rescuesCount} / {rescuesGoal}";
        }
    }

    // NUEVO MÉTODO para cargar la escena de victoria
    private void LoadVictoryScene()
    {
        // Asegúrate de que la escena "Victory" esté añadida a las
        // "Scenes In Build" en File -> Build Settings...
        SceneManager.LoadScene("Victory");
    }
}