using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed;

    public bool canMove = true;
    private LSManager _lSMRef;

    public GameObject levelInfoPanel;
    public GameObject scoreTitleObject, scoreObject, lockIconObject;
    public TextMeshProUGUI levelTitle, levelHighScore;

    // Start is called before the first frame update
    void Start()
    {
        _lSMRef = GameObject.Find("LSManager").GetComponent<LSManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPoint.isLevel && Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f && !levelInfoPanel.activeInHierarchy)
        {
            levelInfoPanel.SetActive(true);

            if(currentPoint.levelToLoad == "Level-1")
            {
                scoreObject.SetActive(false);
                scoreTitleObject.SetActive(false);
                lockIconObject.SetActive(false);
                levelTitle.text = "Capítulo 1: Un Extraño Planeta";
            }

            else if(currentPoint.levelToLoad == "Level-2")
            {
                scoreObject.SetActive(true);
                scoreTitleObject.SetActive(true);
                levelTitle.text = "Capítulo 2: La Civilización Invadida";
                levelHighScore.text = GameManager.gMRef.highScoreLevelTwo.ToString();

                if(!GameManager.gMRef.levelOneClear)
                    lockIconObject.SetActive(true);
                else
                    lockIconObject.SetActive(false);

            }

            else if (currentPoint.levelToLoad == "Level-3")
            {
                scoreObject.SetActive(true);
                scoreTitleObject.SetActive(true);
                levelTitle.text = "Capítulo 3: ¿Seres Tetradimensionales?";
                levelHighScore.text = GameManager.gMRef.highScoreLevelThree.ToString();

                if (!GameManager.gMRef.levelTwoClear)
                    lockIconObject.SetActive(true);
                else
                    lockIconObject.SetActive(false);
            }

            else if( currentPoint.levelToLoad == "Boss")
            {
                scoreObject.SetActive(false);
                scoreTitleObject.SetActive(false);
                levelTitle.text = "Capítulo 4: Combate contra Nyuxh";

                if (!GameManager.gMRef.levelThreeClear)
                    lockIconObject.SetActive(true);
                else
                    lockIconObject.SetActive(false);
            }

            else if (currentPoint.levelToLoad == "Shop")
            {
                scoreObject.SetActive(false);
                scoreTitleObject.SetActive(false);
                levelTitle.text = "Taller de Johnny";

                if (!GameManager.gMRef.levelTwoClear)
                    lockIconObject.SetActive(true);
                else
                    lockIconObject.SetActive(false);
            }
        }
        else if (Vector3.Distance(transform.position, currentPoint.transform.position) > 0.1f && levelInfoPanel.activeInHierarchy)
        {
            levelInfoPanel.SetActive(false);
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (currentPoint.right != null)
                    {
                        SetNextPoint(currentPoint.right);
                    }
                }

                if (Input.GetAxisRaw("Horizontal") < -0.5f || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (currentPoint.left != null)
                    {
                        SetNextPoint(currentPoint.left);
                    }
                }

                if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (currentPoint.up != null)
                    {
                        SetNextPoint(currentPoint.up);
                    }
                }

                if (Input.GetAxisRaw("Vertical") < -0.5f || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (currentPoint.down != null)
                    {
                        SetNextPoint(currentPoint.down);
                    }
                }

                if (currentPoint.isLevel && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return)))
                {
                    _lSMRef.LoadLevel();
                }
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
    }
}
