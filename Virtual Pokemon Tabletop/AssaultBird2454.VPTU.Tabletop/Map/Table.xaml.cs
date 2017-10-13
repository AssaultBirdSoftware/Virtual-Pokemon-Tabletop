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
//using D3D = Microsoft.DirectX.Direct3D;

namespace AssaultBird2454.VPTU.Tabletop.Map
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        private PerspectiveCamera myPCamera = new PerspectiveCamera();

        public Table()
        {
            InitializeComponent();

            Table_Init();

            //Thread thread = new Thread(new ThreadStart(() =>
            //{
            //    D3D.Font text;
            //    System.Drawing.Font systemfont = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Regular);
            //    text = new D3D.Font(device, systemfont);

            //    while (true)
            //    {
            //        if (myPCamera.LookDirection.Z == 360)
            //            myPCamera.LookDirection = new Vector3D(myPCamera.LookDirection.X, myPCamera.LookDirection.Y, 0);
            //        myPCamera.LookDirection = new Vector3D(myPCamera.LookDirection.X, myPCamera.LookDirection.Y, myPCamera.LookDirection.Z + 1);
            //    }
            //}));
            //thread.Start();
        }

        private void Table_Init()
        {
            #region Camera
            // Specify where in the 3D scene the camera is.
            myPCamera.Position = new Point3D(0, 0, -20);

            // Specify the direction that the camera is pointing.
            myPCamera.LookDirection = new Vector3D(0, 0, 1);
            myPCamera.UpDirection = new Vector3D(1, 0, 0);

            // Define camera's horizontal field of view in degrees.
            myPCamera.FieldOfView = 60;

            // Asign the camera to the viewport
            _Table.Camera = myPCamera;
            #endregion

            DrawTerrain(5, 1, 1);
            //Table_Test();
        }

        private void Table_Test()
        {
            ModelVisual3D visual = new ModelVisual3D();
            Model3DGroup group = new Model3DGroup();

            GeometryModel3D mod = new GeometryModel3D();
            MeshGeometry3D mesh = new MeshGeometry3D();
            PointLight light = new PointLight() { Color = Color.FromArgb(255, 255, 255, 0), Position = new Point3D(5, 0, 0), Range = 500 };
            //DirectionalLight dl = new DirectionalLight() { Color = Color.FromArgb(255, 255, 255, 255), Direction = new Vector3D(0, 0, -3) };
            DiffuseMaterial Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));

            #region Test Object
            mesh.Positions.Add(new Point3D(-5, -5, 0));
            mesh.Positions.Add(new Point3D(0, 5, 0));
            mesh.Positions.Add(new Point3D(5, -5, 0));
            mesh.Positions.Add(new Point3D(10, 5, 0));
            mesh.Positions.Add(new Point3D(15, -5, 0));
            mesh.Positions.Add(new Point3D(20, 5, 0));

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);

            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));
            mesh.Normals.Add(new Vector3D(0, 0, -1));

            mesh.TextureCoordinates.Add(new Point(1, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 0));

            mod.Geometry = mesh;
            mod.Material = Material;

            group.Children.Add(mod);
            //group.Children.Add(dl);
            group.Children.Add(light);
            visual.Content = group;
            _Table.Children.Add(visual);
            #endregion
        }

        /**
 * <summary>
 * Method that create the 3d terrain on a Viewport3D control
 * </summary>
 *
 * <param name="terrainMap">terrain to show</param>
 * <param name="terrainSize">terrain size</param>
 * <param name="minHeightValue">minimum terraing height</param>
 * <param name="maxHeightValue">maximum terraing height</param>
 */
        private void DrawTerrain(float Max_X, float Max_Y, int Density = 1)
        {
            ModelVisual3D visual = new ModelVisual3D();// Visual
            Model3DGroup group = new Model3DGroup();// Group

            GeometryModel3D mod = new GeometryModel3D();// Model
            MeshGeometry3D mesh = new MeshGeometry3D();// Mesh
            //Int32Collection triangles = new Int32Collection();// Mesh's Triangles
            PointLight light = new PointLight() { Color = Color.FromArgb(255, 255, 255, 0), Position = new Point3D(1, 0, 0), Range = 500 };// Light
            DiffuseMaterial Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)));// Meterial

            for (var Y = 0; Y < Max_Y; Y++)
            {
                for (var X = 0; X < Max_X; X++)
                {
                    mesh.Positions.Add(new Point3D(X, Y, 0));
                    mesh.Positions.Add(new Point3D(X, Y + 1, 0));
                    mesh.Positions.Add(new Point3D(X + 1, Y, 0));

                    //mesh.Positions.Add(new Point3D(X, Y + 1, 0));
                    //mesh.Positions.Add(new Point3D(X + 1, Y + 1, 0));
                    //mesh.Positions.Add(new Point3D(X + 1, Y, 0));

                    //int ind1 = X + Y * (Density);
                    //int ind2 = ind1 + (Density);
                    //first triangle
                    //triangles.Add(ind1);
                    //triangles.Add(ind2 + 1);
                    //triangles.Add(ind2);

                    //second triangle
                    //triangles.Add(ind1);
                    //triangles.Add(ind1 + 1);
                    //triangles.Add(ind2 + 1);
                }
            }

            #region Test
            mesh.Positions.Add(new Point3D(6, 0, 0));
            mesh.Positions.Add(new Point3D(6, 1, 0));
            mesh.Positions.Add(new Point3D(7, 0, 0));

            mesh.Positions.Add(new Point3D(6, 1, 0));
            mesh.Positions.Add(new Point3D(7, 1, 0));
            mesh.Positions.Add(new Point3D(7, 0, 0));
            #endregion
            
            mod.Geometry = mesh;
            //((MeshGeometry3D)mod.Geometry).TriangleIndices = triangles;
            mod.Material = Material;

            group.Children.Add(mod);
            group.Children.Add(light);
            visual.Content = group;
            _Table.Children.Add(visual);
        }
    }
}
