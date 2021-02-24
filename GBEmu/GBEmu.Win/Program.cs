using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace GBEmu.Win
{
    public static class Program
    {
        private static void Main()
        {

            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "LearnOpenTK - Creating a Window",
            };

            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
