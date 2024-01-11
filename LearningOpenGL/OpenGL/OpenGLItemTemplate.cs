using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace LearningOpenGL.OpenGL
{
    public class OpenGLItemTemplate : IDataTemplate
    {
        readonly Avalonia.Thickness _margin = new(20);
        readonly string _rowDefinitions = "40, 400";
        readonly string _columnDefinitions = "400";

        public Control Build(object param)
        {
            Grid grid = new();
            TextBlock header = new TextBlock() { Text = param.ToString(), FontSize = 20, TextAlignment = Avalonia.Media.TextAlignment.Center };
            Control content;

            grid.Margin = _margin;
            grid.RowDefinitions = new(_rowDefinitions);
            grid.ColumnDefinitions = new(_columnDefinitions);
            grid.Children.Add(header);
            Grid.SetRow(header, 0);

            switch (param)
            {
                case OpenGLItemType.HelloTriangle:
                    content = new HelloTriangle();
                    grid.Children.Add(content);
                    Grid.SetRow(content, 1);
                    return grid;
                case OpenGLItemType.HelloQuad:
                    content = new HelloQuad();
                    grid.Children.Add(content);
                    Grid.SetRow(content, 1);
                    return grid;
                case OpenGLItemType.HelloTriForce:
                    content = new HelloTriForce();
                    grid.Children.Add(content);
                    Grid.SetRow(content, 1);
                    return grid;
                case OpenGLItemType.HelloRefactoring:
                    content = new HelloRefactoring();
                    grid.Children.Add(content);
                    Grid.SetRow(content, 1);
                    return grid;
                default:
                    content = new TextBlock() { Text = "Unknown OpenGLItemType \nAdd case in OpenGLItemTemplate", FontSize = 20, TextAlignment = Avalonia.Media.TextAlignment.Center }; ;
                    grid.Children.Add(content);
                    Grid.SetRow(content, 1);
                    return grid;
            }
        }

        public bool Match(object data)
        {
            // Check if we can accept the provided data
            return data is OpenGLItemType;
        }
    }
}
