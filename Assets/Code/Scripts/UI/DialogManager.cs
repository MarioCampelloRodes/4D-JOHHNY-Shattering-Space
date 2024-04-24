using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder usar los elementos de la UI
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    //La caja de texto para el diálogo
    public TextMeshProUGUI dialogText;
    //El nombre del personaje
    public TextMeshProUGUI dialogCharName;
    //El retrato del personaje que habla en ese momento
    public Image portrait;
    //Referencia a la caja de diálogos
    public GameObject dialogBox;
    //Líneas del diálogo
    public string[] dialogLines;
    //La línea actual de diálogo
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
        //Inicializamos el Singleton si está vacío
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
        //Si el cuadro de diálogo está activo
        if(dialogBox.activeInHierarchy)
        {
            //Al pulsar la tecla X
            if ((Input.GetButtonUp("Fire1") || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && Time.timeScale == 1f)
            {
                //Vamos a la siguiente línea de diálogo
                currentLine++;

                //Si se ha terminado el diálogo
                if (currentLine >= dialogLines.Length)
                {
                    //Activa el Boss
                    if (activatesBossBattle)
                    {
                        bossBattle.SetActive(true);

                        bossLifeBar.SetActive(true);

                        AudioManager.aMRef.PlayBossMusic();
                    }
                    //Desactivamos el cuadro de diálogo
                    dialogBox.SetActive(false);
                    //Permitimos que el jugador se mueva de nuevo
                    if (SceneManager.GetActiveScene().name == "Level-1")
                        _jCRef.isInDialogue = false;
                    else
                        _iCRef.isInDialogue = false;
                }
                //Si el diálogo aún no ha terminado
                else
                {
                    //Comprobamos si hay un cambio de personaje en el diálogo
                    CheckIfName(sNpc);
                    //Muestra la línea de diálogo actual
                    dialogText.text = dialogLines[currentLine];
                }
            }
        }
    }

    //Método que muestra el diálogo pasado por parámetro
    public void ShowDialog(string[] newLines, Sprite theSNpc)
    {
        //El contenido de las líneas de diálogo del Manager pasa a ser el de las líneas de diálogo tras haber activado un diálogo
        dialogLines = newLines;
        //Vamos a la primera línea de diálogo
        currentLine = 0;
        //Asignamos el Sprite del NPC
        sNpc = theSNpc;
        //Comprobamos si hay un cambio de personaje en el diálogo
        CheckIfName(sNpc);
        //Muestro la línea de diálogo actual
        dialogText.text = dialogLines[currentLine];

        dialogCharName.text = charName;
        //Activamos el cuadro de diálogo
        dialogBox.SetActive(true);
        //Hacemos que el jugador no se pueda mover
        if (SceneManager.GetActiveScene().name == "Level-1")
            _jCRef.isInDialogue = true;
        else
            _iCRef.isInDialogue = true;
    }

    //Método para conocer si hay un cambio de personaje en el diálogo
    public void CheckIfName(Sprite theSNpc)
    {
        //Si la línea empieza por n-
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

            //Salto a la siguiente línea de diálogo
            currentLine++;
        }
    }
}
