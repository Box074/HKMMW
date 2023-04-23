// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using HKMMW.Core.Contracts.ModData;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HKMMW.Components;
public sealed partial class ModInfoControl : UserControl, INotifyPropertyChanged
{
    public IModInfo? Mod
    {
        get; set;
    }
    public IModInfoDownloadable? AsDownloadable => Mod as IModInfoDownloadable;
    public bool IsLocal => Mod is IModInfoLocal;
    public bool IsDownloadable => Mod is IModInfoDownloadable downloadable && downloadable.ModSize > 0;
    public bool IsVerifiable => Mod is IModInfoVerifiable;
    public ModInfoControl()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
