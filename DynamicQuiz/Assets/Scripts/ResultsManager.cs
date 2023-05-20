using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    [SerializeField]
    private Text trueText, falseText, pointText;

    [SerializeField]
    private GameObject firstStar, secondStar, thirdStar;
    public void Results(int trueValue, int falseValue , int point)
    {
        trueText.text = trueValue.ToString();
        falseText.text = falseValue.ToString();
        pointText.text = point.ToString();

        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);

        if (trueValue <=2 )
        {
            firstStar.SetActive(true);
        }
        else if (trueValue <= 4)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
        }
        else if (trueValue <= 6)
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
            thirdStar.SetActive(true);
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }


}
