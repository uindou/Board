using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidyBoard : MonoBehaviour
{
    private GameObject grid00;
    private GameObject grid01;
    private GameObject grid10;
    private GameObject ememyArea;
    private GameObject nutralArea;
    private GameObject playerArea;
    private float deltaX;
    private float deltaY;
    // Start is called before the first frame update
    void Start()
    {
        grid00 = this.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject;
        grid01 = this.transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).gameObject;
        grid10 = this.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).gameObject;

        deltaX = grid01.transform.position.x - grid00.transform.position.x;
        deltaY = grid10.transform.position.y - grid00.transform.position.y;

        ememyArea = this.transform.GetChild(0).gameObject;
        nutralArea = this.transform.GetChild(1).gameObject;
        playerArea = this.transform.GetChild(2).gameObject;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                this.transform.GetChild(3).transform.GetChild(i).transform.GetChild(j).gameObject.transform.position
                    = new Vector3(grid00.transform.position.x + deltaX * j, grid00.transform.position.y + deltaY * i, 0); ;

                if (i == 0 && j == 3)
                {
                    ememyArea.transform.position = this.transform.GetChild(3).transform.GetChild(i).transform.GetChild(j).gameObject.transform.position;
                }

                else if (i == 4 && j == 3) {
                    nutralArea.transform.position = this.transform.GetChild(3).transform.GetChild(i).transform.GetChild(j).gameObject.transform.position;
                }

                else if (i == 8 && j == 3)
                {
                    playerArea.transform.position = this.transform.GetChild(3).transform.GetChild(i).transform.GetChild(j).gameObject.transform.position;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
