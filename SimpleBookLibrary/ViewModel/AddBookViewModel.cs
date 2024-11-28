using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleBookLibrary.Model;
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
        /// 标题
        /// </summary>
        [ObservableProperty]
        private string _title;
        /// <summary>
        /// 书
        /// </summary>
        [ObservableProperty]
        private BookModel _book = new BookModel();
        /// <summary>
        /// 提示信息
        /// </summary>
        [ObservableProperty]
        private string _information;
        #endregion
        #region command
        public RelayCommand<Window> OkCommand { get; set; }
        public RelayCommand<Window > CancelCommand { get; set; }
        #endregion

        protected readonly ILogger<AddBookViewModel> _logger;
        protected readonly IBookService _bookService;
        protected readonly IDepartmentService _departmentService;
        public AddBookViewModel() {

            _logger = App.Current.ServiceProvider.GetService<ILogger<AddBookViewModel>>();
            _bookService = App.Current.ServiceProvider.GetService<IBookService>();
            _departmentService = App.Current.ServiceProvider.GetService<IDepartmentService>();

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
                if(string.IsNullOrEmpty(Book.Name))
                {
                    MessageBox.Show("书名不能为空","提示");
                    return;
                }
                if (Book.Count <= 0)
                {
                    MessageBox.Show("数量需要大于0","提示");
                    return;
                }
                if (MessageBox.Show("确认保存吗?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
                if(string.IsNullOrEmpty(Book.Department))
                {
                    Book.DepartmentId = null;
                }
                else
                {
                   var department = _departmentService.GetDepartmentByName(Book.Department);
                    Book.DepartmentId = department.Id;
                }
                
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
