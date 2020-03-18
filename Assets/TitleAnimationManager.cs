using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TitleAnimationManager : MonoBehaviour
{
    public GameObject movingObject;
    public GameObject endGhost;
    private Sprite usingMove0;
    private Sprite usingMove1;
    private Sprite move0;
    private Sprite move1;
    private Sprite move2;
    private Sprite move3;
    public int dl = 50;
    public float moveSpeed = 100;
    private Vector3 startPosition;
    public bool moveVector = true; //右から左がtrue
    public int subStartdl = 5;
    private int rndint = 0;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        rndint = Random.Range(0,5);
        Debug.Log(rndint);
        startPosition = movingObject.transform.position;
        move0 = movingObject.transform.GetChild(0).GetComponent<Image>().sprite;
        move1 = movingObject.transform.GetChild(1).GetComponent<Image>().sprite;
        //move2 = movingObject.transform.GetChild(2).GetComponent<Image>().sprite;
        //move3 = movingObject.transform.GetChild(3).GetComponent<Image>().sprite;
        usingMove0 = move0;
        usingMove1 = move1;
        MovingAnime();
    }

    private async void SubStart()
    {
        await Task.Delay(subStartdl*1000);
        movingObject.transform.position = startPosition;
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
            await Task.Delay(dl);
            movingObject.GetComponent<Image>().sprite = usingMove1;
            await Task.Delay(dl);
        }
    }
}
