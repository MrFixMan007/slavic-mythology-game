using System;
using UnityEngine;
using VContainer;

public interface IEnemyFactory
{
    Enemy CreateEnemy(Vector3 position);
}