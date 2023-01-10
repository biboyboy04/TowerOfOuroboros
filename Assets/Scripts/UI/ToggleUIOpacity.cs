using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToggleUIOpacity : MonoBehaviour
{
    // Store the original opacity of all UI elements
    private Dictionary<Graphic, float> originalOpacity = new Dictionary<Graphic, float>();

    public void ToggleOpacity()
    {
        // Get all UI elements in the scene
        var uiElements = FindObjectsOfType<Graphic>();

        // Toggle the opacity of all UI elements
        foreach (var uiElement in uiElements)
        {
            if (!originalOpacity.ContainsKey(uiElement))
            {
                // Store the original opacity of the UI element
                originalOpacity.Add(uiElement, uiElement.color.a);

                // Set the opacity of the UI element to 0
                Color color = uiElement.color;
                color.a = 0;
                uiElement.color = color;
            }
            else
            {
                // Restore the original opacity of the UI element
                Color color = uiElement.color;
                color.a = originalOpacity[uiElement];
                uiElement.color = color;

                // Remove the UI element from the dictionary
                originalOpacity.Remove(uiElement);
            }
        }
    }
}
