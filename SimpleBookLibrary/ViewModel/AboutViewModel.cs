using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBookLibrary.ViewModel
{
    public partial class AboutViewModel:ObservableObject
    {
        #region prop
        [ObservableProperty]
        private string _copyRight;
        [ObservableProperty]
        private string _version;
        #endregion
        public AboutViewModel()
        {
            CopyRight = $"Pluto Li 版权所有2024-{DateTime.Now.Year}。保留所有权力。";
            Version = $"版本:{Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
