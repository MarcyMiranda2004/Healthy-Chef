using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Question
{
    public string question;
    public string[] answers;
    public int correctIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("Riferimenti UI")]
    public TMP_Text DomandeText;
    public Button[] BottoneRisposta;
    public TMP_Text feedbackText;

    [Header("Dati")]
    public TextAsset txtFile;

    private List<Question> Domande = new List<Question>();
    private Question domandaCorrente;
    private int indiceDomanda = 0;
    private int risposteCorrette = 0; // Contatore risposte corrette

    void Start()
    {
        LoadQuestions();
        Domande = Domande.OrderBy(q => Random.value).ToList(); // Mescola le domande
        ShowNextQuestion();
    }

    void LoadQuestions()
    {
        string[] lines = txtFile.text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Trim().Split(';');
            if (parts.Length < 3) continue;

            Question q = new Question();
            q.question = parts[0];
            q.answers = new string[parts.Length - 2];
            for (int i = 1; i < parts.Length - 1; i++)
            {
                q.answers[i - 1] = parts[i];
            }

            int.TryParse(parts[parts.Length - 1], out q.correctIndex);
            Domande.Add(q);
        }
    }

    void ShowNextQuestion()
    {
        if (indiceDomanda >= Domande.Count)
        {
            DomandeText.text = $"Quiz completato!\nHai risposto correttamente a {risposteCorrette} su {Domande.Count}.";
            feedbackText.text = "";
            foreach (var btn in BottoneRisposta) btn.gameObject.SetActive(false);
            return;
        }

        domandaCorrente = Domande[indiceDomanda];
        DomandeText.text = domandaCorrente.question;

        // mescola risposte mantenendo la corretta
        int correctIndexOriginale = domandaCorrente.correctIndex;
        string[] risposteOriginali = domandaCorrente.answers;

        List<int> indici = new List<int>();
        for (int i = 0; i < risposteOriginali.Length; i++)
            indici.Add(i);

        for (int i = 0; i < indici.Count; i++)
        {
            int rnd = Random.Range(0, indici.Count);
            (indici[i], indici[rnd]) = (indici[rnd], indici[i]);
        }

        int nuovoIndiceCorretto = indici.IndexOf(correctIndexOriginale);

        for (int i = 0; i < BottoneRisposta.Length; i++)
        {
            if (i < risposteOriginali.Length)
            {
                BottoneRisposta[i].gameObject.SetActive(true);
                BottoneRisposta[i].GetComponentInChildren<TMP_Text>().text = risposteOriginali[indici[i]];

                int indiceSelezionato = i;
                BottoneRisposta[i].onClick.RemoveAllListeners();
                BottoneRisposta[i].onClick.AddListener(() => CheckAnswer(indiceSelezionato, nuovoIndiceCorretto));
            }
            else
            {
                BottoneRisposta[i].gameObject.SetActive(false);
            }
        }

        feedbackText.text = "";
        feedbackText.alpha = 0; // Reset trasparenza feedback
    }

    void CheckAnswer(int selectedIndex, int correctIndex)
    {
        if (selectedIndex == correctIndex)
        {
            feedbackText.text = "Corretto!";
            feedbackText.color = Color.green; // Colore verde per risposta corretta
            risposteCorrette++; // Incrementa punteggio
        }
        else
        {
            feedbackText.text = "Sbagliato!";
            feedbackText.color = Color.orange; // Colore rosso per risposta sbagliata
        }

        //avvia animazione di comparsa
        StopAllCoroutines();
        StartCoroutine(FadeInFeedback());

        indiceDomanda++;
        Invoke(nameof(ShowNextQuestion), 1.5f);
    }

    // Animazione di fade-in per il feedback
    IEnumerator FadeInFeedback()
    {
        float duration = 0.5f; // Durata animazione
        float elapsed = 0f;
        feedbackText.alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            feedbackText.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        feedbackText.alpha = 1f;
    }
}
