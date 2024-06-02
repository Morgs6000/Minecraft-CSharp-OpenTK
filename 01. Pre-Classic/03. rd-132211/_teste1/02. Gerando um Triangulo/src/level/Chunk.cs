namespace RubyDung.src.level {
    public class Chunk {
        private Tesselator t = new Tesselator();

        private void rebuild() {
            Tile.tile.render(this.t);

            this.t.flush();
        }

        public void render() {
            this.rebuild();
        }

        // Essa função deveria existir?
        public Tesselator getTesselador() {
            return this.t;
        }
    }
}
