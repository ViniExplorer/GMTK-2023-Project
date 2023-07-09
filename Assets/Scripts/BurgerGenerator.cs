using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;

public class BurgerGenerator : MonoBehaviour
{
    // Duration of level, get from GameManager
    public float levelTime;

    // First burgers will have this number of components
    // excluding buns
    public int initialComps = 3;
    public int maxComps = 6; // At the final parts of the level
    int noComps; // Starts at 2 due to top and bottom bun

    // Vertical offset of burger components
    public float componentOffset = 1f;

    // Prefabs for the burger components
    public List<GameObject> componentPrefabs;
    // The current components on the screen
    List<GameObject> burgerComponents = new List<GameObject>();
    // The components of the burger guessed by the user
    List<GameObject> selectedBurgerComponents = new List<GameObject>();
    // The correct answer list for the components
    List<GameObject> correctBurgerComponents = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        levelTime = GameObject.FindObjectOfType<GameManager>().levelTime;
        noComps = initialComps;
        StopAllCoroutines();
        StartCoroutine(AddToTheNoComps());
        GenerateBurger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Gets a prefab of the burger part by its component type
    public GameObject GetBurgerComponentPrefab(BurgerComponent.componentType cType)
    {
        return componentPrefabs.Where(obj => obj.GetComponent<BurgerComponent>().component == cType).SingleOrDefault();
    }

    // Generate burger
    public void GenerateBurger()
    {
        // Generate top bun
        burgerComponents.Add(Instantiate(GetBurgerComponentPrefab(BurgerComponent.componentType.bunbottom), transform.position, Quaternion.identity));

        var totalNoComponentTypes = Enum.GetNames(typeof(BurgerComponent.componentType)).Length;
        // Debug.Log(totalNoComponentTypes);

        // Generates one meat type 
        burgerComponents.Add(Instantiate(GetBurgerComponentPrefab(BurgerComponent.componentType.meat), transform.position + new Vector3(0f, componentOffset), Quaternion.identity));

        // Generates all the other components
        for (int i = 1; i < noComps; i++)
        {
            int chosenComponentTypeIndex = UnityEngine.Random.Range(2, totalNoComponentTypes);
            // Debug.Log($"Component chosen: {(BurgerComponent.componentType)chosenComponentTypeIndex}");
            GameObject componentToAdd = GetBurgerComponentPrefab((BurgerComponent.componentType)chosenComponentTypeIndex);
            // Debug.Log($"{componentToAdd.name}");

            // Adds an offset position upwards
            Vector3 pos = transform.position + new Vector3(0f, componentOffset * (i+1));

            // Generates it onto the world
            burgerComponents.Add(Instantiate(componentToAdd, pos, Quaternion.identity));
            // Putting it in front
            try
            {
                burgerComponents[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            }
            catch {
                int num = burgerComponents[i].transform.childCount;
                for (int j = 0; j < num; j++) 
                {
                    burgerComponents[j].transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = i;
                }
            }
        }

        // Making the bottom bun
        burgerComponents.Add(Instantiate(GetBurgerComponentPrefab(BurgerComponent.componentType.buntop), transform.position + new Vector3(0f, componentOffset * (noComps + 1)), Quaternion.identity));
        burgerComponents[burgerComponents.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = noComps + 1;
        // Getting the correct answer defined
        correctBurgerComponents = burgerComponents;
    }



    // Checks if the selected burger components are the actual correct
    // ingredients with no regard to order
    public bool CheckAnswer()
    {
        return Enumerable.SequenceEqual(selectedBurgerComponents.OrderBy(e => e), correctBurgerComponents.OrderBy(e => e));
    }


    public void SelectBurger(BurgerComponent.componentType cType, bool adding=true)
    {
        if (adding) 
        {
            selectedBurgerComponents.Add(GetBurgerComponentPrefab(cType));
        } 
        else
        {
            selectedBurgerComponents.Remove(GetBurgerComponentPrefab(cType));
        }
    }



    #region Subroutines


    // Gradually adds to the number of components in the burgers generated
    IEnumerator AddToTheNoComps ()
    {
        int noUpdates = maxComps - initialComps + 1;
        float interval = levelTime / noUpdates;
        Debug.Log(interval);
        while (noComps < maxComps)
        {
            Debug.Log($"Waiting for {interval} seconds");
            yield return new WaitForSeconds(interval);
            noComps++;
        }
    }


    #endregion
}
