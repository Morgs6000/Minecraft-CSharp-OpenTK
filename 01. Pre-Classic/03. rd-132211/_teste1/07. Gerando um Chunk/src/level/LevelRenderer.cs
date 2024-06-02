namespace RubyDung.src.level {
    public class LevelRenderer {
        private Chunk chunks;

        public LevelRenderer() {
            int x = 0;
            int y = 0;
            int z = 0;

            int x0 = x * 16;
            int y0 = y * 16;
            int z0 = z * 16;
            int x1 = (x + 1) * 16;
            int y1 = (y + 1) * 16;
            int z1 = (z + 1) * 16;

            this.chunks = new Chunk(x0, y0, z0, x1, y1, z1);
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
