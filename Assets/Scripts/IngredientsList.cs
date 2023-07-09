using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IngredientsList : MonoBehaviour
{
    public List<GameObject> ingredientIconPrefabs;
    public List<GameObject> currentChildren;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GetIngredientComponent(BurgerComponent.componentType cType, List<GameObject> listOfGameObjects)
    {
        return listOfGameObjects.Where(obj => obj.GetComponent<BurgerComponent>().component == cType).SingleOrDefault();
    }

    public void AddIngredient(BurgerComponent.componentType componentType)
    {
        GameObject gToAdd = GetIngredientComponent(componentType, ingredientIconPrefabs);
        currentChildren.Add(Instantiate(gToAdd, transform));
    }

    public void RemoveIngredient(GameObject thing)
    {
        /*
        for (int i = 0; i < currentChildren.Count; i++)
        {
            GameObject gToRemove = transform.GetChild(i).gameObject;
            if (gToRemove.GetComponent<BurgerComponent>().component == componentType)
            {
                Destroy(gToRemove);
            }
        }
        */
        Destroy(thing);
    }

}
