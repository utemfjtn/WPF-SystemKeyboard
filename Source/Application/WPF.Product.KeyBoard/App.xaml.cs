﻿using WPF.Product.UserControls.SystemKeyBoard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Product.KeyBoard
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            Application.Current.Dispatcher.Invoke(() => MessageBox.Show(e.Exception.Message));

            e.Handled = true;
        }


        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();

            var p = Process.GetProcessesByName(thisProc.ProcessName);

            if (p.Length > 1)
            {
                Environment.Exit(0);

                return;
            }

            if(e.Args != null && e.Args.Length > 0 && e.Args[0].Length > 0)
            {
                if(e.Args[0].ToUpper().Contains("KEYBOARD"))
                {
                    KeyBoardWindow.ShowDefault(() => Environment.Exit(0));  //Defalut
                }
                else if (e.Args[0].ToUpper().Contains("NUMBER"))
                {
                    InputNumWindow.ShowDefault(() => Environment.Exit(0));  //Defalut
                }
            }
            else
                KeyBoardWindow.ShowDefault(()=> Environment.Exit(0));  //Defalut
        }
    }
}
