using ComunalPay.Infrastructure;
using ComunalPay.Infrastructure.Concrete;
using ComunalPay.Model.Concrete;
using ComunalPay.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComunalPay.UI.ViewModel
{
    class NewPaymentViewModel: InotifyImplement
    {
        #region Prop
        /// <summary>
        /// номер текущей квартиры
        /// </summary>
        string propAddress;
        /// <summary>
        /// адрес текущей квартиры
        /// </summary>
        string propStreet;

        string selPType;
        public string SelPType
        {
            get { return selPType; }
            set
            {
                selPType = value;
                if(paymantsT.Any())
                    ChangeRowsHeight();
                OnPropertyChanged("SelPType");
            }
        }

        #region TBoxes
        DateTime selDate;
        public DateTime SelDate
        {
            get { return selDate; }
            set
            {
                selDate = value;
                OnPropertyChanged("SelDate");
            }
        }

        decimal selSum = 0;
        public string SelSum
        {
            get { return selSum.ToString(); }
            set
            {
                Decimal.TryParse(value, out selSum);
                OnPropertyChanged("SelSum");
            }
        }

        string selName;
        public string SelName
        {
            get { return selName; }
            set
            {
                selName = value;
                OnPropertyChanged("SelName");
            }
        }

        float selReadings = 0;
        public string SelReadings
        {
            get { return selReadings.ToString(); }
            set
            {
                float.TryParse(value, out selReadings);
                OnPropertyChanged("SelReadings");
            }
        }

        bool selBill;
        public bool SelBill
        {
            get { return selBill; }
            set
            {
                selBill = value;
                OnPropertyChanged("SelBill");
            }
        }

        string selCom;
        public string SelCom
        {
            get { return selCom; }
            set
            {
                selCom = value;
                OnPropertyChanged("SelCom");
            }
        } 
        #endregion

        PaymentTypes paymantsT = new PaymentTypes();

        public List<string> PaymantsT
        {
            get {
                    return paymantsT.GetAllNames.ToList();
            }
            set
            {
                if (paymantsT.GetAllNames != value)
                {
                    paymantsT.GetAllNames = value;
                    
                }
                OnPropertyChanged("PaymantsT");
            }
        }

        #region RowHieghts
        GridLength dateRow;
        public GridLength DateRow
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

        GridLength sumRow;
        public GridLength SumRow
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

        GridLength nameRow;
        public GridLength NameRow
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
        GridLength readingsRow;
        public GridLength ReadingsRow
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

        GridLength billRow;
        public GridLength BillRow
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
        }

        GridLength commentRow;
        public GridLength CommentRow
        {
            get
            {
                return commentRow;
            }
            set
            {
                if (commentRow != value)
                    commentRow = value;
                OnPropertyChanged("CommentRow");
            }
        }
        #endregion

        public ICommand NewPaymantCommand { get; set; }
        #endregion
        public NewPaymentViewModel()
        {}
        public NewPaymentViewModel(string propAddress, string propStreet)
        {
            this.propAddress = propAddress;
            this.propStreet = propStreet;

            if (!DI.Saver.IsEmpty("PaymantsTypes"))
                paymantsT = DI.Saver.GetInfo(new PaymentTypes(), "PaymantsTypes");

            NewPaymantCommand = new CommandClass(arg => NewPaymentMethod());
            SelDate = DateTime.Now;
        }




        #region RowOperations
        private void ChangeRowsHeight()
        {
            DateRow= GetRowLength(GetCurrentPayType().haveDate);
            SumRow = GetRowLength(GetCurrentPayType().haveSum);
            NameRow= GetRowLength(GetCurrentPayType().haveName);
            ReadingsRow = GetRowLength(GetCurrentPayType().haveReadings);
            BillRow = GetRowLength(GetCurrentPayType().haveBill);
            CommentRow = GetRowLength(GetCurrentPayType().haveComment);

        }
        PaymentType GetCurrentPayType()
        {
            return paymantsT.Where(x => x.PayName == selPType).Any() ? paymantsT.Where(x => x.PayName == selPType).FirstOrDefault() : paymantsT[0];
        }

        GridLength GetRowLength(bool haveType)
        {
            if (haveType)
            {
                return new GridLength(1, GridUnitType.Star);
            }
            else
            {
                return new GridLength(0);
            }
        }
        #endregion

        void NewPaymentMethod()
        {
            string propFileName = propAddress  + "_" + propStreet;
            DI.FilesManager.EditFileName(ref propFileName);
            Property proper = DI.Saver.GetInfo(new Property(), propFileName);
            if (proper.payList == null)
                proper.payList = new List<EasyPay>();

            EasyPay p = new EasyPay();
            p.Commentary = selCom;
            p.IncomingBill = selBill;
            p.PayDate = selDate;
            p.PaySum = selSum;
            p.PayType = selPType;
            p.Readings = selReadings;

            proper.payList.Add(p);
            DI.Saver.SaveInfo(proper, propFileName);
            Console.WriteLine();
        }
    }
}
