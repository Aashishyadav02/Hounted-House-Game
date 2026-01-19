using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;
public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public UIDocument uiDocument;
    
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool m_HasAudioPlayed;
    
    //Timer varibles
    private float m_Demo_GameTimer = 0f;
    private bool m_Demo_GameTimerIsTicking = false;
    private Label m_Demo_GameTimerLabel;
    
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    

    private VisualElement m_EndScreen;
    private VisualElement m_CaughtScreen;

    void Start()
    {
        //timer 
        var uiDocument = GetComponent<UIDocument>();
        m_Demo_GameTimerLabel = uiDocument.rootVisualElement.Q<Label>("Demo_TimerLabel");
        m_Demo_GameTimer = 0.0f;
        m_Demo_GameTimerIsTicking = true;
        Demo_UpdateTimerLabel();
        
        m_EndScreen = uiDocument.rootVisualElement.Q<VisualElement>("EndScreen");
        m_CaughtScreen = uiDocument.rootVisualElement.Q<VisualElement>("CaughtScreen");
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    void Update ()
    { 
        //timer update function called
        if (m_Demo_GameTimerIsTicking)
        {
            m_Demo_GameTimer += Time.deltaTime;
            Demo_UpdateTimerLabel();
        }

        if (m_IsPlayerAtExit)
        {
            EndLevel (m_EndScreen, false,exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel (m_CaughtScreen, true,caughtAudio);
        }
    }

    void Demo_UpdateTimerLabel()
    {
        m_Demo_GameTimerLabel.text = m_Demo_GameTimer.ToString("0.00");
    }
    /*  void EndLevel (VisualElement element, bool doRestart,AudioSource audioSource)
    {      
        m_Timer += Time.deltaTime;
        element.style.opacity = m_Timer / fadeDuration;

        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene ("Main");
            }
            else
            {
                Application.Quit ();
                Time.timeScale = 0;
            }
        }
    }
    */
     

    void EndLevel(VisualElement element, bool doRestart, AudioSource audioSource)
    {
        m_Timer += Time.deltaTime;
        element.style.opacity = m_Timer / fadeDuration;

        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                // Restart after 1.5 sec
                StartCoroutine(RestartGameAfterDelay(1.5f));
            }
            else
            {
                // You can choose whether you want to quit here or also restart
                StartCoroutine(RestartGameAfterDelay(1.5f));
            }
        }
    }

    IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main");
    }

}
