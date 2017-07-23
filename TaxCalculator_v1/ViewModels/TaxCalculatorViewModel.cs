using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator_v1.ICommands;
using TaxCalculator_v1.Models;

namespace TaxCalculator_v1.ViewModels
{
    class TaxCalculatorViewModel : ViewModelBase
    {

        private double _salary;
        private int _age;
        private double _deductionTableValue;
        private double _ageDeductionTableRelief;
        private double _ageDeductionTablePercentageDeduction;
        private double _finalTaxValue;

        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();
        public TaxCalculatorModel _taxCalModel;
        public DataTable _deductionTableVM;
        public DataTable _ageDeductionTableVM;

        public TaxCalculatorViewModel()
        {
            _taxCalModel = new TaxCalculatorModel();
            _deductionTableVM = _taxCalModel._deductionTable;
            _ageDeductionTableVM = _taxCalModel._ageDeductionTable;
        }

        public string FinalTaxValue
        {
            get { return _finalTaxValue.ToString(); }
        }
        public IEnumerable<string> History
        {
            get { return _history; }
        }

        public double Salary
        {
            get { return _salary; }
            set
            {
                _salary = value;
                RaisePropertyChangedEvents("Salary");
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChangedEvents("Age");
            }
        }

        public ICommand CalculateTaxCommand
        {
            get { return new DelegateCommand(TaxCalculator); }
        }

        private void TaxCalculator()
        {

            foreach (DataRow row in _deductionTableVM.Rows)
            {
                if (_salary >= row.Field<int>(0) && _salary <= row.Field<int>(1))
                {
                    _deductionTableValue = row.Field<double>(2);
                    break;
                }
            }

            foreach (DataRow ageRow in _ageDeductionTableVM.Rows)
            {
                if (_age >= ageRow.Field<double>(0) && _age <= ageRow.Field<double>(1))
                {
                    _ageDeductionTableRelief = ageRow.Field<double>(2);
                    _ageDeductionTablePercentageDeduction = ageRow.Field<double>(3);
                    break;
                }
            }

            _finalTaxValue = (_salary - _ageDeductionTableRelief) * ((_ageDeductionTablePercentageDeduction/100.0)*(_deductionTableValue)/100);
            AddToHistory(_finalTaxValue);
        }

        private void AddToHistory(double t)
        {
            _history.Clear();
            _history.Add("Your Tax to pay is: " + t.ToString());

        }
    }
}
