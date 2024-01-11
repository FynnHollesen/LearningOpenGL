using CommunityToolkit.Mvvm.ComponentModel;
using LearningOpenGL.OpenGL;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace LearningOpenGL.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    List<OpenGLItemType> _openGLItems = [];

    public MainViewModel()
    {
        foreach (OpenGLItemType openGLType in Enum.GetValues(typeof(OpenGLItemType)))
        {
            _openGLItems.Add(openGLType);
        }
    }
}
