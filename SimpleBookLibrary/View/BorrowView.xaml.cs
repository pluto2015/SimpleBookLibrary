﻿using MahApps.Metro.Controls;
using SimpleBookLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleBookLibrary.View
{
    /// <summary>
    /// BorrowView.xaml 的交互逻辑
    /// </summary>
    public partial class BorrowView : MetroWindow
    {
        public BorrowView()
        {
            InitializeComponent();
            DataContext = new BorrowViewModel();
        }

        private void ComboBox_book_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
