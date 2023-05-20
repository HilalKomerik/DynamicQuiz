using System.Collections;
using System.Collections.Generic;
using System.Linq; // *
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unanswered;

    private Question validQuestion;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text trueReply, falseReply;

    [SerializeField]
    private GameObject trueBtn, falseBtn;

    int trueValue, falseValue;

    int totalValue;

    [SerializeField]
    private GameObject resultsPanel;

    ResultsManager resultsManager;
    void Start()
    {
        if (unanswered == null || unanswered.Count == 0)
        {
            unanswered = questions.ToList<Question>(); //Listeye ekle.
        }
        trueValue = 0;
        falseValue = 0;
        totalValue = 0;
        RandomQuestion();
    }

    void RandomQuestion()
    {
        falseBtn.GetComponent<RectTransform>().DOLocalMoveX(320f, 0.1f);
        trueBtn.GetComponent<RectTransform>().DOLocalMoveX(-320f, 0.1f);

        int randomQuestionIndex = Random.Range(0, unanswered.Count);
        validQuestion = unanswered[randomQuestionIndex];

        questionText.text = validQuestion.question;

        if (validQuestion.itTrue)
        {
            trueReply.text = "DO�RU CEVAPLADINIZ";
            falseReply.text = validQuestion.questionReply;
        }
        else
        {
            trueReply.text = validQuestion.questionReply;
            falseReply.text = "DO�RU CEVAPLADINIZ";
        }
    }


    IEnumerator WaitBetweenQuestionsRoutine()
    {
        unanswered.Remove(validQuestion); // o anki soruyu siler.

        yield return new WaitForSeconds(1.5f);

        if (unanswered.Count<=0)
        {
            resultsPanel.SetActive(true);

            resultsManager = Object.FindObjectOfType<ResultsManager>();
            resultsManager.Results(trueValue, falseValue, totalValue); 
        }
        else
        {
            RandomQuestion();
        }
    }

    public void TrueButton()
    {
        if (validQuestion.itTrue)
        {
            trueValue++;
            totalValue += 100;
        }
        else
        {
            falseValue++;
        }
        falseBtn.GetComponent<RectTransform>().DOLocalMoveX(1000f, 1.5f);
        StartCoroutine(WaitBetweenQuestionsRoutine());
    }

    public void FalseButton()
    {
        if (!validQuestion.itTrue)
        {
            trueValue++;
            totalValue += 100;
        }
        else
        {
            falseValue++;
        }
        trueBtn.GetComponent<RectTransform>().DOLocalMoveX(-1000f, 1.5f);
        StartCoroutine(WaitBetweenQuestionsRoutine());
    }
}
