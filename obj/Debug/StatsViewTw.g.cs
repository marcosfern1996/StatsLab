﻿#pragma checksum "..\..\StatsViewTw.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6B101D7718E73C6301025B808290FA925AA2EFB87A107900B257C9FA3BB99FC9"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Web.WebView2.Wpf;
using StatsLab;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace StatsLab {
    
    
    /// <summary>
    /// StatsViewTw
    /// </summary>
    public partial class StatsViewTw : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal StatsLab.StatsViewTw MiventanaChat;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image CandadoA;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image CandadoC;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBlock;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NumViewers;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\StatsViewTw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse InDirect;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StatsLab;component/statsviewtw.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StatsViewTw.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MiventanaChat = ((StatsLab.StatsViewTw)(target));
            
            #line 18 "..\..\StatsViewTw.xaml"
            this.MiventanaChat.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DragWindow);
            
            #line default
            #line hidden
            
            #line 19 "..\..\StatsViewTw.xaml"
            this.MiventanaChat.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.TuVentana_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Close = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\StatsViewTw.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.ClosedButton);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CandadoA = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.CandadoC = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.ButtonBlock = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\StatsViewTw.xaml"
            this.ButtonBlock.Click += new System.Windows.RoutedEventHandler(this.BlockButton);
            
            #line default
            #line hidden
            return;
            case 6:
            this.NumViewers = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.InDirect = ((System.Windows.Shapes.Ellipse)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

