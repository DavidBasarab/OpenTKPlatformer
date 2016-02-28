using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace OpenTKPlatformer
{
    public class InputController
    {
        public List<Key> KeysDown { get; set; }

        public List<Key> KeysDownLast { get; set; }

        public List<MouseButton> MouseButtonsDown { get; set; }

        public List<MouseButton> MouseButtonsDownLast { get; set; }

        public InputController(GameWindow game)
        {
            KeysDown = new List<Key>();
            KeysDownLast = new List<Key>();

            MouseButtonsDown = new List<MouseButton>();
            MouseButtonsDownLast = new List<MouseButton>();

            game.KeyDown += GameOnKeyDown;
            game.KeyUp += GameOnKeyUp;

            game.MouseDown += GameOnMouseDown;
            game.MouseUp += GameOnMouseUp;
        }

        public bool KeyDown(Key key)
        {
            return KeysDown.Contains(key);
        }

        public bool KeyPress(Key key)
        {
            return KeysDown.Contains(key) && !KeysDownLast.Contains(key);
        }

        public bool KeyRelease(Key key)
        {
            return !KeysDown.Contains(key) && KeysDownLast.Contains(key);
        }

        public bool MouseButtonDown(MouseButton mouseButton)
        {
            return MouseButtonsDown.Contains(mouseButton);
        }

        public bool MouseButtonPress(MouseButton mouseButton)
        {
            return MouseButtonsDown.Contains(mouseButton) && !MouseButtonsDownLast.Contains(mouseButton);
        }

        public bool MouseButtonRelease(MouseButton mouseButton)
        {
            return !MouseButtonsDown.Contains(mouseButton) && MouseButtonsDownLast.Contains(mouseButton);
        }

        public void Update()
        {
            KeysDownLast = new List<Key>(KeysDown);
            MouseButtonsDownLast = new List<MouseButton>(MouseButtonsDown);
        }

        private void GameOnKeyDown(object sender, KeyboardKeyEventArgs keyboardKeyEventArgs)
        {
            KeysDown.Add(keyboardKeyEventArgs.Key);
        }

        private void GameOnKeyUp(object sender, KeyboardKeyEventArgs keyboardKeyEventArgs)
        {
            KeysDown.RemoveAll(i => i == keyboardKeyEventArgs.Key);
        }

        private void GameOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            MouseButtonsDown.Add(mouseButtonEventArgs.Button);
        }

        private void GameOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            MouseButtonsDown.RemoveAll(i => i == mouseButtonEventArgs.Button);
        }
    }
}