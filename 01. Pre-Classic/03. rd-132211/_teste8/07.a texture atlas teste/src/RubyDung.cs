using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        this.framebuffer_size_callback(e.Width, e.Height);
    }

    // ..:: Shader ::..
    Shader shader;

    // ..:: Triangle ::..
    private float[] vertices = {
        // vertices     // texture coords
        -0.5f, -0.5f,   0.0f, 0.0f,       // bottom left  // 0
         0.5f, -0.5f,   1.0f, 0.0f,       // bottom right // 1
         0.5f,  0.5f,   1.0f, 1.0f,       // top right    // 2
        -0.5f,  0.5f,   0.0f, 1.0f        // top left     // 3
    };

    private int[] indices = {
        0, 1, 2, // first Triangle
        0, 2, 3  // second Triangle
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object

    private void Triangle() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        // position attribute
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // texture coord attribute
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // Element Buffer Object
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    // ..:: Texture ::..
    //Texture texture;
    private TextureAtlas textureAtlas;
    private int textureId;

    protected override void OnLoad() {
        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");
        this.Triangle();
        //this.texture = new Texture("terrain.png");

        var texturePaths = new List<string> {
            "grass_top", "stone", "dirt", "grass_side", "wood", "stoneslab_side", "stoneslab_top", "brick", "tnt_side", "tnt_top", "tnt_bottom", "web", "rose", "flower", "", "sapling",
            "stonebrick", "bedrock", "sand", "gravel", "tree_side", "tree_top", "blockIron", "blockGold", "blockDiamond", "blockEmerald", "", "", "mushroom_red", "mushroom_brown", "sapling_jungle", "",
            "oreGold", "oreIron", "oreCoal", "bookshelf", "stoneMoss", "obsidian", "grass_side_overlay", "tallgrass", "", "beacon", "", "workbench_top", "furnace_front", "furnace_side", "dispenser_front", "",
            "sponge", "glass", "oreDiamond", "oreRedstone", "leaves", "leaves_opaque", "stonebricksmooth", "deadbush", "fern", "", "", "workbench_side", "workbench_front", "furnace_front_lit", "furnace_top", "sapling_spruce",
            "cloth_0", "mobSpawner", "snow", "ice", "snow_side", "cactus_top", "cactus_side", "cactus_bottom", "clay", "reeds", "musicBlock", "jukebox_top", "waterlily", "mycel_side", "mycel_top", "sapling_birch",
            "torch", "doorWood_upper", "doorIron_upper", "ladder", "trapdoor", "fenceIron", "farmland_wet", "farmland_dry", "crops_0", "crops_1", "crops_2", "crops_3", "crops_4", "crops_5", "crops_6", "crops_7",
            "lever", "doorWood_lower", "doorIron_lower", "redtorch_lit", "stonebricksmooth_mossy", "stonebricksmooth_cracked", "pumpkin_top", "hellrock", "hellsand", "lightgem", "piston_top_sticky", "piston_top", "piston_side", "piston_bottom", "piston_inner_top", "stem_straight",
            "rail_turn", "cloth_15", "cloth_7", "redtorch", "tree_spruce", "tree_birch", "pumpkin_side", "pumpkin_face", "pumpkin_jack", "cake_top", "cake_side", "cake_inner", "cake_bottom", "mushroom_skin_red", "mushroom_skin_brown", "stem_bent",
            "rail", "cloth_14", "cloth_6", "repeater", "leaves_spruce", "leaves_spruce_opaque", "bed_feet_top", "bed_head_top", "melon_side", "melon_top", "cauldron_top", "cauldron_inner", "", "mushroom_skin_stem", "mushroom_inside", "vine",
            "blockLapis", "cloth_13", "cloth_5", "repeater_lit", "thinglass_top", "bed_feet_end", "bed_feet_side", "bed_head_side", "bed_head_end", "tree_jungle", "cauldron_side", "cauldron_bottom", "brewingStand_base", "brewingStand", "endframe_top", "endframe_side",
            "oreLapis", "cloth_12", "cloth_4", "activatorRail", "redstoneDust_cross", "redstoneDust_line", "enchantment_top", "dragonEgg", "cocoa_2", "cocoa_1", "cocoa_0", "oreEmerald", "tripWireSource", "tripWire", "endframe_eye", "whiteStone",
            "sandstone_top", "cloth_11", "cloth_3", "activatorRail_powered", "redstoneDust_cross_overlay", "redstoneDust_line_overlay", "enchantment_side", "enchantment_bottom", "commandBlock", "itemframe_back", "flowerPot", "", "", "", "", "",
            "sandstone_side", "cloth_10", "cloth_2", "detectorRail", "leaves_jungle", "leaves_jungle_opaque", "wood_spruce", "wood_jungle", "carrots_0", "carrots_1", "carrots_2", "carrots_3", "potatoes_3", "", "", "",
            "sandstone_bottom", "cloth_9", "cloth_1", "redstoneLight", "redstoneLight_lit", "stonebricksmooth_carved", "wood_birch", "anvil_base", "anvil_top_damaged_1", "", "", "", "", "", "", "",
            "netherBrick", "cloth_8", "netherStalk_0", "netherStalk_1", "netherStalk_2", "sandstone_carved", "sandstone_smooth", "anvil_top", "anvil_top_damaged_2", "", "", "", "", "", "", "",
            "destroy_0", "destroy_1", "destroy_2", "destroy_3", "destroy_4", "destroy_5", "destroy_6", "destroy_7", "destroy_8", "destroy_9", "", "", "", "", "", ""
        }; // Lista de caminhos para suas texturas
        textureAtlas = new TextureAtlas(256, 256); // Dimensões do atlas (ajuste conforme necessário)

        int x = 0, y = 0;
        int textureSize = 16; // Supondo que todas as texturas são de 16x16 pixels

        foreach(var path in texturePaths) {
            if(!string.IsNullOrWhiteSpace(path)) {
                textureAtlas.AddTexture(path, x, y);
            }
            // Se o caminho estiver vazio, não faz nada, deixando um espaço em branco

            x += textureSize;
            if(x >= 256) { // Passa para a próxima linha do atlas
                x = 0;
                y += textureSize;
            }
        }

        textureId = textureAtlas.CreateTexture(); // Cria a textura do OpenGL a partir do atlas
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0F);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        //this.texture.bind();
        GL.BindTexture(TextureTarget.Texture2D, textureId);

        this.shader.use();

        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

        this.SwapBuffers();
    }

    private bool isWireframe = false;

    private void processInput() {
        // CLose Window
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        // Wireframe
        if(this.KeyboardState.IsKeyDown(Keys.F3) && this.KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            //GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), this.isWireframe ? 1 : 0);
            this.shader.setBool("isWireframe", isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }

    private void framebuffer_size_callback(int width, int height) {
        GL.Viewport(0, 0, width, height);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
