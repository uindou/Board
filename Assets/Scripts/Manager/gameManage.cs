using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataBase;
using static gameManage;
using static myAI;
using static InterstitialManager;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class gameManage : MonoBehaviour
{
    [SerializeField]State state;
    public static situation receiveMode;
    public static bool moveReset;
    public static bool attackReset;
    public static bool turn;
    public static bool selectFlag;
    public static bool attackFlag;
    public static bool AIWaiting;
    public static bool endWaiting;
    public static bool aiPlaying;

    public enum situation
    {
        select,
        move,
        attackselect,
        attack,
        free
    }
    public static void Skip()
    {
        switch (receiveMode)
        {
            case situation.select:
                selectFlag = true;
                receiveMode = situation.attackselect;
                break;
            case situation.move:
                selectFlag = true;
                receiveMode = situation.attackselect;
                var (i3, j3) = DataBase.selectMove;
                GameObject obj1 = DataBase.objs[i3, j3];
                gameManage.FlashControl(obj1, false);
                break;
            case situation.attackselect:
                attackFlag = true;
                receiveMode = situation.select;
                break;
            case situation.attack:
                attackFlag = true;
                receiveMode = situation.select;
                var (i4, j4) = DataBase.attackSelect;
                GameObject obj2 = DataBase.objs[i4, j4];
                gameManage.AttackFlash(obj2, false);
                break;
            default:
                break;
        }
    }
    public static async void WaitTime()
    {
        await Task.Delay(500);
        endWaiting = true;
    }
    public static async void AIWait()
    {
        await Task.Delay(0);
        AIWaiting = true;
        
    }
    public static void requestEnqueue(GameObject obj)
    {
        switch (receiveMode)
        {
            case situation.select:
                if (obj.GetComponent<interFace>() != null)
                    
                {
                    if(obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Request(s, t, DataBase.situation.select);
                    }
                    
                }
                break;

            case situation.move:
                if(obj.GetComponent<interFace>() != null)
                {
                    if(obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int i3, j3;
                        (i3, j3) = DataBase.selectMove;
                        GameObject obj1 = DataBase.objs[i3, j3];
                        FlashControl(obj1, false);
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Reset(DataBase.situation.select);
                        DataBase.Request(s, t, DataBase.situation.select);
                        moveReset = true;
                    }
                    break;

                }
                else if (obj.GetComponent<clickReceiver>().IsRange())
                {
                    int s, t;
                    (s, t) = DataBase.objSearch(obj);
                    DataBase.Request(s, t, DataBase.situation.move);
                }
                break;

            case situation.attackselect:
                if (obj.GetComponent<interFace>() != null)
                {
                    if (obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Request(s, t, DataBase.situation.attackselect);
                    }    
                }
                break;

            case situation.attack:
                if (obj.GetComponent<interFace>() != null)
                {
                    if (obj.GetComponent<interFace>().IsTeam() == turn)
                    {
                        int i3, j3;
                        (i3, j3) = DataBase.attackSelect;
                        GameObject obj1 = DataBase.objs[i3, j3];
                        AttackFlash(obj1, false);
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Reset(DataBase.situation.attackselect);
                        DataBase.Request(s, t, DataBase.situation.attackselect);
                        attackReset = true;
                    }

                    else if (obj.GetComponent<clickReceiver>().IsRange())
                    {
                        int s, t;
                        (s, t) = DataBase.objSearch(obj);
                        DataBase.Request(s, t, DataBase.situation.attack);
                    }
                    break;
                }
                break;

            default:
                break;


        }
    }
    public static void FlashControl(GameObject obj,bool isflash)
    {
        
        List<(int, int)> area = obj.GetComponent<interFace>().Movable();
        if (area == null)
        {
            return;
        }
        if (isflash)
        {
            obj.GetComponent<Image>().color = new Color32(90,177,222,255);
        }
        else
        {
            obj.GetComponent<Image>().color = Color.white;
        }
        foreach ((int, int) T in area)
        {
            var (i2, j2) = T;
            GameObject obj1 = DataBase.objs[i2, j2];
            if (isflash)
            {
                obj1.GetComponent<clickReceiver>().Flash();
            }
            else
            {
                obj1.GetComponent<clickReceiver>().StopFlash();
            }
        }
    }
    public static void AttackFlash(GameObject obj,bool isflash)
    {
        List<(int, int)> area = obj.GetComponent<interFace>().Attackable();
        if (isflash)
        {
            obj.GetComponent<Image>().color = new Color32(221, 101,90, 255);
        }
        else
        {
            obj.GetComponent<Image>().color = Color.white;
        }
        foreach ((int, int) T in area)
        {
            var (i2, j2) = T;
            GameObject obj1 = DataBase.objs[i2, j2];
            if (obj1.GetComponent<interFace>() != null)
            {
                if (obj1.GetComponent<interFace>().IsTeam() != turn)
                {
                    if (isflash)
                    {
                        obj1.GetComponent<clickReceiver>().Flash();
                    }
                    else
                    {
                        obj1.GetComponent<clickReceiver>().StopFlash();
                    }
                }
            }
        }
    }
    
    private void Start()
    {
        state = new Free();
        receiveMode = situation.select;
        turn = false;
        aiPlaying = false;
        
    }
    private void Update()
    {
        state = state.Execute();
    }
}

public interface State
{
    State Execute();
}

public class Start : State
{
    public State Execute()
    {
        gameManage.selectFlag = false;
        gameManage.attackFlag = false;
        DataBase.FlugInit();
        DataBase.TurnChange();
        receiveMode = gameManage.situation.select;
        DataBase.PhaseChange(true);
        return new Select();
    }
}

public class Select : State
{
    public State Execute()
    {
        if (selectFlag)
        {
            if (DataBase.CantAttack(gameManage.turn))
            {
                return new Final();
            }
            else
            {
                DataBase.PhaseChange(false);
                return new AttackSelect();
            }
        }
        else if (DataBase.selectFlug)
        {
            int i, j;
            (i, j) = DataBase.selectMove;
            GameObject obj = DataBase.objs[i, j];
            gameManage.FlashControl(obj, true);
            gameManage.receiveMode = gameManage.situation.move;
            return new Move();
        }
        else
        {
            return this;
        }
    }
}

public class Move:State{
    public State Execute()
    {
        if (gameManage.selectFlag)
        {
            if (DataBase.CantAttack(gameManage.turn)) {
                endWaiting = false;
                gameManage.WaitTime();
                return new Final();
            }
            else
            {
                DataBase.PhaseChange(false);
                return new AttackSelect();
            }
        }
        else if (DataBase.moveFlug)
        {
            int i3, j3;
            int i4, j4;
            (i3, j3) = DataBase.selectMove;
            (i4, j4) = DataBase.realMove;
            GameObject obj = DataBase.objs[i4, j4];
            GameObject obj1 = DataBase.objs[i3, j3];
            gameManage.FlashControl(obj1, false);

            Vector3 c = obj.transform.position;
            obj.transform.position = obj1.transform.position;
            obj1.transform.position = c;
            DataBase.objs[i3, j3] = obj;
            DataBase.objs[i4, j4] = obj1;
            obj1.GetComponent<interFace>().Move(i4,j4);
            int teamNum = obj1.GetComponent<interFace>().IsTeam() ? 2 : 1;
            DataBase.Set(i4, j4, teamNum);
            DataBase.Set(i3, j3, 0);
            if (DataBase.GameOver(!gameManage.turn))
            {
                endWaiting = false;
                gameManage.WaitTime();
                return new Final();
            }else if (DataBase.CantAttack(gameManage.turn))
            {
                endWaiting = false;
                gameManage.WaitTime();
                return new Final();
            }
            else
            {
                gameManage.receiveMode = gameManage.situation.attackselect;
                DataBase.PhaseChange(false);
                return new AttackSelect();
            }
        }else if(gameManage.moveReset){
            gameManage.moveReset = false;
            return new Select();
        }
        else
        {
            return this;
        }
    }
}
public class AttackSelect : State
{
    public State Execute()
    {
        if (attackFlag)
        {
            endWaiting = false;
            gameManage.WaitTime();
            return new Final();
        }
        else if (DataBase.attackSelectFlug)
        {
            int i3, j3;
            (i3, j3) = DataBase.attackSelect;
            GameObject obj1 = DataBase.objs[i3, j3];
            AttackFlash(obj1, true);
            gameManage.receiveMode = gameManage.situation.attack;
            return new Attack();
        }
        else
        {
            return this;
        }
    }
}
public class Attack : State
{
    public State Execute()
    {
        if (attackFlag)
        {
            endWaiting = false;
            gameManage.WaitTime();
            return new Final();
        }
        else if (DataBase.attackFlug)
        {
            int i3, j3;
            int i4, j4;
            (i3, j3) = DataBase.attackSelect;
            (i4, j4) = DataBase.realAttack;
            GameObject obj = DataBase.objs[i4, j4];
            GameObject obj1 = DataBase.objs[i3, j3];
            gameManage.AttackFlash(obj1, false);
            obj1.GetComponent<interFace>().AttackImage();
            obj1.GetComponent<interFace>().AttackSound();
            obj.GetComponent<interFace>().AddDamage(obj1.GetComponent<interFace>().Power());

            gameManage.receiveMode = gameManage.situation.free;
            endWaiting = false;
            gameManage.WaitTime();
            return new Final();
        }
        else if (gameManage.attackReset)
        {
            gameManage.attackReset = false;
            return new AttackSelect();
        }
        else
        {
            return this;
        }
    }
}
public class Final : State
{
    
    public State Execute()
    {
        if (!endWaiting) return this;

        if (DataBase.GameOver(!gameManage.turn) || DataBase.NoKoma(gameManage.turn))
        {
            gameManage.receiveMode = gameManage.situation.free;
            DataBase.GameEnd(!gameManage.turn);
            
            return new PreEnd();
        }
        else
        {
            
            if (DataBase.AImode && !turn)
            {
                aiPlaying = true;
                AIWaiting = false;
                gameManage.AIWait();
                return new AI();//2ターンに一度呼ばれる
            }
            else
            {
                gameManage.turn = !gameManage.turn;
                aiPlaying = false;
                GameObject obj = objs[0, 0];
                if (DataBase.AImode) obj.GetComponent<clickReceiver>().ChangeAct();
                gameManage.receiveMode = gameManage.situation.select;
                return new Start();
            }
        }
    }
    
}
public class PreEnd : State
{
    public State Execute()
    {

        if (!endWaiting) return this;
        else
        {
            DataBase.winner = gameManage.turn;
            DataBase.preStage = DataBase.SceneName;
            switch (DataBase.SceneName)
            {
                case "Stage1":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin=0;
                    break;
                case "Stage2":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin=0;
                    break;
                case "Stage3":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin = 0;
                    break;
                case "Stage4":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin = 0;
                    break;
                case "Stage5":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin = 0;
                    break;
                case "Stage6":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin = 0;
                    break;
                case "Stage7":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin = 0;
                    break;
                case "Stage8":
                    ResultManager.StageName = "Game";
                    DataBase.bonusCoin = 0;
                    break;
                case "AIStage1":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage2":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage3":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage4":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage5":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage6":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage7":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                case "AIStage8":
                    ResultManager.StageName = "AIStage";
                    DataBase.bonusCoin = 20;
                    break;
                default:
                    
                    DataBase.bonusCoin = 0;
                    break;
            }
            
            DataBase.SceneName = "";
            SceneManager.LoadScene("Win");
            
            InterstitialManager.GameOver();
            return new End();
        }
    }
}
public class End : State
{
    public State Execute()
    {
        return this;
    }
}
public class AI : State
{
    public State Execute()
    {
        if (!AIWaiting) return this;
        else
        {
            GameObject obj = objs[0, 0];
            obj.GetComponent<clickReceiver>().ChangeAct();//クリックレシーバーのモードを変えるための処理
            if (DataBase.EndWarn())
            {
                gameManage.turn = !gameManage.turn;
                Debug.Log("割り込み");
                myAI.StartAI(5);//割り込み
            }
            else
            {
                gameManage.turn = !gameManage.turn;
                Debug.Log("通常モード");
                myAI.StartAI(6);
            }
            return new Start();
        }
    }
}

public class Free : State
{
    public State Execute()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Game":
                return new Start();
            default:
                return this;
        }
    }
}
