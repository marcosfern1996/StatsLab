﻿#pragma checksum "..\..\SettingWindows.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "28A95773C85A32C73225F670C7B5155F79DB6FF97BBDFD1647F408AF709DD08E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// SettingWindows
    /// </summary>
    public partial class SettingWindows : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortTXT;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTXT;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameMicrophone;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameSourse;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Conectar;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChanelName;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Datos;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\SettingWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Monitoreo;
        
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
            System.Uri resourceLocater = new System.Uri("/StatsLab;component/settingwindows.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SettingWindows.xaml"
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
            
            #line 12 "..\..\SettingWindows.xaml"
            ((StatsLab.SettingWindows)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 44 "..\..\SettingWindows.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 45 "..\..\SettingWindows.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PortTXT = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PasswordTXT = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.NameMicrophone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.NameSourse = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Conectar = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\SettingWindows.xaml"
            this.Conectar.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ChanelName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.Datos = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\SettingWindows.xaml"
            this.Datos.Click += new System.Windows.RoutedEventHandler(this.Datos_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Monitoreo = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\SettingWindows.xaml"
            this.Monitoreo.Click += new System.Windows.RoutedEventHandler(this.Monitoreo_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 111 "..\..\SettingWindows.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler(this.Hyperlink_RequestNavigate);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
