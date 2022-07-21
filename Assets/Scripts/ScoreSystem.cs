using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //takes the TMPro namespace


public class ScoreSystem : MonoBehaviour
{
    private TextMeshProUGUI scoreTextDisplay; //refers to the TMPro component in the current gameObject
    public int scoreValue; //the score value

    void Awake() //called before Start()
    {
        EnemyHealth.Killed += RunCoroutine; //adds the RunCoroutine function to the Killed event
        scoreTextDisplay = GetComponent<TextMeshProUGUI>(); //gets the TMPro component from the current gameObject
    }

    void Start()
    {
        scoreValue = 0; //sets the score value to 0
    }

    void Update()
    {
        scoreTextDisplay.text = "Points: "+ scoreValue.ToString(); //sets the text to the score value
    }

    private IEnumerator ScoreIncrease() //pulse score UI when score increases
    {
        for (float i = 1f; i <= 1.2f; i += 0.05f) 
        {
            scoreTextDisplay.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreTextDisplay.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        scoreValue += 10; //increases the score value by 10

        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            scoreTextDisplay.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreTextDisplay.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void RunCoroutine() //runs the coroutine on seperate function to avoid exceptions
    {
        StartCoroutine(ScoreIncrease()); //starts the ScoreIncrease coroutine
    }

    private void OnDestroy() //unsubscribe from the EnemyDeath event
    {
        EnemyHealth.Killed -= RunCoroutine;
    }

}
