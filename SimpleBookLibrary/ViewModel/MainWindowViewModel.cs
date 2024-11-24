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
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleBookLibrary.ViewModel
{
    public partial class MainWindowViewModel:ObservableObject
    {
        #region prop
        /// <summary>
        /// 搜索书名
        /// </summary>
        [ObservableProperty]
        private string _searchBookName;
        /// <summary>
        /// 搜索作者名
        /// </summary>
        [ObservableProperty]
        private string _searchAuthor;
        /// <summary>
        /// 搜索科室
        /// </summary>
        [ObservableProperty]
        private string _searchDepartment;
        /// <summary>
        /// 搜索借阅人
        /// </summary>
        [ObservableProperty]
        private string _searchBorrower;
        /// <summary>
        /// 搜索购买时间-开始
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBuyStartDate;
        /// <summary>
        /// 搜索购买时间-截至
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBuyEndDate;
        /// <summary>
        /// 搜索借阅时间-开始
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBorrowStartDate;
        /// <summary>
        /// 搜索借阅时间-截至
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchBorrowEndDate;
        /// <summary>
        /// 搜索归还时间-开始
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchReturnStartDate;
        /// <summary>
        /// 搜索归还时间-截至
        /// </summary>
        [ObservableProperty]
        private DateTime? _searchReturnEndDate;
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
        public RelayCommand<string> EditBookCommand { set; get; }
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
            EditBookCommand = new RelayCommand<string>(OnEditBookCommand);
        }

        private void OnEditBookCommand(string? obj)
        {
            try
            {
                var dlg = new AddBookView();
                if (dlg.ShowDialog().Value)
                {
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
                if (dlg.ShowDialog().Value)
                {
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
                var books = _bookService.SearchBooks(SearchBookName,SearchAuthor,SearchDepartment,SearchBorrower,SearchBuyStartDate,SearchBuyEndDate,
                    SearchBorrowStartDate, SearchBorrowEndDate,SearchReturnStartDate,SearchReturnEndDate);
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
