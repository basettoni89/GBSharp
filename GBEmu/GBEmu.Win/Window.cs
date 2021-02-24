
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;

namespace GBEmu.Win
{
    public struct Color
    {
        public Color(ushort r, ushort g, ushort b, ushort a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ushort R { get; }
        public ushort G { get; }
        public ushort B { get; }
        public ushort A { get; }

        public float[] GetNormalizedColor()
        {
            float[] normalized = new float[4];

            normalized[0] = R / 255f;
            normalized[1] = G / 255f;
            normalized[2] = B / 255f;
            normalized[3] = A / 255f;

            return normalized;
        }
    }

    public class Window : GameWindow
    {
        private readonly uint[] _indices =
        {
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        // Add a handle for the EBO
        private int _elementBufferObject;

        private readonly Color bgColor = new Color(0x9B, 0xBC, 0x0F, 0xFF);
        private readonly Color fgColor = new Color(0x30, 0x62, 0x30, 0xFF);

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            var normalizedBG = bgColor.GetNormalizedColor();
            GL.ClearColor(normalizedBG[0], normalizedBG[1], normalizedBG[2], normalizedBG[3]);

            var normalizedFG = fgColor.GetNormalizedColor();
            float[] _vertices =
            {
                 0.5f,  0.5f, 0.0f, normalizedFG[0], normalizedFG[1], normalizedFG[2], // top right
                 0.5f, -0.5f, 0.0f, normalizedFG[0], normalizedFG[1], normalizedFG[2], // bottom right
                -0.5f, -0.5f, 0.0f, normalizedFG[0], normalizedFG[1], normalizedFG[2], // bottom left
                -0.5f,  0.5f, 0.0f, normalizedFG[0], normalizedFG[1], normalizedFG[2], // top left
            };

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            Debug.WriteLine($"Maximum number of vertex attributes supported: {maxAttributeCount}");

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteProgram(_shader.Handle);

            base.OnUnload();
        }
    }
}
