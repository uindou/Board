using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TitleAnimationManager : MonoBehaviour
{
    public GameObject movingObject;
    public GameObject effectLayer;
    public GameObject pairMovingObject;
    public GameObject endGhost;

    public AudioSource attackSE;

    private Sprite usingMove0;
    private Sprite usingMove1;
    
    private Sprite move0;
    private Sprite move1;
    
    private Sprite attack0;
    private Sprite attack1;
    private Sprite attack2;
    private Sprite attack3;
    
    private Sprite transparent;
    
    private bool stoptrigger;
    
    public int movedl = 50;
    public int atcdl = 50;
    public float moveSpeed = 100;
    
    private Vector3 startPosition;
    
    public bool moveVector = true; //右から左がtrue
    
    // Start is called before the first frame update
    void Start()
    {
        stoptrigger = false;
        startPosition = movingObject.transform.position;

        move0 = movingObject.transform.GetChild(0).GetComponent<Image>().sprite;
        move1 = movingObject.transform.GetChild(1).GetComponent<Image>().sprite;

        attack0= effectLayer.transform.GetChild(0).GetComponent<Image>().sprite;
        attack1 = effectLayer.transform.GetChild(1).GetComponent<Image>().sprite;
        attack2 = effectLayer.transform.GetChild(2).GetComponent<Image>().sprite;
        attack3 = effectLayer.transform.GetChild(3).GetComponent<Image>().sprite;
        
        transparent = effectLayer.transform.GetChild(4).GetComponent<Image>().sprite;

        usingMove0 = move0;
        usingMove1 = move1;

        MovingAnime();
    }

    private void SubStart()
    {
        movingObject.transform.position = startPosition;
        pairMovingObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveVector)
        {
            if (movingObject.transform.position.x >= endGhost.transform.position.x)
            {
                movingObject.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                SubStart();
            }
        }
        else
        {
            if (movingObject.transform.position.x <= endGhost.transform.position.x)
            {
                movingObject.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                SubStart();
            }
        }
    }

    public async void MovingAnime()
    {
        Debug.Log("呼ばれた");
        while (true)
        {
            movingObject.GetComponent<Image>().sprite = usingMove0;
            await Task.Delay(movedl);
            movingObject.GetComponent<Image>().sprite = usingMove1;
            await Task.Delay(movedl);
            if (stoptrigger) break;
        }
    }

    private void OnDisable()
    {
        stoptrigger = true;
    }

    private void OnEnable()
    {
        stoptrigger = false;
        MovingAnime();
    }

    public async void OnClick()
    {
        attackSE.Play();

        effectLayer.GetComponent<Image>().sprite = attack0;
        await Task.Delay(atcdl);
        effectLayer.GetComponent<Image>().sprite = attack1;
        await Task.Delay(atcdl);
        effectLayer.GetComponent<Image>().sprite = attack2;
        await Task.Delay(atcdl);
        effectLayer.GetComponent<Image>().sprite = attack3;
        await Task.Delay(atcdl);
        effectLayer.GetComponent<Image>().sprite = transparent;
    }
}
