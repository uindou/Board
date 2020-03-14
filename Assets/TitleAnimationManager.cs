using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TitleAnimationManager : MonoBehaviour
{
    public GameObject movingObject;
    public GameObject endGhost;
    private Sprite Move0;
    private Sprite Move1;
    public int dl = 50;
    public float moveSpeed = 100;
    private Vector3 startPosition;
    public bool moveVector = true;
    public int subStartdl = 5; 
    // Start is called before the first frame update
    void Start()
    {
        startPosition = movingObject.transform.position;
        Move0 = movingObject.transform.GetChild(0).GetComponent<Image>().sprite;
        Move1 = movingObject.transform.GetChild(1).GetComponent<Image>().sprite;
        MovingAnime();
    }

    private void SubStart()
    {
        movingObject.gameObject.SetActive(true);
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
                movingObject.gameObject.SetActive(false);
                Invoke("SubStart", subStartdl);
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
                movingObject.gameObject.SetActive(false);
                Invoke("SubStart", subStartdl);
            }
        }
    }

    public async void MovingAnime()
    {
        while (true)
        {
            movingObject.GetComponent<Image>().sprite = Move0;
            await Task.Delay(dl);
            movingObject.GetComponent<Image>().sprite = Move1;
            await Task.Delay(dl);
        }
    }

    private void OnDisable()
    {
        movingObject.transform.position = startPosition;
    }
}
