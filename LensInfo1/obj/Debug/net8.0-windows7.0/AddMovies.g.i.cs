﻿#pragma checksum "..\..\..\AddMovies.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F1DDA14DADCC27E83049C8195BBEF57E7D758E6F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LensInfo1;
using LensInfo1.UserControls;
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


namespace LensInfo1 {
    
    
    /// <summary>
    /// AddMovies
    /// </summary>
    public partial class AddMovies : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox InputTextMovieName;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox InputTextAgeLimit;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.InputTextBoxTime InputTextDuration;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox InputTextMovieGenre;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MovieImage;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAddMovieImage;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TopButton AddButtonMovies;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\AddMovies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TopButton AddButtonCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LensInfo1;component/addmovies.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddMovies.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.InputTextMovieName = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 2:
            this.InputTextAgeLimit = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 3:
            this.InputTextDuration = ((LensInfo1.UserControls.InputTextBoxTime)(target));
            return;
            case 4:
            this.InputTextMovieGenre = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 5:
            this.MovieImage = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.ButtonAddMovieImage = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\AddMovies.xaml"
            this.ButtonAddMovieImage.Click += new System.Windows.RoutedEventHandler(this.ButtonAddMovieImage_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AddButtonMovies = ((LensInfo1.UserControls.TopButton)(target));
            
            #line 52 "..\..\..\AddMovies.xaml"
            this.AddButtonMovies.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.AddButtonMovies_Click));
            
            #line default
            #line hidden
            return;
            case 8:
            this.AddButtonCancel = ((LensInfo1.UserControls.TopButton)(target));
            
            #line 53 "..\..\..\AddMovies.xaml"
            this.AddButtonCancel.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.AddButtonCancel_Click));
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

