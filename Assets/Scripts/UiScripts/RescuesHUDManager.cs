using UnityEngine;
using UnityEngine.UIElements;


public class RescuesHUDManager : MonoBehaviour
{
 
    [SerializeField]
    private int rescuesGoal = 5;

 
    private const string CivilianCountLabelName = "CivilianCountLabel";

    private Label civilianCountLabel;
    private int rescuesCount = 0;

    private void OnEnable()
    {
      
        ObjectoInteractivo.OnCivilianRescued += IncrementRescuesCount;
    }

    private void OnDisable()
    {
   
        ObjectoInteractivo.OnCivilianRescued -= IncrementRescuesCount;
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
            Debug.Log("All civilians rescued!");
            
        }
    }

    private void UpdateCounterDisplay()
    {
        if (civilianCountLabel != null)
        {
            civilianCountLabel.text = $"{rescuesCount} / {rescuesGoal}";
        }
    }
}