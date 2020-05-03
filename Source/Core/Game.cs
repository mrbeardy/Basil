using Basil.Engine;
using Basil.Core.Windows;
using Basil.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Basil.Core
{
    class Game
    {
        public static Game Instance
        {
            get
            {
                lock (m_Padlock)

                    if (m_Instance == null)
                    {
                        m_Instance = new Game();
                    }

                return m_Instance;
            }
        }

        // TODO: Implement SceneManager
        public List<Scene> scenes { get; private set; } = new List<Scene>();

        private static Game m_Instance = null;
        private static readonly object m_Padlock = new object();

        // TODO: Make this private
        public SFML.Graphics.RenderWindow window { get => basilWindow?.window; }
        private Window basilWindow;
        private WindowFollower debugWindow;

        private List<Window> windows = new List<Window>();

        private bool isRunning;
        private int sleepInterval = 16;

        public void Run(string title)
        {
            if (isRunning) return;

            Initialize(title);
            GameLoop();
        }

        public void SetTitle(string title)
        {
            window.SetTitle(title);
        }

        // TODO: Game should Update the World, which will Update the ActiveScene
        public void AddScene(Scene scene)
        {
            if (scenes.Contains(scene))
                throw new ArgumentException("Scene already exists in Game", "scene");

            scenes.Add(scene);
        }

        private void Initialize(string title)
        {
            basilWindow = new Window(title, width: 960, height: 480);
            basilWindow.window.Closed += Window_Closed;
            basilWindow.window.KeyPressed += Window_KeyPressed;
            windows.Add(basilWindow);

            debugWindow = new WindowFollower(basilWindow);
            windows.Add(debugWindow);
        }

        private void GameLoop()
        {
            if (isRunning) return;
            isRunning = true;

            DateTime lastTime = DateTime.Now;

            while (isRunning)
            {
                DateTime currentTime = DateTime.Now;
                Time.deltaTime = (currentTime - lastTime).Milliseconds;

                Update();
                Render();

                Thread.Sleep(sleepInterval);
                lastTime = currentTime;
            }
        }

        // TODO: Clean up. Way too many things are updating and rendering other things...
        private void Update()
        {
            IEnumerable<IUpdatable> updatables = Enumerable.Concat<IUpdatable>(scenes, windows);

            foreach (IUpdatable updatable in updatables) updatable.Update();
        }

        // TODO: Remove this in favor of components registering to an OnRender Action on the window.
        private void Render()
        {
            IEnumerable<IRenderable> renderables = Enumerable.Concat<IRenderable>(scenes, windows);

            foreach (IRenderable renderable in renderables) renderable.Render();
        }

        private void Shutdown()
        {
            // TODO: Run through shutdown for Scenes and Windows, and propagate to children
            // Environment.Exit is a lazy way to handle shutdown because it kills everything, including the main Thread.
            // This is a problem because if the User has their own shutdown code after Game.Run, it will never be called.
            Environment.Exit(0);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
#if DEBUG
            if (e.Code == SFML.Window.Keyboard.Key.Escape) Shutdown();
#endif
        }
    }
}
