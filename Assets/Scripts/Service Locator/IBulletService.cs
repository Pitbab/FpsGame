using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletService : IGameService
{
    public abstract void Hit(TempContoller shooter, Ray ray);
}
