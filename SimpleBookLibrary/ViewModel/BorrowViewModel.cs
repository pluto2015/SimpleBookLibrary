using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleBookLibrary.Model;
using SimpleBookLibrary.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SimpleBookLibrary.ViewModel
{
    public partial class BorrowViewModel:ObservableObject
    {
        #region prop
        [ObservableProperty]
        private ObservableCollection<BookModel> _books = new ObservableCollection<BookModel>();
        public BookModel Book;
        [ObservableProperty]
        private string _bookStr;
        [ObservableProperty]
        private ObservableCollection<BorrowerModel> _borrowers = new ObservableCollection<BorrowerModel>();
        public BorrowerModel Borrower;
        [ObservableProperty]
        private string _borrowerStr;
        [ObservableProperty]
        private int _Count = 0;
        [ObservableProperty]
        private bool _IsBookNameDropDown = false;
        [ObservableProperty]
        private bool _IsBorrowerDropDown = false;
        #endregion
        #region command
        public RelayCommand<Window> SaveCommand { set; get; }
        public RelayCommand<Window> CancelCommand { set; get; }
        public RelayCommand<string> SearchBookCommand { set; get; }
        public RelayCommand<string> SearchBorrowerCommand { set; get; }
        #endregion
        protected readonly ILogger<BorrowViewModel> _logger;
        protected readonly IBookService _bookService;
        protected readonly IBorrowHistoryService _borrowerHistoryService;
        protected readonly IMapper _mapper;
        protected readonly IBorrowerService _borrowerService;
        public BorrowViewModel()
        {
            _logger = App.Current.ServiceProvider.GetService<ILogger<BorrowViewModel>>();
            _bookService = App.Current.ServiceProvider.GetService<IBookService>();
            _borrowerHistoryService = App.Current.ServiceProvider.GetService<IBorrowHistoryService>();
            _mapper = App.Current.ServiceProvider.GetService<IMapper>();
            _borrowerService = App.Current.ServiceProvider.GetService<IBorrowerService>();

            InitCommand();
        }

        void InitCommand()
        {
            SaveCommand = new RelayCommand<Window>(OnSaveCommand);
            CancelCommand = new RelayCommand<Window>(OnCancelCommand);
            SearchBookCommand = new RelayCommand<string>(OnSearchBookCommand);
            SearchBorrowerCommand = new RelayCommand<string>(OnSearchBorrowerCommand);
        }

        private void OnSearchBorrowerCommand(string name)
        {
            try
            {
                Borrowers.Clear();
                if (string.IsNullOrEmpty(name))
                {
                    return;
                }
                var borrowerEntiies = _borrowerService.SearchBorrowers(name);
                Borrowers = new ObservableCollection<BorrowerModel>(borrowerEntiies.Select(x => _mapper.Map<BorrowerModel>(x)));

                IsBorrowerDropDown = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OnSearchBookCommand(string name)
        {
            try
            {
                Books.Clear();
                if (string.IsNullOrEmpty(name))
                {
                    return;
                }
                var bookEntities = _bookService.SearchBooks(new SearchBookModel { SearchBookName = name });
                Books = new ObservableCollection<BookModel>(bookEntities.Select(x => _mapper.Map<BookModel>(x)));

                IsBookNameDropDown = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OnCancelCommand(Window? window)
        {
            try
            {
                window.DialogResult = false;
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
                var book = Book;
                var dlgRes = MessageBox.Show("确认保存吗？", "提示", MessageBoxButton.OKCancel);
                if (dlgRes != MessageBoxResult.OK)
                {
                    return;
                }

                if (string.IsNullOrEmpty(BookStr))
                {
                    MessageBox.Show("请输入图书", "出错了");
                    return;
                }
                var bookEntity = _bookService.GetBookByName(BookStr);
                if (bookEntity == null)
                {
                    MessageBox.Show("输入的图书不存在", "出错了");
                    return;
                }

                Book = _mapper.Map<BookModel>(bookEntity);

                if(string.IsNullOrEmpty(BorrowerStr))
                {
                    MessageBox.Show("请输入借阅人", "出错了");
                    return;
                }
                var borrowerEntity = _borrowerService.AddBorrower(BorrowerStr);
                Borrower = _mapper.Map<BorrowerModel>(borrowerEntity);

                if(Count <=0)
                {
                    MessageBox.Show("借阅数量需要大于0", "出错了");
                    return;
                }
                _borrowerHistoryService.AddBorrowHistory(Book.Id, Borrower.Id, Count);

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
