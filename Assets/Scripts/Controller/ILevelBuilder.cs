using Mobge.CarGame.ErkanYasun.Model;

namespace Mobge.CarGame.ErkanYasun.Controller
{
    public interface ILevelBuilder
    {
        ILevelBuilder AddObstacle(Obstacle aObstacle);
    }
}