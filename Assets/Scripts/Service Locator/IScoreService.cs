using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreService : IGameService
{
    public abstract void GiveScore(int amount);
}
