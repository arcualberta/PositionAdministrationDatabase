﻿using PD.Models.ChartFields;
using PD.Models.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Models.AppViewModels
{
    public class EmployeeViewModel
    {
        public enum eFundingSource { O = 1, S }

        public string DeptName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PositionNumber { get; set; }
        public int RecordNumber { get; set; }
        public Position.eWorkload FtPtStatus { get; set; }
        public Position.eContractType ContractStatus { get; set; }
        public eFundingSource FundingSource { get; set; }

        public SalaryViewModel Salary { get; set; }
    }
}
