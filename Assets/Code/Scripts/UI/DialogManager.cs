using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder usar los elementos de la UI
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    //La caja de texto para el di�logo
    public TextMeshProUGUI dialogText;
    //El nombre del personaje
    public TextMeshProUGUI dialogCharName;
    //El retrato del personaje que habla en ese momento
    public Image portrait;
    //Referencia a la caja de di�logos
    public GameObject dialogBox;
    //L�neas del di�logo
    public string[] dialogLines;
    //La l�nea actual de di�logo
    public int currentLine;
    //Nombre del personaje que habla en ese momento
    private string charName;
    //El sprite del NPC
    private Sprite sNpc;

    private IkalController _iCRef;
    private JohnnyController _jCRef;

    public bool activatesBossBattle;
    public GameObject bossBattle;
    public GameObject bossLifeBar;

    //Hacemos una referencia (Singleton)
    public static DialogManager instance;
    private void Awake()
    {
        //Inicializamos el Singleton si est� vac�o
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level-1")
            _jCRef = GameObject.FindWithTag("Player").GetComponent<JohnnyController>();
        else
            _iCRef = GameObject.FindWithTag("Player").GetComponent<IkalController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si el cuadro de di�logo est� activo
        if(dialogBox.activeInHierarchy)
        {
            //Al pulsar la tecla X
            if ((Input.GetButtonUp("Fire1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && Time.timeScale == 1f)
            {
                //Vamos a la siguiente l�nea de di�logo
                currentLine++;

                //Si se ha terminado el di�logo
                if (currentLine >= dialogLines.Length)
                {
                    //Activa el Boss
                    if (activatesBossBattle)
                    {
                        bossBattle.SetActive(true);

                        bossLifeBar.SetActive(true);

                        AudioManager.aMRef.PlayBossMusic();
                    }
                    //Desactivamos el cuadro de di�logo
                    dialogBox.SetActive(false);
                    //Permitimos que el jugador se mueva de nuevo
                    if (SceneManager.GetActiveScene().name == "Level-1")
                        _jCRef.isInDialogue = false;
                    else
                        _iCRef.isInDialogue = false;
                }
                //Si el di�logo a�n no ha terminado
                else
                {
                    //Comprobamos si hay un cambio de personaje en el di�logo
                    CheckIfName(sNpc);
                    //Muestra la l�nea de di�logo actual
                    dialogText.text = dialogLines[currentLine];
                }
            }
        }
    }

    //M�todo que muestra el di�logo pasado por par�metro
    public void ShowDialog(string[] newLines, Sprite theSNpc)
    {
        //El contenido de las l�neas de di�logo del Manager pasa a ser el de las l�neas de di�logo tras haber activado un di�logo
        dialogLines = newLines;
        //Vamos a la primera l�nea de di�logo
        currentLine = 0;
        //Asignamos el Sprite del NPC
        sNpc = theSNpc;
        //Comprobamos si hay un cambio de personaje en el di�logo
        CheckIfName(sNpc);
        //Muestro la l�nea de di�logo actual
        dialogText.text = dialogLines[currentLine];

        dialogCharName.text = charName;
        //Activamos el cuadro de di�logo
        dialogBox.SetActive(true);
        //Hacemos que el jugador no se pueda mover
        if (SceneManager.GetActiveScene().name == "Level-1")
            _jCRef.isInDialogue = true;
        else
            _iCRef.isInDialogue = true;
    }

    //M�todo para conocer si hay un cambio de personaje en el di�logo
    public void CheckIfName(Sprite theSNpc)
    {
        //Si la l�nea empieza por n-
        if(dialogLines[currentLine].StartsWith("n-"))
        {
            //Obtenemos el nombre del personaje que habla en ese momento
            charName = dialogLines[currentLine].Replace("n-", "");
            //Si es distinto de los nombres de los personajes principales
            if (charName != "Ikal")
                //Ponemos el sprite del npc en concreto
                portrait.sprite = theSNpc;
            //Si es el nombre de un personaje principal
            else
                //Ponemos el sprite de ese personaje
                portrait.sprite = _iCRef.thePlayerSprite;

            //Salto a la siguiente l�nea de di�logo
            currentLine++;
        }
    }
}
