using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        turk
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; //janela do dialogo
    public Image profileSprite; //sprite do perfil
    public Text speechText; //texto da fala
    public Text actorNameText; //nome do npc

    [Header("Settings")]
    public float typingSpeed; //velocidade da fala

    //variaveis de controle
    private bool _isShowing; //ver se a janela esta visivel
    private int index; //contar sentencas
    private string[] _sentences;

    public static DialogueControl instance;

    public bool isShowing
    {
        get { return _isShowing; }
        set { _isShowing = value; }
    }

    public string[] sentences
    {
        get { return _sentences; }
        set { _sentences = value; }
    }

    //awake e chamado antes de todos os Start() na hierarquia de execucao de scripts
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in _sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == _sentences[index])
        {
            if (index < _sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //quando terminam os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                _sentences = null;
                _isShowing = false;
            }
        }
    }

    public void Speech(string[] txt)
    {
        if(!_isShowing)
        {
            dialogueObj.SetActive(true);
            _sentences = txt;
            StartCoroutine(TypeSentence());
            _isShowing = true;
        }
    }
}
