using UnityEngine;

public class TipiDiCibi : MonoBehaviour
{
    public enum FoodCategory
    {
        Base,
        FruitsVegetables,
        ProteinsDairy,
        Fats,
        Occasional,
    }

    public class FoodType : MonoBehaviour
    {
        public FoodCategory category;
    }
}
