using System.Collections.Generic;
using UnityEngine;

public class PyramidManager : MonoBehaviour
{
    public List<GrabAndDrag> allFoods = new List<GrabAndDrag>();

    [SerializeField]
    private string nextSceneName;

    [SerializeField]
    private GameObject victorySprite;

    private void Start()
    {
        // All'inizio, nasconde la sprite di vittoria
        if (victorySprite != null)
            victorySprite.SetActive(false);
    }

    // Questo metodo verrà chiamato ogni volta che un alimento si attacca
    public void CheckCompletion()
    {
        foreach (GrabAndDrag food in allFoods)
        {
            if (!food.IsCorrectlyAttached)
            {
                // Se almeno uno non è al posto giusto → non ancora completato
                return;
            }
        }

        // Se arriviamo qui, tutti i cibi sono corretti!
        if (victorySprite != null)
        {
            victorySprite.SetActive(true);
            GameManager.Instance.ChangeScene(nextSceneName);
        }
    }
}
