using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Abstract;
using ComunalPay.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComunalPay.UI.ViewModel
{
    public class MainWindowViewModel: InotifyImplement
    {

        #region Prop
        ISaver save;
        List<EasyPay> payList { get; set; }
        DataTable pdt;
        public DataTable PayDataTable
        {
            get { return pdt; }
            set
            {

                if (pdt != value)
                    pdt = value;
                OnPropertyChanged("PayDataTable");
            }
        }


        string selCity;
        public string SelCity
        {
            get { return selCity; }
            set
            {
                selCity = value;
                if (selCity != null)
                {
                    CreatingAddressList();
                }
                OnPropertyChanged("SelCity");
            }
        }
        public List<String> CityList { get; set; }

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
        public List<String> AddressList {
            get
            {
                return addressList;
            }
            set { addressList = value;
                OnPropertyChanged("AddressList");
            } }


        string selService;
        public string SelService
        {
            get { return selService; }
            set
            {
                selService = value;
                OnPropertyChanged("SelCity");
            }
        }
        public List<String> ServiceList { get; set; }


        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                if (dateTo > value)
                    dateFrom = value;
                else
                {
                    dateFrom = dateTo;
                }
                OnPropertyChanged("DateFrom");
            }
        }

        private DateTime dateTo;
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }

        #region FilterChB

        bool chBCity;
        public bool ChBCity
        {
            get { return chBCity; }
            set
            {
                chBCity = value;
                if (chBCity != null)
                {
                    CreatingAddressList();
                }
                OnPropertyChanged("ChBCity");
            }
        }

        bool chBAddress;
        public bool ChBAddress
        {
            get { return chBAddress; }
            set
            {
                chBAddress = value;
                if (chBAddress != null)
                {
                    CreatingAddressList();
                }
                OnPropertyChanged("ChBAddress");
            }
        }

        bool chBService;
        public bool ChBService
        {
            get { return chBService; }
            set
            {
                chBService = value;
                if (chBService != null)
                {
                    CreatingAddressList();
                }
                OnPropertyChanged("ChBService");
            }
        }

        bool chBDates;
        public bool ChBDates
        {
            get { return chBDates; }
            set
            {
                chBDates = value;
                OnPropertyChanged("ChBDates");
            }
        }
        #endregion
        #region Commands
        public ICommand filterCommand { get; set; }
        public ICommand clearFilterCommand { get; set; } 
        #endregion
        #endregion

        public MainWindowViewModel()
        {}
        public MainWindowViewModel(ISaver s, List<EasyPay> lp)
        {
            
            save = s;
            if(!s.IsEmpty()){
                payList = s.GetInfo(lp.GetType());
            }
            else
            {
                payList = lp; 
                s.SaveInfo(payList);

            }
            CreateDataTable();
            FillDataTable(payList);
            FillLists();
            SetFirstValues();

            filterCommand = new CommandClass(arg => AcceptFilter());
            clearFilterCommand = new CommandClass(arg => ClearFilter());
        }

        /// <summary>
        /// Начальные значения
        /// </summary>
        private void SetFirstValues()
        {
            SelCity = payList[0].City;
            SelService = payList[0].PaymentType;
            SelAddress = payList[0].Street;
            dateFrom = DateTime.Now;
            dateTo = DateTime.Now;
        }

        /// <summary>
        /// Fill all lists
        /// </summary>
        private void FillLists()
        {
            CityList = payList.Select(x => x.City).Distinct().ToList();
            ServiceList = payList.Select(x => x.PaymentType).Distinct().ToList();
            AddressList= payList.Select(x => x.Street).Distinct().ToList();
        }

        /// <summary>
        /// Create DT for dataGrid
        /// </summary>
        private void CreateDataTable()
        {
            PayDataTable = new DataTable();
            PayDataTable.Columns.Add("Адрес");
            PayDataTable.Columns.Add("Квартира");
            PayDataTable.Columns.Add("Услуга");
            PayDataTable.Columns.Add("Показания", typeof(float));
            PayDataTable.Columns.Add("Оплата", typeof(decimal));
            PayDataTable.Columns.Add("Положено", typeof(decimal));
            PayDataTable.Columns.Add("Разница", typeof(decimal));
            PayDataTable.Columns.Add("Дата платежа", typeof(DateTime));
        }

        /// <summary>
        /// Filling dt for dataGrid
        /// </summary>
        /// <param name="t">incoming list (for filters)</param>
        void FillDataTable(IEnumerable<EasyPay> t)
        {
            PayDataTable.Rows.Clear();

            decimal fullPayed = 0;
            decimal fullPut = 0;
            decimal fullDifference = 0;
            float ReadingsDifference = 0;

            foreach (var tmp in t)
            {
                DataRow row = PayDataTable.NewRow();
                row[0] = tmp.City;
                row[1] = tmp.Street;
                row[2] = tmp.PaymentType;
                row[3] = tmp.Readings;
                row[4] = tmp.GetPaymant;
                row[5] = tmp.PutOnAccount;
                row[6] = tmp.Difference;
                row[7] = tmp.PayDate;

                fullPayed += tmp.GetPaymant;
                fullPut += tmp.PutOnAccount;
                fullDifference += tmp.Difference;

                PayDataTable.Rows.Add(row);
            }

            ReadingsDifference = Convert.ToSingle(PayDataTable.Rows[PayDataTable.Rows.Count - 1][3]) - Convert.ToSingle(PayDataTable.Rows[0][3]);

            DataRow row2 = PayDataTable.NewRow();
            row2[0] = "Всего:";
            row2[3] = ReadingsDifference;
            row2[4] = fullPayed;
            row2[5] = fullPut;
            row2[6] = fullDifference;
            PayDataTable.Rows.Add(row2);

        }

        void CreatingAddressList()
        {
            AddressList = payList.Where(z => z.City == SelCity).Select(x => x.Street).ToList();
        }


        #region Methods
        void AcceptFilter()
        {
            List<EasyPay> epList=new List<EasyPay>();
            if (ChBCity)
            {
                if (ChBAddress)
                {
                    epList = payList.Where(x => x.City == SelCity & x.Street == SelAddress).ToList();
                }
                else
                {
                    epList = payList.Where(x => x.City == SelCity).ToList();
                }  
            }
            if (ChBService)
            {
                epList = payList.Where(x => x.PaymentType == SelService).ToList();
            }
            if (ChBDates)
            {
                epList = payList.Where(x => x.PayDate <= DateTo && x.PayDate>=DateFrom).ToList();
            }

            //Проверка на пустой фильтр
            if (epList.Any())
            {
                FillDataTable(epList);
            }
        }
        void ClearFilter()
        {
            FillDataTable(payList);
            ChBAddress = false;
            ChBCity = false;
            ChBService = false;
            ChBDates = false;
        } 
        #endregion
    }
}
