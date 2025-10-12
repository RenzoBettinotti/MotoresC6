using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 

public class OxygenTankUIController : MonoBehaviour
{
    
    [SerializeField] private UIDocument uiDocument;

   
    private VisualElement oxygenFillBar;
    private Label oxygenPercentageLabel;

  
    private List<VisualElement> hearts = new List<VisualElement>();

    // Player's current health
    private int currentHealth = 3;

    // A variable to simulate the current oxygen level (0-100)
    [Range(0, 100)]
    public float currentOxygenLevel = 100f;

    // The maximum height of the fill bar in pixels.
    public float maxBarHeight = 200f;

    private bool isBlinking = false;
    private Coroutine blinkCoroutine;

    private void OnEnable()
    {
        
        VisualElement root = uiDocument.rootVisualElement;

        oxygenFillBar = root.Q<VisualElement>("OxygenFillBar");
        oxygenPercentageLabel = root.Q<Label>("OxygenPercentageLabel");

      
        hearts.Add(root.Q<VisualElement>("Heart1"));
        hearts.Add(root.Q<VisualElement>("Heart2"));
        hearts.Add(root.Q<VisualElement>("Heart3"));
    }

    private void Update()
    {
     
        currentOxygenLevel = Mathf.Max(0, currentOxygenLevel - Time.deltaTime * 1.5f);

    
        UpdateOxygenUI();

       
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage();
        }

       
        if (currentOxygenLevel <= 25f && !isBlinking)
        {
            blinkCoroutine = StartCoroutine(BlinkEffect());
            isBlinking = true;
        }
        else if (currentOxygenLevel > 25f && isBlinking)
        {
            StopCoroutine(blinkCoroutine);
            isBlinking = false;
            
            oxygenFillBar.style.backgroundColor = new StyleColor(new Color(0.2f, 0.8f, 1f));
        }

        CheckGameOver();
    }

    private void UpdateOxygenUI()
    {
        
        float newHeight = maxBarHeight * (currentOxygenLevel / 100f);

       
        oxygenFillBar.style.height = new StyleLength(newHeight);

        oxygenPercentageLabel.text = $"{Mathf.CeilToInt(currentOxygenLevel)}%";
    }

    // New function to handle taking damage and updating hearts
    private void TakeDamage()
    {
        // Decrease health
        currentHealth = Mathf.Max(0, currentHealth - 1);

        // Update the heart icons
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        // Loop through all the hearts
        for (int i = 0; i < hearts.Count; i++)
        {
           
            if (i < currentHealth)
            {
                // Remove the "heart-empty" class if it exists
                hearts[i].RemoveFromClassList("heart-empty");
            }
            
            else
            {
                // Add the "heart-empty" class to make it black
                hearts[i].AddToClassList("heart-empty");
            }
        }
    }

    private void CheckGameOver()
    {
        // Check if oxygen is zero or health is zero
        if (currentOxygenLevel <= 0f || currentHealth <= 0)
        {
            
            SceneManager.LoadScene("DeathScreen");
        }
    }

    
    private IEnumerator BlinkEffect()
    {
        while (true)
        {
            // Set the color to red
            oxygenFillBar.style.backgroundColor = new StyleColor(new Color(1f, 0.2f, 0.2f));
            yield return new WaitForSeconds(0.5f); // Wait for half a second

            // Set the color to blue
            oxygenFillBar.style.backgroundColor = new StyleColor(new Color(0.2f, 0.8f, 1f));
            yield return new WaitForSeconds(0.5f); // Wait for half a second
        }
    }
}