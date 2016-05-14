using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Concrete;
using ComunalPay.Model.Concrete;
using ComunalPay.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComunalPay.UI.ViewModel
{
    class StatisticWindowViewModel: InotifyImplement
    {
        #region Prop
        /// <summary>
        /// текущая недвижимость
        /// </summary>
        Property curProp;
        /// <summary>
        /// название файла недвижимости
        /// </summary>
        string propFileName;

        private DataRowView selDataGridRow ;

        public DataRowView SelDataGridRow 
        {
            get { return selDataGridRow; }
            set
            {
                if (selDataGridRow != value)
                    selDataGridRow = value;
                OnPropertyChanged("SelDataGridRow");
            }
        }

        public ObservableCollection<string> AllPaymentsT { get; set; }
        DataTable paymantsTable;
        public DataTable PaymantsTable
        {
            get { return paymantsTable; }
            set
            {

                if (paymantsTable != value)
                    paymantsTable = value;
                OnPropertyChanged("PaymantsTable");
            }
        }
        public PaymentTypes PaymentT { get; set; }
        public string curFullAddress
        {
            get
            {
                if (curProp != null)
                    return curProp.Address + " " + curProp.Street;
                else
                {
                    return null;
                }
            }
            set { curFullAddress = value; }
        }

        private string fullInfo;

        public string FullInfo
        {
            get { return fullInfo; }
            set
            { fullInfo = value;
                OnPropertyChanged("FullInfo");
            }
        }


        #region Filter
        DateTime from;
        public DateTime From
        {
            get { return from; }
            set
            {
                if (value > to)
                {
                    from = to;
                }
                else
                {
                    from = value;
                }
                OnPropertyChanged("From");
            }
        }

        DateTime to;
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged("To");
            }
        }

        private string selType;

        public string SelType
        {
            get { return selType; }
            set
            {
                selType = value;
                OnPropertyChanged("SelType");
            }
        }

        private bool chBDate;

        public bool ChBDate
        {
            get { return chBDate; }
            set
            {
                chBDate = value;
                OnPropertyChanged("ChBDate");
            }
        }

        private bool chBType;

        public bool ChBType
        {
            get { return chBType; }
            set
            {
                chBType = value;
                OnPropertyChanged("ChBType");
            }
        }
        #endregion
        #region RowVisible
        Visibility dateRow;
        public Visibility DateRow
        {
            get
            {
                return dateRow;
            }
            set
            {
                if (dateRow != value)
                    dateRow = value;
                OnPropertyChanged("DateRow");
            }
        }

        Visibility sumRow;
        public Visibility SumRow
        {
            get
            {
                return sumRow;
            }
            set
            {
                if (sumRow != value)
                    sumRow = value;
                OnPropertyChanged("SumRow");
            }
        }

        Visibility nameRow;
        public Visibility NameRow
        {
            get
            {
                return nameRow;
            }
            set
            {
                if (nameRow != value)
                    nameRow = value;
                OnPropertyChanged("NameRow");
            }
        }
        Visibility readingsRow;
        public Visibility ReadingsRow
        {
            get
            {
                return readingsRow;
            }
            set
            {
                if (readingsRow != value)
                    readingsRow = value;
                OnPropertyChanged("ReadingsRow");
            }
        }

        /*Visibility billRow;
        public Visibility BillRow
        {
            get
            {
                return billRow;
            }
            set
            {
                if (billRow != value)
                    billRow = value;
                OnPropertyChanged("BillRow");
            }
        }*/

        Visibility typeRow;
        public Visibility TypeRow
        {
            get
            {
                return typeRow;
            }
            set
            {
                if (typeRow != value)
                    typeRow = value;
                OnPropertyChanged("TypeRow");
            }
        }
        #endregion

        #region Commands
        public ICommand AcceptFilterCommand { get; set; }
        public ICommand ResetFilterCommand { get; set; } 
        #endregion
        #endregion
        public StatisticWindowViewModel()
        {}
        public StatisticWindowViewModel(string selAddress, string selStreet)
        {
            //получить текущую недвижимость
            propFileName = selAddress + "_" + selStreet;
            DI.FilesManager.EditFileName(ref propFileName);
            curProp = DI.Saver.GetInfo(new Property(), propFileName);

            AllPaymentsT = new ObservableCollection<string>();
            CreatePaymentsList();
            CreateDataTable();

            SetFirstValue();
            GetAllRowsHeight();

            AcceptFilterCommand = new CommandClass(arg => AcceptFilterMethod());
            ResetFilterCommand = new CommandClass(arg => ResetFilterMethod());
        }

        private void SetFirstValue()
        {
            To = DateTime.Now;
            From = DateTime.Now;
            SelType = PaymentT[0].PayName;
        }

        void CreateDataTable()
        {
            PaymantsTable = new DataTable();
            PaymantsTable.Columns.Add("Дата", typeof(DateTime));
            PaymantsTable.Columns.Add("Тип");
            PaymantsTable.Columns.Add("Сумма", typeof(decimal));
            PaymantsTable.Columns.Add("Имя");
            PaymantsTable.Columns.Add("Показания", typeof(decimal));
            //PaymantsTable.Columns.Add("Входящий счёт", typeof(bool));
            FillDataTable(curProp.payList);
        }

         void FillDataTable(IEnumerable<EasyPay> p)
        {
            PaymantsTable.Rows.Clear();
            foreach (var tmp in  p)
            {
                DataRow row = PaymantsTable.NewRow();
                row[0] = tmp.PayDate;
                row[1] = tmp.PayType;
                if(tmp.IncomingBill)
                    row[2] = -tmp.PaySum;
                else
                    row[2] = tmp.PaySum;
                row[3] = tmp.PayName;
                row[4] = tmp.Readings;
                //row[5] = tmp.IncomingBill;
                PaymantsTable.Rows.Add(row); 
            }

            CreateFullData(p);
        }

        private void CreateFullData(IEnumerable<EasyPay> p)
        {
            decimal i = p.Where(x => !x.IncomingBill).Select(z => z.PaySum).Sum();
            i -= p.Where(x => x.IncomingBill).Select(z => z.PaySum).Sum();
            FullInfo = "Общая сумма:" + i.ToString();
        }

        /// <summary>
        /// Общая статистика по всем платежам
        /// </summary>
        void CreatePaymentsList()
        {
            decimal smm = 0;
            if (!DI.Saver.IsEmpty("PaymantsTypes"))
                PaymentT = DI.Saver.GetInfo(new PaymentTypes(), "PaymantsTypes");

            foreach (var item in PaymentT)
            {
                var payments = curProp.payList.Where(x => x.PayType == item.PayName);
                if (item.haveBill)
                {
                    decimal plus = payments.Where(x => x.IncomingBill == false).Select(x => x.PaySum).Sum();
                    decimal minus = payments.Where(x => x.IncomingBill == true).Select(x => x.PaySum).Sum();

                    AllPaymentsT.Add(item.PayName + ": " + (plus - minus).ToString());
                    smm += plus - minus;
                }
                else
                {
                    decimal sum = payments.Select(x => x.PaySum).Sum();
                    AllPaymentsT.Add(item.PayName + ": " + sum.ToString());

                    smm += sum;
                }
            }
            AllPaymentsT.Add("Всего:" + smm);
        }


        #region RowOperations
        private void ChangeRowsHeight()
        {
            DateRow = GetRowLength(GetCurrentPayType().haveDate);
            SumRow = GetRowLength(GetCurrentPayType().haveSum);
            NameRow = GetRowLength(GetCurrentPayType().haveName);
            ReadingsRow = GetRowLength(GetCurrentPayType().haveReadings);
            //BillRow = GetRowLength(GetCurrentPayType().haveBill);
            TypeRow = Visibility.Visible;

        }
        void GetAllRowsHeight()
        {
            DateRow = Visibility.Visible;
            SumRow = Visibility.Visible;
            NameRow = Visibility.Visible;
            ReadingsRow = Visibility.Visible;
            //BillRow = Visibility.Visible;
            TypeRow = Visibility.Visible;
        }
        PaymentType GetCurrentPayType()
        {
            return PaymentT.Where(x => x.PayName == selType).Any() ? PaymentT.Where(x => x.PayName == selType).FirstOrDefault() : PaymentT[0];
        }

        Visibility GetRowLength(bool haveType)
        {
            if (haveType)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        #endregion

        #region Methods
        void AcceptFilterMethod()
        {
            DataGridToStart();
            var temp = curProp.payList;
            if (ChBDate)
            {
                temp = temp.
                    Where(x => x.PayDate.Date <= To.Date && x.PayDate.Date >= From.Date).
                    ToList();
            }
            if (ChBType)
            {
                temp = temp.Where(x => x.PayType == SelType).ToList();
                ChangeRowsHeight();
            }
            FillDataTable(temp);

        }

        void ResetFilterMethod()
        {
            ChBDate = false;
            ChBType = false;
            DataGridToStart();
        } 
        void DataGridToStart()
        {
            GetAllRowsHeight();
            FillDataTable(curProp.payList);
        }
        #endregion
    }
}
