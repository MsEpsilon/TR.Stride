using Stride.Core.Diagnostics;
using Stride.Engine;
using Stride.Games;
using Stride.Graphics;
using StrideCommunity.ImGuiDebug;
using System.Linq;

using var game = new CustomGame();
game.Run();

class CustomGame : Game
{
    public override void ConfirmRenderingSettings(bool gameCreation)
    {
        base.ConfirmRenderingSettings(gameCreation);

        // Make sure labels are exposed in renderdoc
        Profiler.EnableAll();
        GraphicsDeviceManager.DeviceCreationFlags |= DeviceCreationFlags.Debug;
    }

    protected override void BeginRun()
    {
        base.BeginRun();

        // Fix Update order
        ((GameSystemBase)GameSystems.First(x => x is InputSystem)).UpdateOrder = -2;

        var imGuiSystem = new ImGuiSystem(Services, GraphicsDeviceManager)
        {
            UpdateOrder = -1
        };

        new HierarchyView(Services);
    }
}