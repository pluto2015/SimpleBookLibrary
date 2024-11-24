﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleBookLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleBookLibrary.ViewModel
{
    public partial class AddBookViewModel:ObservableObject
    {
        #region prop
        /// <summary>
        /// 书名
        /// </summary>
        [ObservableProperty]
        private string _bookName;
        /// <summary>
        /// 书号
        /// </summary>
        [ObservableProperty]
        private string _code;
        /// <summary>
        /// 作者
        /// </summary>
        [ObservableProperty]
        private string _author;
        /// <summary>
        /// 价格
        /// </summary>
        [ObservableProperty]
        private double? _price;
        /// <summary>
        /// 数量
        /// </summary>
        [ObservableProperty]
        private int _count;
        /// <summary>
        /// 科室
        /// </summary>
        [ObservableProperty]
        private string _department;
        /// <summary>
        /// 备注
        /// </summary>
        [ObservableProperty]
        private string _remark;
        /// <summary>
        /// 购买时间
        /// </summary>
        [ObservableProperty]
        private DateTime? _buyTime;
        /// <summary>
        /// 出版社
        /// </summary>
        [ObservableProperty]
        private string _publisher;
        #endregion
        #region command
        public RelayCommand<Window> OkCommand { get; set; }
        public RelayCommand<Window > CancelCommand { get; set; }
        #endregion

        protected readonly ILogger<AddBookViewModel> _logger;
        protected readonly IBookService _bookService;
        public AddBookViewModel() {

            _logger = App.Current.ServiceProvider.GetService<ILogger<AddBookViewModel>>();
            _bookService = App.Current.ServiceProvider.GetService<IBookService>();

            InitCommand();
        }

        private void InitCommand()
        {
            OkCommand = new RelayCommand<Window>(OnOkCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        private void OnCancelCommand(Window? window)
        {
            if(MessageBox.Show("确认取消吗?","提示",MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            window.DialogResult = false;
            window.Close();
        }

        private void OnOkCommand(Window? window)
        {
            try
            {
                if(string.IsNullOrEmpty(BookName))
                {
                    MessageBox.Show("书名不能为空","提示");
                    return;
                }
                if (Count <= 0)
                {
                    MessageBox.Show("数量需要大于0","提示");
                    return;
                }
                if (MessageBox.Show("确认保存吗?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
                _bookService.AddBook(BookName, Author, Department, Price, BuyTime, Count, Code, Remark,Publisher);

                window.DialogResult = true;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }
    }
}