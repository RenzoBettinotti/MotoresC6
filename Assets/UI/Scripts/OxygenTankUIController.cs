using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerHealthAndOxygen : MonoBehaviour
{
    [Header("UI Document")]
    [SerializeField] private UIDocument uiDocument;

    // --- Oxygen UI ---
    private VisualElement oxygenFillBar;
    private Label oxygenPercentageLabel;

    // --- Hearts (vidas) ---
    private List<VisualElement> hearts = new List<VisualElement>();
    private int currentHealth = 3; // 3 corazones

    // --- Oxygen ---
    [Range(0, 100)] public float currentOxygenLevel = 100f;
    public float maxBarHeight = 200f;

    private bool isBlinking = false;
    private Coroutine blinkCoroutine;



    private void OnEnable()
    {
        // Vinculamos con los elementos del UIDocument
        VisualElement root = uiDocument.rootVisualElement;

        oxygenFillBar = root.Q<VisualElement>("OxygenFillBar");
        oxygenPercentageLabel = root.Q<Label>("OxygenPercentageLabel");

        hearts.Add(root.Q<VisualElement>("Heart1"));
        hearts.Add(root.Q<VisualElement>("Heart2"));
        hearts.Add(root.Q<VisualElement>("Heart3"));
    }

    private void Update()
    {
        // El oxígeno se consume lentamente con el tiempo
        currentOxygenLevel = Mathf.Max(0, currentOxygenLevel - Time.deltaTime * 0.5f);
        UpdateOxygenUI();

        // Si el oxígeno está bajo, parpadea
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

        // Chequear si se acabó el juego
        CheckGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Smoke"))
        {
            ReduceOxygen(10f); // Reduce oxígeno al tocar humo
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            TakeDamage(); // Pierde un corazón al tocar fuego
        }
    }

    // --- Reducir oxígeno ---
    private void ReduceOxygen(float amount)
    {
        currentOxygenLevel = Mathf.Max(0, currentOxygenLevel - amount);
        UpdateOxygenUI();
    }

    // --- Reducir vida (corazones) ---
    private void TakeDamage()
    {
        currentHealth = Mathf.Max(0, currentHealth - 1);
        UpdateHearts();
    }

    // --- Actualizar UI del oxígeno ---
    private void UpdateOxygenUI()
    {
        float newHeight = maxBarHeight * (currentOxygenLevel / 100f);
        oxygenFillBar.style.height = new StyleLength(newHeight);
        oxygenPercentageLabel.text = $"{Mathf.CeilToInt(currentOxygenLevel)}%";
    }

    // --- Actualizar corazones ---
    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
                hearts[i].RemoveFromClassList("heart-empty");
            else
                hearts[i].AddToClassList("heart-empty");
        }
    }

    // --- Revisar si se acabó el juego ---
    private void CheckGameOver()
    {
        if (currentOxygenLevel <= 0f || currentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }

    // --- Efecto de parpadeo del oxígeno ---
    private IEnumerator BlinkEffect()
    {
        while (true)
        {
            oxygenFillBar.style.backgroundColor = new StyleColor(new Color(1f, 0.2f, 0.2f));
            yield return new WaitForSeconds(0.5f);

            oxygenFillBar.style.backgroundColor = new StyleColor(new Color(0.2f, 0.8f, 1f));
            yield return new WaitForSeconds(0.5f);
        }
    }
}

