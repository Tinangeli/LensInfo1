﻿#pragma checksum "..\..\..\..\AddDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3DB061F370827E0C9173F4688990042D7421A59E"
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
    /// AddDialog
    /// </summary>
    public partial class AddDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox TextBoxFirstName;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox TextBoxLastName;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox TextBoxPhoneNum;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox TextBoxPosition;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox TextBoxUsername;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TextInputBox TextBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TopButton AddRecordButton;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\AddDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LensInfo1.UserControls.TopButton CancelButton;
        
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
            System.Uri resourceLocater = new System.Uri("/LensInfo1;component/adddialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\AddDialog.xaml"
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
            this.TextBoxFirstName = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 2:
            this.TextBoxLastName = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 3:
            this.TextBoxPhoneNum = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 4:
            this.TextBoxPosition = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 5:
            this.TextBoxUsername = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 6:
            this.TextBoxPassword = ((LensInfo1.UserControls.TextInputBox)(target));
            return;
            case 7:
            this.AddRecordButton = ((LensInfo1.UserControls.TopButton)(target));
            
            #line 60 "..\..\..\..\AddDialog.xaml"
            this.AddRecordButton.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.AddRecordButton_Click));
            
            #line default
            #line hidden
            return;
            case 8:
            this.CancelButton = ((LensInfo1.UserControls.TopButton)(target));
            
            #line 61 "..\..\..\..\AddDialog.xaml"
            this.CancelButton.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.CancelButton_Click));
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

