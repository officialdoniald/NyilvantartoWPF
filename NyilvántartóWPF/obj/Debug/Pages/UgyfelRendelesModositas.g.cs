﻿#pragma checksum "..\..\..\Pages\UgyfelRendelesModositas.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B3A0DFD786AB016B29C863F536A820D011641247"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NyilvántartóWPF.Pages;
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


namespace NyilvántartóWPF.Pages {
    
    
    /// <summary>
    /// UgyfelRendelesModositas
    /// </summary>
    public partial class UgyfelRendelesModositas : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame _NavigationFrame;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox rendelesek_listazasa_listbox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ugyfelnev_textbox;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox anyagnév_textbox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mennyiség_textbox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dátum_textbox;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button modosit_button;
        
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
            System.Uri resourceLocater = new System.Uri("/NyilvántartóWPF;component/pages/ugyfelrendelesmodositas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
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
            this._NavigationFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.rendelesek_listazasa_listbox = ((System.Windows.Controls.ListBox)(target));
            
            #line 23 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
            this.rendelesek_listazasa_listbox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.rendelesek_listazasa_listbox_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ugyfelnev_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.anyagnév_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.mennyiség_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.dátum_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.modosit_button = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\Pages\UgyfelRendelesModositas.xaml"
            this.modosit_button.Click += new System.Windows.RoutedEventHandler(this.modosit_button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

