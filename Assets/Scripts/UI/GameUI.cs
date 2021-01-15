using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument), typeof(EventSystem))]
public class GameUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement rootVisualElement;

    private void Awake()
    {
        this.uiDocument = GetComponent<UIDocument>();
        this.rootVisualElement = this.uiDocument.rootVisualElement;
        Assert.IsNotNull(this.uiDocument, "Missing asset");

        //var menu = this.rootVisualElement.Q<VisualElement>(className: "menu");
            //label = root.Q<Label>(className: "container__label");
            //menu.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

        VisualElement p1 = this.rootVisualElement.Q<VisualElement>(name: "HealthBarPlayer1");
        //VisualElement p2 = this.rootVisualElement.Q<VisualElement>(name: "player2");

        p1.Clear();
        Debug.Log("Debug: " + p1);
    }

}
