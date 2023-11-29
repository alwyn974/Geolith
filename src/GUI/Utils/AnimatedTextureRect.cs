using Godot;

namespace Geolith.GUI.Utils
{
    [Tool]
    [GlobalClass]
    public partial class AnimatedTextureRect : TextureRect
    {
        [Export] public SpriteFrames Frames;
        [Export] public string CurrentAnimation = "default";
        [Export] public int FrameIndex;
        [Export] public float SpeedScale = 1.0f;
        [Export] public bool AutoPlay;
        [Export] public bool Playing;

        [Signal]
        public delegate void AnimationFinishedEventHandler(string animationName);

        private float _refreshRate = 1.0f;
        private double _fps = 60.0f;
        private float _frameDelta;

        public override void _Ready()
        {
            GetAnimationData();

            if (AutoPlay) Play(CurrentAnimation);
        }

        public override void _Process(double delta)
        {
            if (!Playing || Frames == null) return;

            if (!Frames.HasAnimation(CurrentAnimation))
            {
                GD.PrintErr("Animation " + CurrentAnimation + " does not exist!");
                return;
            }

            GetAnimationData();
            _frameDelta += (float)delta * SpeedScale;

            if (_frameDelta >= _refreshRate / _fps)
            {
                Texture = GetNextTexture();
                _frameDelta = 0.0f;
            }
        }

        private Texture2D GetNextTexture()
        {
            FrameIndex++;

            int frameCount = Frames.GetFrameCount(CurrentAnimation);

            if (FrameIndex >= frameCount)
            {
                if (!Frames.GetAnimationLoop(CurrentAnimation)) Stop();
                FrameIndex = 0;
            }

            GetAnimationData();
            return Frames.GetFrameTexture(CurrentAnimation, FrameIndex);
        }

        private void GetAnimationData()
        {
            _fps = Frames.GetAnimationSpeed(CurrentAnimation);
            _refreshRate = Frames.GetFrameDuration(CurrentAnimation, FrameIndex);
        }

        public void Play(string animationName)
        {
            FrameIndex = 0;
            _frameDelta = 0.0f;
            CurrentAnimation = animationName;

            GetAnimationData();
            Playing = true;
        }

        public void Stop()
        {
            FrameIndex = 0;
            Playing = false;

            EmitSignal(nameof(AnimationFinished), CurrentAnimation);
        }

        public void Resume()
        {
            Playing = true;
        }

        public void Pause()
        {
            Playing = false;
        }
    }
}
