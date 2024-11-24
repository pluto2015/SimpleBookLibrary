using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleBookLibrary.ViewModel
{
    public class BorrowViewModel:ObservableObject
    {
        #region command
        public RelayCommand<Window> SaveCommand { set; get; }
        public RelayCommand<Window> CancelCommand { set; get; }
        #endregion
        protected readonly ILogger<BorrowViewModel> _logger;
        public BorrowViewModel()
        {
            _logger = App.Current.ServiceProvider.GetService<ILogger<BorrowViewModel>>();

            InitCommand();
        }

        void InitCommand()
        {
            SaveCommand = new RelayCommand<Window>(OnSaveCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
        }

        private void OnCancelCommand(Window? window)
        {
            try
            {
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OnSaveCommand(Window? window)
        {
            try
            {
                var dlgRes = MessageBox.Show("确认保存吗？", "提示", MessageBoxButton.OKCancel);
                if (dlgRes != MessageBoxResult.OK)
                {
                    return;
                }

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
