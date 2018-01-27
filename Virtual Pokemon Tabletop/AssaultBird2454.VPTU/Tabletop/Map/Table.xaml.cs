using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Windows.Media.Media3D;
using System.Threading;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph;

namespace AssaultBird2454.VPTU.Tabletop.Map
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
        }
        private Axies axies = new Axies();
        private Vertex[] vertices = null;
        private float[] vertexArrayValues;
        private LookAtCamera camera = new LookAtCamera();

        private void Table_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            //  Get the OpenGL instance that's been passed to us.
            OpenGL gl = args.OpenGL;

            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //  Move the geometry into a fairly central position.
            gl.Translate(0f, 0.0f, -6.0f);

            axies.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Design);

            RenderVertices_VertexArray(args.OpenGL);

            //  Flush OpenGL.
            gl.Flush();
            /*
            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Reset the modelview matrix.
            gl.LoadIdentity(); */
        }

        private void RenderVertices_VertexArray(OpenGL gl)
        {
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.VertexPointer(3, 0, vertexArrayValues);
            gl.DrawArrays(OpenGL.GL_POINTS, 0, 2);//vertices.Length);
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
        }
    }
}