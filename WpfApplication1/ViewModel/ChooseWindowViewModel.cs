using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Abstract;
using ComunalPay.Infrastructure.Concrete;
using ComunalPay.UI.Infrastructure;
using ComunalPay.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComunalPay.UI.ViewModel
{
    class ChooseWindowViewModel: InotifyImplement
    {
        #region Prop
        ISaver<dynamic> save;
        List<Property> propList { get; set; }

        #region ComboBox
        string selStreet;
        public string SelStreet
        {
            get { return selStreet; }
            set
            {
                selStreet = value;
                if (selStreet != null)
                {
                    CreatingAddressList();
                    SelAddress = addressList[0];
                }
                OnPropertyChanged("SelStreet");
            }
        }
        public List<String> StreetList { get; set; }

        string selAddress;
        public string SelAddress
        {
            get { return selAddress; }
            set
            {
                selAddress = value;
                OnPropertyChanged("SelAddress");
            }
        }

        List<string> addressList;
        public List<String> AddressList
        {
            get
            {
                return addressList;
            }
            set
            {
                addressList = value;

                OnPropertyChanged("AddressList");
            }
        }
        #endregion

        public ICommand NewPaymantCommand { get; set; }
        public ICommand PaymantsStatCommand { get; set; }
        public ICommand AddPropertyCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }

        #endregion

        public ChooseWindowViewModel()
        {
            propList = new List<Property>();
            if (!DI.FilesManager.IsEmpty())
            {
                List<string> files = DI.FilesManager.GetFilesName().ToList();
                foreach (var file in files)
                {
                    if (!DI.Saver.IsEmpty(file))
                    {
                        propList.Add(DI.Saver.GetInfo(new Property(), file));
                    }
                }

                FillLists();
                SetFirstValues();

                
            }
            NewPaymantCommand = new CommandClass(arg => AddNewPaymentMethod());
            AddPropertyCommand = new CommandClass(arg => AddPropertyMethod());
        }

        

        /// <summary>
        /// Fill all lists
        /// </summary>
        private void FillLists()
        {
            StreetList = propList.Select(x => x.Street).Distinct().ToList();
            AddressList = propList.Select(x => x.Address).Distinct().ToList();
        }

        /// <summary>
        /// Начальные значения
        /// </summary>
        private void SetFirstValues()
        {
            SelStreet = propList[0].Street;      
            SelAddress = propList[0].Address;
        }

        void CreatingAddressList()
        {
            AddressList = propList.Where(z => z.Street == SelStreet).Select(x => x.Address).ToList();
        }


        #region Methods
        void AddNewPaymentMethod()
        {
            EditPaymantView view = new EditPaymantView();
            ChooseWindowViewModel vm = new ChooseWindowViewModel();
            view.DataContext = vm;
            view.Show();
        }
        void AddPropertyMethod()
        {
            PropertyAddView view = new PropertyAddView();
            PropertyAddViewModel vm = new PropertyAddViewModel();
            view.DataContext = vm;
            view.Show();
        } 
        #endregion
    }
}
