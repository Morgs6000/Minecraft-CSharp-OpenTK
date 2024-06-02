namespace RubyDung.src.level {
    public class LevelRenderer {
        private Chunk chunks;

        public LevelRenderer() {
            this.chunks = new Chunk();
            this.chunks.render();
        }

        public void render() {
            // isso deveria ser chamado aqui?
            this.chunks.getTesselador().use();
        }

        public Shader getShader() {
            return this.chunks.getShader();
        }
    }
}
