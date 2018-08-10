//Attach this script to a GameObject.
//Create a Text GameObject (Create>UI>Text) and attach it to the My Text field in the Inspector of your GameObject
//Press the space bar in Play Mode to see the Text change.

using UnityEngine;
using UnityEngine.UI;

public class ChangeTextExample : MonoBehaviour
{
    public Text m_MyText;
    float timeLeft = 3f;
    void Start()
    {
        //Text sets your text to say this message
        m_MyText.text = "Press Space To Change Text";
       
    }

    void Update()
    {
        timeLeft -=  Time.deltaTime;
       
        //Press the space key to change the Text message
        if (Input.GetKey(KeyCode.Space))
        {
            timeLeft = 3f;
            m_MyText.text = "Text has changed.";
        }

        if (timeLeft <= 0f)
        {
            m_MyText.text = "Press Space to Change Text";
        }

    }
}