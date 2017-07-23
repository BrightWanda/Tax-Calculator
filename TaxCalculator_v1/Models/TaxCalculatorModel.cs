using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator_v1.Models
{
    public class TaxCalculatorModel
    {
        public DataTable _deductionTable;
        public DataTable _ageDeductionTable;

        public TaxCalculatorModel()
        {
            _deductionTable = GetDeductionTable();
            _ageDeductionTable = AgeBasedTable();
        }


        private static DataTable GetDeductionTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("Min", typeof(int));
            table.Columns.Add("Max", typeof(int));
            table.Columns.Add("Deduction", typeof(double));

            // Here we add five DataRows.
            table.Rows.Add(5000, 10000, 5.0);
            table.Rows.Add(10001, 20000, 7.5);
            table.Rows.Add(20001, 35000, 9.0);
            table.Rows.Add(35001, 50000, 15.0);
            table.Rows.Add(50001, 70000, 25.0);
            table.Rows.Add(70001, int.MaxValue, 30.0);

            return table;
        }

        private static DataTable AgeBasedTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("MinAge", typeof(double));
            table.Columns.Add("MaxAge", typeof(double));
            table.Columns.Add("Relief", typeof(double));
            table.Columns.Add("% Deduction", typeof(double));


            // Here we add five DataRows.
            table.Rows.Add(18, 50, 2000, 100);
            table.Rows.Add(50, int.MaxValue, 5000,85);

            return table;
        }
    }
}
