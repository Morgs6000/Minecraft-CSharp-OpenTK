namespace openTk_Minecraft_Clone_Tutorial_Series {
    public class Program {
        // Entry point of the program
        static void Main(string[] args) {
            // Creates game object and disposes of it afet leaving the scope
            using(Game game = new Game(1280, 720)) {
                // running the game
                game.Run();
            }
        }
    }
}