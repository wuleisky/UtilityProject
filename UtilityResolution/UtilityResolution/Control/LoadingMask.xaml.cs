using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace System.Windows.Controls
{
    public partial class LoadingMask : UserControl,INotifyPropertyChanged
    {
        public LoadingMask()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void UpdateLoadingInfo(string percent,string name)
        {
            pr.LoadingPercent = percent;
            pr.LoadingName = name;
            this.LoadingName = name;
        }


        private string _LoadingName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string LoadingName
        {
            get => _LoadingName;
            set
            {
                _LoadingName = value;
                UpdateProperty();
            }
        }


        public void UpdateProperty([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Brush Back 
        {
            get 
            {
                return this.grid.Background;
            }
            set 
            {
                this.grid.Background = value;
            }
        }
    }
}