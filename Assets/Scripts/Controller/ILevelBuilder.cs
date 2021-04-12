using Mobge.CarGame.ErkanYasun.Model;
using UnityEngine;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface ILevelBuilder
    {
        ILevelBuilder addObstacle(Obstacle aObstacle);

    }
}
