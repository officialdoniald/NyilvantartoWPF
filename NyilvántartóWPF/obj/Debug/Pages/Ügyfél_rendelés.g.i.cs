﻿#pragma checksum "..\..\..\Pages\Ügyfél_rendelés.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76D17B1D771C8C4481C96CCD3F2A732B11EE2AAB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NyilvántartóWPF;
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


namespace NyilvántartóWPF {
    
    
    /// <summary>
    /// Ügyfél_rendelés
    /// </summary>
    public partial class Ügyfél_rendelés : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame _NavigationFrame;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ügyfélkiválasztós_combobox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox anyagnévválasztós_combobox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mennyiség_textbox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox egységár_textbox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox áfatartalom_textbox;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker rendelés_dátuma_datepicker;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Pages\Ügyfél_rendelés.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ügyfél_rendelés_leadása_button;
        
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
            System.Uri resourceLocater = new System.Uri("/NyilvántartóWPF;component/pages/%c3%9cgyf%c3%a9l_rendel%c3%a9s.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Ügyfél_rendelés.xaml"
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
            this.ügyfélkiválasztós_combobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 34 "..\..\..\Pages\Ügyfél_rendelés.xaml"
            this.ügyfélkiválasztós_combobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ügyfélkiválasztós_combobox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.anyagnévválasztós_combobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 41 "..\..\..\Pages\Ügyfél_rendelés.xaml"
            this.anyagnévválasztós_combobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.anyagnévválasztós_combobox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mennyiség_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.egységár_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.áfatartalom_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.rendelés_dátuma_datepicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.ügyfél_rendelés_leadása_button = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Pages\Ügyfél_rendelés.xaml"
            this.ügyfél_rendelés_leadása_button.Click += new System.Windows.RoutedEventHandler(this.ügyfél_rendelés_leadása_button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

