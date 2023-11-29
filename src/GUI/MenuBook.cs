using Geolith.GUI.Utils;
using Godot;

namespace Geolith.GUI
{
    public partial class MenuBook : Control
    {
        private AnimatedTextureRect _book;

        public override void _Ready()
        {
            _book = GetNode<AnimatedTextureRect>("Book");
            // _book.Play("open");

            _book.AnimationFinished += _on_Book_animation_finished;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        private void _on_Book_animation_finished(string animName)
        {

        }

        public override void _ExitTree()
        {
            _book.AnimationFinished -= _on_Book_animation_finished;
        }
    }
}
