﻿#pragma checksum "..\..\..\WisielecOknoPoczatkowe.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B411AB0A3A2CB8FE7105489B6764A7E9775DAE8F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using Wisielec;


namespace Wisielec {
    
    
    /// <summary>
    /// WisielecOkno
    /// </summary>
    public partial class WisielecOkno : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\WisielecOknoPoczatkowe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BWrocMenu;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\WisielecOknoPoczatkowe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmBWyborKategori;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\WisielecOknoPoczatkowe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Bstart;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\WisielecOknoPoczatkowe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmBWyborTrudnosci;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\WisielecOknoPoczatkowe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtNazwaGracza;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Wisielec;V1.0.0.0;component/wisielecoknopoczatkowe.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WisielecOknoPoczatkowe.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BWrocMenu = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\WisielecOknoPoczatkowe.xaml"
            this.BWrocMenu.Click += new System.Windows.RoutedEventHandler(this.BWrocMenu_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CmBWyborKategori = ((System.Windows.Controls.ComboBox)(target));
            
            #line 16 "..\..\..\WisielecOknoPoczatkowe.xaml"
            this.CmBWyborKategori.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmBWyborKategori_SelectionIndexChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Bstart = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\WisielecOknoPoczatkowe.xaml"
            this.Bstart.Click += new System.Windows.RoutedEventHandler(this.Bstart_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CmBWyborTrudnosci = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\WisielecOknoPoczatkowe.xaml"
            this.CmBWyborTrudnosci.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmBWyborTrudnosci_SelectionIndexChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TxtNazwaGracza = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\WisielecOknoPoczatkowe.xaml"
            this.TxtNazwaGracza.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

