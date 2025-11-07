using TMPro;
using UnityEngine;

public class Avvenimenti : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eventiText;

    private void OnEnable()
    {
        Raccolta.OnEventiChanged += ShowEventi;
    }

    private void OnDisable()
    {
        Raccolta.OnEventiChanged -= ShowEventi;
    }

    private void ShowEventi(string message)
    {
        if (eventiText != null)
            eventiText.text = message;
    }
}
