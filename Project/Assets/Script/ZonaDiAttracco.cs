using UnityEngine;

public class ZonaDiAttracco : MonoBehaviour
{
    public TipiDiCibi acceptedCategory;

    public bool CanAccept(TipiDiCibi food)
    {
        return food != null && food == acceptedCategory;
    }
}
