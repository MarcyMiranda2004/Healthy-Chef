using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Punti : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntiText;

    private void Update()
    {
        stampaPunti();
    }

    private void stampaPunti()
    {
        if (puntiText != null && GameManager.Instance != null)
        {
            puntiText.text = "Punti: " + PointManager.Instance.GetPoints();
        }
    }
}
