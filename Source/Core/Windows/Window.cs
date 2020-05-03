using SFML.Window;
using SFML.Graphics;
using Basil.Interfaces;

namespace Basil.Core.Windows
{
    class Window : IUpdatable, IRenderable
    {
        public RenderWindow window { get; private set; }

        public Window(
            // TODO: Move out into WindowSettings
            string title = "",
            uint width = 100,
            uint height = 100,
            Styles styles = Styles.Titlebar | Styles.Close
        )
        {
            var mode = new VideoMode(width, height);
            window = new RenderWindow(mode, title, styles);
        }

        virtual public void Update()
        {
            window.DispatchEvents();
            window.Clear();
        }

        virtual public void Render()
        {
            window.Display();
        }
    }
}
