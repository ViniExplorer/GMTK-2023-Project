using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A script for the behaviour of a burger component. 
/// This gets the necessary colour for the component and assigns
/// it on the sprite renderer.
/// It can also make it disappear once selected correctly
/// </summary>
public class BurgerComponent : MonoBehaviour
{
    #region Burger Data
    public class BComponent
    {
        public GameObject gameObj;
        public Color color; // colour for when changing the burger

        public BComponent(List<int> rgb, GameObject gameObj = null)
        {
            this.color = new Color();
            // Applying the rgb components to the colour component
            for (int i = 0; i < 4; i++)
            {
                color[i] = (float)rgb[i] / 255f;
            }
            this.gameObj = gameObj;
        }
    }

    // Mapping out all the burger components' colors
    public static Dictionary<componentType, BComponent> components = new Dictionary<componentType, BComponent>()
    {
        { componentType.buntop, new BComponent(new List<int>{255, 208, 150, 255}) },
        { componentType.bunbottom, new BComponent(new List<int> { 255, 208, 150, 255}) },
        { componentType.meat, new BComponent(new List<int> { 92, 60, 28, 255 }) },
        { componentType.chicken, new BComponent(new List<int> { 224, 180, 132, 255 }) },
        { componentType.cheese, new BComponent(new List<int> { 237, 234, 59, 255 })},
        { componentType.pickles, new BComponent(new List<int>{45, 107, 0, 255})},
        { componentType.bacon, new BComponent(new List<int> { 255, 136, 117, 255 })},
        { componentType.avocado, new BComponent(new List<int> { 0, 130, 0, 255 })},
        { componentType.lettuce, new BComponent(new List<int> { 123, 255, 41, 255 })},
        { componentType.onions, new BComponent(new List<int> { 255, 219, 253, 255 })},
        { componentType.chillies, new BComponent(new List<int> { 222, 0, 0, 255 })},
        { componentType.friedEgg, new BComponent(new List<int> { 255, 230, 8, 255 })},
        { componentType.tomatoes, new BComponent(new List<int> { 255, 41, 25, 255 })}
    };

    // Each of the component types in enum form because dropdowns in Unity are cool
    public enum componentType
    {
        buntop,
        bunbottom,
        meat,
        chicken,
        cheese,
        pickles,
        bacon,
        avocado,
        lettuce,
        onions,
        chillies,
        friedEgg,
        tomatoes
    };


    
    #endregion

    public componentType component; // Gets the type of component for colour to be applied
    SpriteRenderer sprite; // Sprite for colour to be applied and changed
    BurgerGenerator generator;

    IngredientsList ingredientsList;
    SpriteRenderer[] childrenSprites;

    // Start is called before the first frame update
    void Start()
    {
        // Applying the colour to the grayscale sprite of the burger component
        try
        {
            sprite = GetComponent<SpriteRenderer>();
            // Debug.Log(components[component].color);
            sprite.color = (components[component].color);

        } catch
        {
            Debug.Log("crying noises");
        }
        ingredientsList = FindObjectOfType<IngredientsList>();
        generator = FindObjectOfType<BurgerGenerator>();
    }

    private void Awake()
    {
        // Applying the colour to the grayscale sprite of the burger component
        try
        {
            sprite = GetComponent<SpriteRenderer>();
            // Debug.Log(components[component].color);
            sprite.color = (components[component].color);

        }
        catch
        {
            try
            {
                childrenSprites = GetComponentsInChildren<SpriteRenderer>();
                foreach (var c in childrenSprites)
                {
                    c.color = components[component].color;
                }
            } catch
            {
                Debug.Log("crying noises");
            }
        }
        ingredientsList = FindObjectOfType<IngredientsList>();
        generator = FindObjectOfType<BurgerGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Other Functions
    

    public void GetToAdd()
    {
        ingredientsList.AddIngredient(component);
        generator.SelectBurger(component);
    }

    public void GetToRemove()
    {
        FindObjectOfType<BurgerGenerator>().SelectBurger(component, false);
        ingredientsList.RemoveIngredient(gameObject);
    }

    #endregion
}
