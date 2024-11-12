using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject customizeMenu;
    [SerializeField] private Image playerColorDisplay;

    [Header("Settings")]
    [SerializeField] private List<Color> playerColors = new List<Color>{Color.white};

    [Header("References")]
    [SerializeField] private Material playerMaterial;

    [Header("Local Variables")]
    private int selectedColorIndex = 0;

    private void Start()
    {
        if (playerColors.Count == 0)
        {
            InitializeColors();
        }
        playerMaterial.color = playerColors[selectedColorIndex];
        playerColorDisplay.color = playerColors[selectedColorIndex];
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCustomizeClick()
    {
        mainMenu.SetActive(false);
        customizeMenu.SetActive(true);
    }

    public void OnCustomizeBackClick()
    {
        mainMenu.SetActive(true);
        customizeMenu.SetActive(false);
    }

    private void InitializeColors()
    {
        playerColors.Add(Color.white);
        selectedColorIndex = 0;
    }

    public void OnNextColorClick()
    {
        if (playerColors.Count == 0)
        {
            InitializeColors();
        }
        else if (selectedColorIndex < playerColors.Count - 1)
        {
            selectedColorIndex++;
        }
        else if (selectedColorIndex == playerColors.Count - 1)
        {
            selectedColorIndex = 0;
        }

        playerMaterial.color = playerColors[selectedColorIndex];
        playerColorDisplay.color = playerColors[selectedColorIndex];
    }

    public void OnPreviousColorClick()
    {
        if (playerColors.Count == 0)
        {
            InitializeColors();
        }
        else if (selectedColorIndex > 0)
        {
            selectedColorIndex--;
        }
        else if (selectedColorIndex == 0)
        {
            selectedColorIndex = playerColors.Count - 1;
        }

        playerMaterial.color = playerColors[selectedColorIndex];
        playerColorDisplay.color = playerColors[selectedColorIndex];
    }

    public void Exit()
    {
        Application.Quit();
    }
}
