using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface interFace /* CharaParentにかく関数は全部ここに書く。cの ~.hみたいなもん*/
{
    void Init(int x, int y,bool team);
    bool IsTeam();
    int ShowHP();
    void Move(int i,int j);
    void AttackSound();
    void AddDamage(int damage);
    string GetName();
    void Erase();
    int Power();
    int Evaluation(int x, int y);
    void AttackImage();
    int AtcEvaluation();
    void DamageImage();
    List<(int, int)> AIMovable();
    List<(int, int)> Movable();
    List<(int, int)> Attackable();

}