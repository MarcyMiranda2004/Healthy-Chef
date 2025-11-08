using UnityEngine;

public class Attach : MonoBehaviour
{
    [Header("Categoria accettata")]
    public FoodCategory acceptedCategory;

    private GrabAndDrag currentFood;

    public bool CanAccept(FoodType food)
    {
        // Accetta solo se la categoria corrisponde e la zona è libera
        return food != null && food.category == acceptedCategory && currentFood == null;
    }

    // Chiamato quando un alimento si attacca correttamente
    public void SetCurrentFood(GrabAndDrag food)
    {
        currentFood = food;
    }

    // Chiamato quando un alimento viene rimosso o spostato
    public void ClearCurrentFood()
    {
        currentFood = null;
    }
}
