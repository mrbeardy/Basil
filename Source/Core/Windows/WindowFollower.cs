using SFML.Graphics;
using SFML.System;
using System;

namespace Basil.Core.Windows
{
    class WindowFollower : Window
    {
        private Window target;
        private RenderWindow targetWindow { get => target.window; }

        private Vector2i lastTargetPosition;
        private uint height;

        private static Vector2i alignmentOffset = new Vector2i(8, 50);
        private static uint borderWidth = 2;


        public WindowFollower(Window target, uint height = 200) : base(styles: SFML.Window.Styles.None)
        {
            this.target = target;
            this.height = height;

            SnapToTarget();
        }

        override public void Update()
        {
            if (target.window.Position != lastTargetPosition) SnapToTarget();
        }

        public void SnapToTarget()
        {
            lastTargetPosition = target.window.Position;


            // TODO: Add ability to snap to different sides of the target window
            window.Size = new Vector2u(target.window.Size.X + borderWidth, height);
            window.Position = new Vector2i(
                lastTargetPosition.X, 
                (int)(lastTargetPosition.Y + target.window.Size.Y)
            ) + alignmentOffset;

            target.window.RequestFocus();
        }
    }
}
