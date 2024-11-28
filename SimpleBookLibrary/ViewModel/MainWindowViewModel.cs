using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.LayoutRenderers.Wrappers;
using SimpleBookLibrary.Model;
using SimpleBookLibrary.Service;
using SimpleBookLibrary.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace SimpleBookLibrary.ViewModel
{
    public partial class MainWindowViewModel:ObservableObject
    {
        #region prop
        /// <summary>
        /// 搜索条件
        /// </summary>
        [ObservableProperty]
        private SearchBookModel _searchBook = new SearchBookModel();
        /// <summary>
        /// 搜索到的图书
        /// </summary>
        public ObservableCollection<BookModel> SearchedBooks { get; set; } = new ObservableCollection<BookModel>();

        private BookModel _selectedBook;
        public BookModel SelectedBook
        {
            set
            {
                _selectedBook = value;
                SetProperty(ref _selectedBook, value);
                OnBookSelected(value);
            }
            get
            {
                return _selectedBook;
            }
        }
        /// <summary>
        /// 借阅记录-未归还
        /// </summary>
        public ObservableCollection<BorrowHistoryModel> BorrowHistoriesNotReturn { get; set; } = new ObservableCollection<BorrowHistoryModel>();
        /// <summary>
        /// 借阅记录-已归还
        /// </summary>
        public ObservableCollection<BorrowHistoryModel> BorrowHistoriesReturned { get; set; } = new ObservableCollection<BorrowHistoryModel>();
        #endregion
        #region command
        public RelayCommand BorrowCommand { set; get; }
        public RelayCommand SearchCommand { set; get; }
        public RelayCommand AboutCommand { set; get; }
        public RelayCommand AddBookCommand { set; get; }
        public RelayCommand<BookModel> EditBookCommand { set; get; }
        #endregion
        protected readonly ILogger<MainWindowViewModel> _logger;
        protected readonly IBookService _bookService;
        protected readonly IMapper _mapper;
        protected readonly IBorrowHistoryService _borrowHistoryService;
        public MainWindowViewModel() 
        {
            _logger = App.Current.ServiceProvider.GetService<ILogger<MainWindowViewModel>>();
            _bookService = App.Current.ServiceProvider.GetService<IBookService>();
            _mapper = App.Current.ServiceProvider.GetService<IMapper>();
            _borrowHistoryService = App.Current.ServiceProvider.GetService<IBorrowHistoryService>();

            InitCommand();
        }

        void InitCommand()
        {
            BorrowCommand = new RelayCommand(OnBorrowCommand);
            SearchCommand = new RelayCommand(OnSearchCommand);
            AboutCommand = new RelayCommand(OnAboutCommand);
            AddBookCommand = new RelayCommand(OnAddBookCommand);
            EditBookCommand = new RelayCommand<BookModel>(OnEditBookCommand);
        }

        private void OnEditBookCommand(BookModel? book)
        {
            try
            {
                var dlg = new AddBookView();
                var vm = dlg.DataContext as AddBookViewModel;
                vm.Book = book;
                vm.Title = "编辑图书";
                if (dlg.ShowDialog().Value)
                {
                    _bookService.EditBook(book);
                    OnSearchCommand();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OnAddBookCommand()
        {
            try
            {
                var dlg = new AddBookView();
                var vm = dlg.DataContext as AddBookViewModel;
                vm.Title = "新增图书";
                vm.Information = "注意!按照如下逻辑新增:\n" +
                    "1.按照书名去重,书名相同的认为是同一本书.\n" +
                    "2.涉及英文和拼音的不区分大小写。\n" +
                    "3.数据库里已有的叠加数量，没有的新增";
                if (dlg.ShowDialog().Value)
                {
                    _bookService.AddBook(vm.Book.Name, vm.Book.Author, vm.Book.Department, vm.Book.Price, vm.Book.PurchaseDateTime,
                        vm.Book.Count, vm.Book.Code, vm.Book.Remark, vm.Book.Publisher);
                    OnSearchCommand();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OnAboutCommand()
        {
            try
            {
                var dlg = new AboutView();
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OnSearchCommand()
        {
            try
            {
                var books = _bookService.SearchBooks(SearchBook);
                SearchedBooks.Clear();
                foreach (var book in books)
                {
                    var bookModel = _mapper.Map<BookModel>(book);
                    SearchedBooks.Add(bookModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(),ex.Message);
            }
        }

        private void OnBorrowCommand()
        {
            var dlg = new BorrowView();
            dlg.ShowDialog();
        }

        private void OnBookSelected(BookModel book)
        {
            try
            {
                if(book == null)
                {
                    return;
                }

                BorrowHistoriesNotReturn.Clear();
                BorrowHistoriesReturned.Clear();
                var histories = _borrowHistoryService.SearchBorrowHistory(book.Name);
                foreach (var history in histories)
                {
                    if(history.ReturnDateTime == null)
                    {
                        BorrowHistoriesNotReturn.Add(_mapper.Map<BorrowHistoryModel>(history));
                    }else
                    {
                        BorrowHistoriesReturned.Add(_mapper.Map<BorrowHistoryModel>(history));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }
    }
}
