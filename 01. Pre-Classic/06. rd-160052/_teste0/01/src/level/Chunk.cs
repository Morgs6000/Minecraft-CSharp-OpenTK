﻿using RubyDung.src.level.tile;

namespace RubyDung.src.level;

public class Chunk {
    public Level level;

    public int x0;
    public int y0;
    public int z0;

    public int x1;
    public int y1;
    public int z1;

    private Texture texture;
    private Tesselator t = new Tesselator();

    public Chunk(Level level, int x0, int y0, int z0, int x1, int y1, int z1) {
        this.level = level;

        this.x0 = x0;
        this.y0 = y0;
        this.z0 = z0;

        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
    }

    public void rebuild() {
        for(int x = this.x0; x < this.x1; x++) {
            for(int y = this.y0; y < this.y1; y++) {
                for(int z = this.z0; z < this.z1; z++) {
                    int tileId = this.level.getTile(x, y, z);

                    //Console.WriteLine($"Tile ID at ({x}, {y}, {z}): {tileId}");

                    if(tileId > 0) {
                        Tile.tiles[tileId].render(t, this.level, x, y, z);
                        //Tile.rock.render(t, this.level, x, y, z);
                        //Tile.grass.render(t, this.level, x, y, z);
                        //Tile.dirt.render(t, this.level, x, y, z);
                        //Tile.stoneBrick.render(t, this.level, x, y, z);
                        //Tile.wood.render(t, this.level, x, y, z);
                    }
                }
            }
        }

        this.t.flush();
    }

    public void render() {
        this.t.bind();
    }
}
