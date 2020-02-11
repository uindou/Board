using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface interFace /* CharaParentにかく関数は全部ここに書く。cの ~.hみたいなもん*/
{
    void Init(int x, int y,bool team);
    bool IsTeam();
    void Move(int i,int j);
    void AddDamage(int damage);
    string GetName();
    void Erase();
    int Power();

    void AttackImage();

    void DamageImage();
    List<(int, int)> AIMovable();
    List<(int, int)> Movable();
    List<(int, int)> Attackable();

}