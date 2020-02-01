using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface interFace /* CharaParentにかく関数は全部ここに書く。cの ~.hみたいなもん*/
{
    void Init(int x, int y);
    void AddDamage(int damage);
    string GetName();
    void Erase();
    List<(int, int)> Movable();
    List<(int, int)> Attackable();

}