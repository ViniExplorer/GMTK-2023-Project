using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float levelTime;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        levelTime = FindObjectOfType<GameManager>().levelTime;
        StartCoroutine(Timing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timing()
    {
        do
        {
            levelTime -= 1f;
            timeText.text = levelTime.ToString();
            yield return new WaitForSeconds(1f);
        } while (levelTime > 0f);
        FindObjectOfType<BurgerGenerator>().CheckAnswer();
    }
}
