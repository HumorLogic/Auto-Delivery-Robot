﻿#pragma checksum "C:\Users\humor\Documents\Projects\Auto-Delivery-Robot\TestTool\Robot Test Tool\Scenario1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2F8355AB39A75C6A35AA5D36AC1CB9FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Robot_Test_Tool
{
    partial class Scenario1 : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Scenario1.xaml line 12
                {
                    this.DeviceListSource = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 3: // Scenario1.xaml line 128
                {
                    this.JoyStick = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // Scenario1.xaml line 129
                {
                    this.ControllerAera = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 5: // Scenario1.xaml line 136
                {
                    this.Controller = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                    ((global::Windows.UI.Xaml.Shapes.Ellipse)this.Controller).PointerPressed += this.Controller_PointerPressed;
                    ((global::Windows.UI.Xaml.Shapes.Ellipse)this.Controller).PointerReleased += this.Controller_PointerReleased;
                }
                break;
            case 6: // Scenario1.xaml line 146
                {
                    this.JoystickTransform = (global::Windows.UI.Xaml.Media.TranslateTransform)(target);
                }
                break;
            case 7: // Scenario1.xaml line 115
                {
                    this.StopBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.StopBtn).Click += this.StopBtn_Click;
                }
                break;
            case 8: // Scenario1.xaml line 98
                {
                    this.ForwardBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ForwardBtn).Click += this.ForwardBtn_Click;
                }
                break;
            case 9: // Scenario1.xaml line 105
                {
                    this.BackwardBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BackwardBtn).Click += this.BackwardBtn_Click;
                }
                break;
            case 10: // Scenario1.xaml line 74
                {
                    this.SerialConnectBtn = (global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target);
                    ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)this.SerialConnectBtn).Click += this.SerialConnectBtn_Click;
                }
                break;
            case 11: // Scenario1.xaml line 91
                {
                    this.TipText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Scenario1.xaml line 54
                {
                    this.BaudRateComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.BaudRateComboBox).SelectionChanged += this.BaudRateComboBox_SelectionChanged;
                }
                break;
            case 13: // Scenario1.xaml line 34
                {
                    this.PortNameComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.PortNameComboBox).SelectionChanged += this.PortNameComboBox_SelectionChanged;
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.PortNameComboBox).DropDownOpened += this.PortNameComboBox_DropDownOpened;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

