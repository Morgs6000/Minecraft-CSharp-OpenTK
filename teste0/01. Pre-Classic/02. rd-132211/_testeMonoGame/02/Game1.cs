using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RubyDung;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Matrix _view;
    private Matrix _projection;

    private VertexBuffer _vertexBuffer;

    public const string ContentFolderEffects = "Effects/";
    private Effect _effect;

    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        // TODO: Add your initialization logic here

        // Configurar matrizes de visualização e projeção
        _view = Matrix.CreateLookAt(Vector3.One * 5, Vector3.Zero, Vector3.Up);
        _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 25000.0f);

        // Ajustar o tamanho do buffer de retorno
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
        _graphics.ApplyChanges();

        var vertices = new VertexPosition[] {
            new VertexPosition(new Vector3(-1, 0, -1)),
            new VertexPosition(new Vector3(-1, 0, 1)),
            new VertexPosition(new Vector3(1, 0, -1)),
        };

        _vertexBuffer = new VertexBuffer(GraphicsDevice, VertexPosition.VertexDeclaration, 3, BufferUsage.None);
        _vertexBuffer.SetData(vertices);

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        _effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");
    }

    protected override void Update(GameTime gameTime) {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        // Definir parâmetros do shader
        _effect.Parameters["World"].SetValue(Matrix.Identity);
        _effect.Parameters["View"].SetValue(_view);
        _effect.Parameters["Projection"].SetValue(_projection);
        _effect.Parameters["DiffuseColor"].SetValue(Color.Red.ToVector3());

        // Definir estado do rasterizador
        var rasterizerState = RasterizerState.CullNone;
        GraphicsDevice.RasterizerState = rasterizerState;

        // Definir buffer de vértice
        GraphicsDevice.SetVertexBuffer(_vertexBuffer);

        // Comece a desenhar
        foreach(var pass in _effect.CurrentTechnique.Passes) {
            pass.Apply();

            // Desenhe o triângulo
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
        }

        base.Draw(gameTime);
    }
}
