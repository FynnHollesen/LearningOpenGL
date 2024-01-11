using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using LearningOpenGL.OpenGL.Shaders;
using Silk.NET.OpenGL;
using System;

namespace LearningOpenGL.OpenGL
{
    public class HelloRefactoring : OpenGlControlBase
    {
        private static GL _gl;
        private static ShaderProgram _shaderProgram;
        private static uint _vao;
        private static uint _vbo;
        private static uint _ebo;

        protected unsafe override void OnOpenGlInit(GlInterface gl)
        {
            base.OnOpenGlInit(gl);
            _gl = GL.GetApi(gl.GetProcAddress);

            float[] vertices =
            {
                -1f,  -0.5f * (float)Math.Sqrt(3), 0.0f, // low left
                0.0f, -0.5f * (float)Math.Sqrt(3), 0.0f, // low center
                1f,  -0.5f * (float)Math.Sqrt(3), 0.0f, // low right
                0.5f, 0.0f, 0.0f, // up right
                0.0f, 0.5f * (float)Math.Sqrt(3), 0.0f, // up center
                -0.5f, 0.0f, 0.0f, // up left
            };

            uint[] indices =
            {
                0u, 1u, 5u,
                1u, 2u, 3u,
                3u, 4, 5u,
            };

            _vao = _gl.GenVertexArray();
            _vbo = _gl.GenBuffer();
            _ebo = _gl.GenBuffer();

            _gl.BindVertexArray(_vao);

            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
            fixed (float* buf = vertices)
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);

            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
            fixed (uint* buf = indices)
                _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);

            const uint positionLoc = 0;
            _gl.EnableVertexAttribArray(positionLoc);
            _gl.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            _gl.BindVertexArray(0);
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

            _shaderProgram = new ShaderProgram(_gl, "OpenGL/Shaders/shader.vert", "OpenGL/Shaders/shader.frag");
        }

        protected override void OnOpenGlDeinit(GlInterface gl)
        {
            base.OnOpenGlDeinit(gl);
            _gl.DeleteBuffer(_vao);
            _gl.DeleteBuffer(_vbo);
            _gl.DeleteBuffer(_ebo);
            _shaderProgram.Dispose();
        }

        protected override unsafe void OnOpenGlRender(GlInterface gl, int fb)
        {
            _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
            _gl.Viewport(0, 0, (uint)Bounds.Width, (uint)Bounds.Height);
            _shaderProgram.Use();
            _gl.BindVertexArray(_vao);

            _gl.DrawElements(PrimitiveType.Triangles, 9, DrawElementsType.UnsignedInt, (void*)0);
        }
    }
}
