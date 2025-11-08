using UnityEngine;

public enum FoodCategory
{
    Base,
    ConsumoModerato,
    ConsumoLimitato,
    Occasionale,
}

public class FoodType : MonoBehaviour
{
    public FoodCategory category;
}
