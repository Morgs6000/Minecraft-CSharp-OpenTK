namespace RubyDung.src;

public class AnimatedTexture {
    public string Name {
        get; private set;
    }
    public int[] Frames {
        get; private set;
    }
    public int FrameTime {
        get; private set;
    }
    private int currentFrameIndex;
    private double elapsedTime;

    public AnimatedTexture(string name, int[] frames, int frameTime) {
        Name = name;
        Frames = frames;
        FrameTime = frameTime;
        currentFrameIndex = 0;
        elapsedTime = 0.0;
    }

    public void Update(double deltaTime) {
        elapsedTime += deltaTime;
        if(elapsedTime >= FrameTime) {
            elapsedTime = 0.0;
            currentFrameIndex = (currentFrameIndex + 1) % Frames.Length;
        }
    }

    public int GetCurrentFrame() {
        return Frames[currentFrameIndex];
    }
}
