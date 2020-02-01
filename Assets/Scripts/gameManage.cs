using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManage : MonoBehaviour
{
    private static Queue<Action> battleQueue;
    [SerializeField] private GameObject attackObj;
    [SerializeField] private GameObject receiveObj;
    [SerializeField] AnimationClip moveAnimationClip;
    [SerializeField] AnimationClip attackAnimationClip;
    [SerializeField] AnimationClip receiveAnimationClip;
    [SerializeField] ParticleSystem moveParticle;
    public enum NowState
    {
        MoveSelect,
        Move,
        AttackSelect,
        Attack
    };
    public static void requestEnqueue(GameObject obj)
    {
        Debug.Log("ゲームマネージャー起動");
        switch (obj.GetComponent<interFace>().GetName())
        {
            case "Soldier":
                Debug.Log("分岐");
                List<(int, int)> area = obj.GetComponent<interFace>().Movable();
                foreach((int,int)T in area)
                {
                    Debug.Log("送信");
                    var (i, j) = T;
                    GameObject obj1 = GameObject.Find("Grid(" + i + "," + j + ")");
                    Debug.Log("送信2");
                    Debug.Log(i);
                    Debug.Log(j);
                    obj1.GetComponent<clickReceiver>().Flash();

                }
                break;
            default:
                break;
        }
    }

    void Enqueue(GameObject obj,AnimationClip animationClip)
    {
        Action action1 = new Action()
        {
            p = new Performance { chara = attackObj, animationClip = animationClip, particle = moveParticle }
        };
        battleQueue.Enqueue(action1);
    }
    void Start()
    {
        battleQueue = new Queue<Action>();
        while (!isGameEnd()) { 
    /*
        Action action4 = new Action()
        {
            d = new Damage { attackChara = attackObj, receiveChara = receiveObj, mp_use = 10, damage = 30, abnormalState = AbnormalState.Poison }
        };
        battleQueue.Enqueue(action4);
        */
        StartCoroutine(ActionCoroutine());
    }
    }

    IEnumerator ActionCoroutine()
    {
        Debug.Log(battleQueue.Count);
        while (battleQueue.Count > 0)
        {
            Action action = battleQueue.Dequeue();
            Debug.Log(action);
            if (action.p != null) { action.p.Method(); yield return new WaitForSeconds(2); }
            if (action.d != null) { action.d.Method(); yield return new WaitForSeconds(2); }

        }
    }
    private bool isGameEnd()
    {
        return true;
    }
    public struct Action
    {
        public Performance p;
        public Damage d;
    }

    public class Performance
    {
        public GameObject chara;
        public AnimationClip animationClip;
        public ParticleSystem particle;

        public void Method()
        {
            animationClip.legacy = true;
            chara.GetComponent<Animation>().AddClip(animationClip, "Play");
            chara.GetComponent<Animation>().Play("Play");
            ParticleSystem tempParticle = Instantiate(particle) as ParticleSystem;
            tempParticle.transform.parent = chara.transform;
            tempParticle.Play();
        }
    }

    public class Damage
    {
        public GameObject attackChara;
        public GameObject receiveChara;
        public int mp_use;
        public int damage;

        public void Method()
        {
            /*attackChara.GetComponent<CharaStatus>().mpSet -= mp_use;
            receiveChara.GetComponent<CharaStatus>().hpSet -= damage;
            if (abnormalState != AbnormalState.None)
            {
                List<AbnormalState> temp = receiveChara.GetComponent<CharaStatus>().abnormalSet;
                temp.Add(abnormalState);
                receiveChara.GetComponent<CharaStatus>().abnormalSet = temp;
            }
            */
        }
    }
}
