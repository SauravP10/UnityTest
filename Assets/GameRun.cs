using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ReadSpeaker;

public class GameRun : MonoBehaviour
{
    public RuntimeAnimatorController firstAnim;
    public RuntimeAnimatorController lastAnim;
    public GameObject gameObj;

    public Image[] answers;
    public Sprite[] answersSprites;
    public Image question;
    public Button nextBtn;
    string[] digits = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    int d = 0;

    public static int qIndex;
    string actionSpriteName;
    public Text answerText;

    public TTSSpeaker speaker;
    public float delay;


    // Start is called before the first frame update
    void Start()
    {
        TTS.Init();
        TTS.Say("Find the number on left from the given selection!" , speaker);
        qIndex = 1;
        answerText.text = "";
        nextBtn.interactable = false;
        gameObj.GetComponent<Animator>().runtimeAnimatorController = firstAnim;
        setSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextButton() {
        answerText.text = "";

        if(qIndex < 9)
        {
            ++qIndex;
        }
        else
        {
            qIndex = 1;
        }
        
        gameObj.GetComponent<Animator>().runtimeAnimatorController = lastAnim;
        StartCoroutine(startAnim());

        if (d < 8)
        {
            ++d;
        }
        else
        {
            d=0;
        }
    }

    public IEnumerator startAnim()
    {
        yield return new WaitForSeconds(0.5f);
        gameObj.GetComponent<Animator>().runtimeAnimatorController = firstAnim;
        setSprite();
        answerText.text = "";
        nextBtn.interactable = false;
    }

    public void setSprite()
    {
        question.GetComponent<Image>().sprite = answersSprites[qIndex-1];
        for(int i = 0; i < 9; i++) {
            answers[i].GetComponent<Image>().sprite = answersSprites[i];
        }
    }

    public void correctAns()
    {
        string questionName = question.sprite.name;
        if (questionName.Equals(actionSpriteName))
        {
            answerText.text = "Correct Answer!";
            nextBtn.interactable = true;
            TTS.Say("Correct Answer. It is pronounced, " + digits[d], speaker);
            
        }
        else 
        {
            answerText.text = "Wrong Answer!";
            TTS.Say("Wrong Answer, Try Again", speaker);
        }
    }

    public void answerSpriteAction(Image image)
    {
        actionSpriteName = image.sprite.name;
        correctAns();
    }

    public void closeBtn()
    {
        SceneManager.LoadScene(0);
    }
}
