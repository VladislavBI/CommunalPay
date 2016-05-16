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
        List<Property> propList { get; set; }

        #region ComboBox
        string selStreet;
        public string SelStreet
        {
            get { return selStreet; }
            set
            {
                selStreet = value;
                
                OnPropertyChanged("SelStreet");
            }
        }
        List<String> streetList;
        public List<String> StreetList
        {
            get
            {
                return streetList;
            }
            set
            {
                streetList = value;
                OnPropertyChanged("StreetList");
            }
        }

        string selAddress;
        public string SelAddress
        {
            get { return selAddress; }
            set
            {
                selAddress = value;
                if (selAddress != null)
                {
                    CreatingStreetList();
                    SelStreet = StreetList[0];
                }
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

        #region Commands
        public ICommand NewPaymantCommand { get; set; }
        public ICommand PaymantsStatCommand { get; set; }
        public ICommand AddPropertyCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; } 
        #endregion

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
                

                propList.RemoveAll(x => x == null);
                SetFirstValues();
                FillLists();

                
            }
            NewPaymantCommand = new CommandClass(arg => AddNewPaymentMethod());
            AddPropertyCommand = new CommandClass(arg => AddPropertyMethod());
            PaymantsStatCommand = new CommandClass(arg => PaymentStatMethod());
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
            SelAddress = propList[0].Address;
        }

        void CreatingStreetList()
        {
            StreetList = propList.Where(z => z.Address == SelAddress).Select(x => x.Street).ToList();
        }


        #region Methods
        void AddNewPaymentMethod()
        {
            EditPaymantView view = new EditPaymantView();
            NewPaymentViewModel vm = new NewPaymentViewModel(selStreet, selAddress);
            view.DataContext = vm;
            view.Show();
        }
        void AddPropertyMethod()
        {
            PropertyAddView view = new PropertyAddView();

            int i = propList.Select(x => x.IdProp).Max();

            PropertyAddViewModel vm = new PropertyAddViewModel(i);
            view.DataContext = vm;
            view.Show();
        }

        void PaymentStatMethod()
        {
            StatisticWindowView view = new StatisticWindowView();
            StatisticWindowViewModel vm = new StatisticWindowViewModel(selStreet, selAddress);
            view.DataContext = vm;
            view.Show();
        }
        #endregion
    }
}
