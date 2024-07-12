using RubyDung.src.phys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubyDung.src.level;

public class LevelRenderer {
    private Level level;
    private Chunk[] chunks;

    private int xChunks;
    private int yChunks;
    private int zChunks;

    Tesselator t = new Tesselator();

    public LevelRenderer(Level level) {
        this.level = level;

        this.xChunks = level.width / 16;
        this.yChunks = level.height / 16;
        this.zChunks = level.depth / 16;

        this.chunks = new Chunk[this.xChunks * this.yChunks * this.zChunks];

        for(int x = 0; x < this.xChunks; ++x) {
            for(int y = 0; y < this.yChunks; ++y) {
                for(int z = 0; z < this.zChunks; ++z) {
                    int x0 = x * 16;
                    int y0 = y * 16;
                    int z0 = z * 16;

                    int x1 = (x + 1) * 16;
                    int y1 = (y + 1) * 16;
                    int z1 = (z + 1) * 16;

                    this.chunks[(x + y * this.xChunks) * this.zChunks + z] = new Chunk(level, x0, y0, z0, x1, y1, z1);
                    this.chunks[(x + y * this.xChunks) * this.zChunks + z].load();
                }
            }
        }
    }

    public void render() {
        for(int i = 0; i < this.chunks.Length; i++) {
            this.chunks[i].render();
        }
    }

    public void pick(Player player) {
        float r = 3.0f;

        AABB box = player.bb.grow(r, r, r);

        int x0 = (int)box.x0;
        int x1 = (int)(box.x1 + 1.0f);

        int y0 = (int)box.y0;
        int y1 = (int)(box.y1 + 1.0f);

        int z0 = (int)box.z0;
        int z1 = (int)(box.z1 + 1.0f);

        for(int x = x0; x < x1; x++) {
            for(int y = y0; x < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    for(int i = 0; i < 6; i++) {
                        this.t.init();
                        Tile.rock.renderFace(this.t, x, y, z, i);
                        this.t.flush();
                        //this.t.render();
                    }
                }
            }
        }
    }
}
