﻿using OfficeOpenXml;
using PD.Data;
using PD.Models;
using PD.Models.AppViewModels.Faculty;
using PD.Models.ChartFields;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static PD.Models.AppViewModels.EmployeeViewModel;
using PD.Models.Compensations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using PD.Models.Positions;

namespace PD.Services
{
    public class ImportService: PdServiceBase
    {
        public ImportService(ApplicationDbContext db)
            : base(db)
        {
        }

        private enum ColIndex
        {
            deptNameCol = 1,
            rankCol,
            employeeIdCol,
            employeeNameCol,
            posNumCol,
            recordNumCol,
            ftPtStatusCol,
            statusCol,
            fundSrcCol,
            meritDecCol,
            meritReasonCol,
            pastSalaryCol,
            contractSettlementCol,
            meritCol,
            specialAdjustCol,
            currSalaryCol,
            marketSupplementCol,
            promotedCol,
            notesCol,
            scCol,
            deptIdCol,
            fundCol,
            progCol,
            accCol,
            unknown1,
            bargUnitCol
        };

        public void InjestFacultySalaryAdjustmentData(string fileName, string worksheetName, DateTime fiscalYearStartDate, DateTime fiscalYearEndDate, DateTime sampleDate, DateTime nextFiscalYearEndDate)
        {
            FileInfo file = new FileInfo(fileName);
            //try
            {
                byte[] dataBytes = File.ReadAllBytes(fileName);
                using (MemoryStream ms = new MemoryStream(dataBytes))
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    StringBuilder sb = new StringBuilder();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets
                        .Where(ws => ws.Name == worksheetName)
                        .FirstOrDefault();

                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;

                    List<FacultyEmployeeViewModel> employees = new List<FacultyEmployeeViewModel>();

                    //validating columns
                    List<string> errors = new List<string>();
                    List<string> expectedHeadings = new List<string>(){
                        "Faculty / Department Name",
                        "Rank",
                        "Employee ID",
                        "Employee Name",
                        "Position Number",
                        "Record #",
                        "FT/PT",
                        "Status",
                        "Fund Source",
                        "Merit Decision",
                        "Merit Reason",
                        "Salary",
                        "Contract Settlement (1.0%)",
                        "Merit",
                        "Special Adjustment",
                        "Salary",
                        "Market Supplement",
                        "Promoted",
                        "Notes",
                        "SC",
                        "DeptID",
                        "Fund",
                        "Prog",
                        "Acc#",
                        "Check1",
                        "Barg_Unit"
                    };

                    for (int i = 1; i <= Enum.GetValues(typeof(ColIndex)).Length; ++i)
                    {
                        string val = worksheet.Cells[1, i].Value.ToString().Trim();

                        if (worksheet.Cells[1, i].Value.ToString().Trim() != expectedHeadings[i - 1])
                            errors.Add("Column " + i + " must be " + expectedHeadings[i - 1] + ". Found " + worksheet.Cells[1, i].Value.ToString());
                    }

                    //Making sure past and current salaries are in the correct columns
                    int[] olderYearRange = worksheet.Cells[2, (int)ColIndex.pastSalaryCol].Value.ToString().Split("/").Select(x => int.Parse(x)).ToArray();
                    int[] latterYearRange = worksheet.Cells[2, (int)ColIndex.currSalaryCol].Value.ToString().Split("/").Select(x => int.Parse(x)).ToArray();
                    if (olderYearRange[0] != fiscalYearStartDate.Year || olderYearRange[1] != fiscalYearEndDate.Year)
                        throw new Exception("Specified fiscal year does not match with the data");

                    if (olderYearRange[1] != latterYearRange[0])
                        throw new Exception("The ending year of the older fiscal year must be as same as the begining year of the latter fiscal year.");

                    for (int row = 3; row <= rowCount; ++row)
                    {
                        Position.eWorkload ftptStatus = Enum.Parse<Position.eWorkload>(worksheet.Cells[row, (int)ColIndex.ftPtStatusCol].Value.ToString());
                        eFundingSource fundingSource = Enum.Parse<eFundingSource>(worksheet.Cells[row, (int)ColIndex.fundSrcCol].Value.ToString());

                        FacultyEmployeeViewModel empl = new FacultyEmployeeViewModel()
                        {
                            DeptName = worksheet.Cells[row, (int)ColIndex.deptNameCol].Value.ToString().Trim(),
                            Rank = worksheet.Cells[row, (int)ColIndex.rankCol].Value.ToString().Trim(),
                            EmployeeId = worksheet.Cells[row, (int)ColIndex.employeeIdCol].Value.ToString().Trim(),
                            EmployeeName = worksheet.Cells[row, (int)ColIndex.employeeNameCol].Value.ToString().Trim(),
                            PositionNumber = worksheet.Cells[row, (int)ColIndex.posNumCol].Value.ToString().Trim(),
                            FtPtStatus = ftptStatus,
                            FundingSource = fundingSource,
                            RecordNumber = Int32.Parse(worksheet.Cells[row, (int)ColIndex.recordNumCol].Value.ToString().Trim()),
                        };

                        empl.Salary = new FacultySalaryViewModel();
                        empl.FacultySalary.Account = new Account() { Value = worksheet.Cells[row, (int)ColIndex.accCol].Value.ToString().Trim() };
                        empl.FacultySalary.BargUnit = worksheet.Cells[row, (int)ColIndex.bargUnitCol].Value == null ? null : worksheet.Cells[row, (int)ColIndex.bargUnitCol].Value.ToString().Trim();
                        empl.FacultySalary.ContractSettlement = decimal.Parse(worksheet.Cells[row, (int)ColIndex.contractSettlementCol].Value.ToString().Trim());
                        empl.FacultySalary.NextSalary = decimal.Parse(worksheet.Cells[row, (int)ColIndex.currSalaryCol].Value.ToString().Trim());
                        empl.FacultySalary.DeptId = new DeptID() { Value = worksheet.Cells[row, (int)ColIndex.deptIdCol].Value.ToString().Trim() };
                        empl.FacultySalary.Fund = new Fund() { Value = worksheet.Cells[row, (int)ColIndex.fundCol].Value.ToString().Trim() };
                        empl.FacultySalary.IsPromoted = "Yes" == worksheet.Cells[row, (int)ColIndex.promotedCol].Value.ToString().Trim();
                        empl.FacultySalary.CurrentSalary = decimal.Parse(worksheet.Cells[row, (int)ColIndex.pastSalaryCol].Value.ToString().Trim());
                        empl.FacultySalary.MarketSupplement = decimal.Parse(worksheet.Cells[row, (int)ColIndex.marketSupplementCol].Value.ToString().Trim());
                        empl.FacultySalary.Merit = decimal.Parse(worksheet.Cells[row, (int)ColIndex.meritCol].Value.ToString().Trim());
                        empl.FacultySalary.MeritDecision = decimal.Parse(worksheet.Cells[row, (int)ColIndex.meritDecCol].Value.ToString().Trim());
                        empl.FacultySalary.MeritReason = worksheet.Cells[row, (int)ColIndex.meritReasonCol].Value == null ? null : worksheet.Cells[row, (int)ColIndex.meritReasonCol].Value.ToString().Trim();
                        empl.FacultySalary.Notes = worksheet.Cells[row, (int)ColIndex.notesCol].Value == null ? null : worksheet.Cells[row, (int)ColIndex.notesCol].Value.ToString().Trim();
                        empl.FacultySalary.Program = new Program() { Value = worksheet.Cells[row, (int)ColIndex.progCol].Value.ToString().Trim() };
                        empl.FacultySalary.SpecialAdjustment = decimal.Parse(worksheet.Cells[row, (int)ColIndex.specialAdjustCol].Value.ToString().Trim());
                        empl.FacultySalary.Speedcode = new Speedcode() { Value = worksheet.Cells[row, (int)ColIndex.scCol].Value.ToString().Trim() };
                        empl.ContractStatus = Enum.Parse<Position.eContractType>(worksheet.Cells[row, (int)ColIndex.statusCol].Value.ToString().Trim());
                        
                        employees.Add(empl);
                    }


                    //Adding new departments that are in the data file but not in the database to the database
                    string[] deptNames = employees.Select(empl => empl.DeptName).Distinct().ToArray();
                    foreach (string deptName in deptNames)
                    {
                        if (!Db.Departments.Where(dept => dept.Name == deptName).Any())
                            Db.Departments.Add(new Department() { Name = deptName });
                    }
                    Db.SaveChanges();

                    //Adding new employees that are in the data file but not in the database to the database
                    foreach (var empl in employees)
                    {
                        Person dbPersonRecord = Db.Persons.Where(p => p.EmployeeId == empl.EmployeeId).FirstOrDefault();
                        if (dbPersonRecord == null)
                        {
                            dbPersonRecord = new Person()
                            {
                                EmployeeId = empl.EmployeeId,
                                Name = DataProtector.Encrypt(empl.EmployeeName)
                            };
                            Db.Persons.Add(dbPersonRecord);
                        }
                    }
                    Db.SaveChanges();

                    //Creating all the chart fields and speed codes if they do not exist
                    foreach (var empl in employees)
                    {
                        //Account
                        if (!Db.ChartFields.Where(cf => cf is Account && cf.Value == empl.FacultySalary.Account.Value).Any())
                            Db.ChartFields.Add(empl.FacultySalary.Account);
                        else //reload the field so that it contains all relationships
                            empl.FacultySalary.Account = Db.ChartFields.Where(cf => cf is Account && cf.Value == empl.FacultySalary.Account.Value).FirstOrDefault() as Account;

                        //Program
                        if (!Db.ChartFields.Where(cf => cf is Program && cf.Value == empl.FacultySalary.Program.Value).Any())
                            Db.ChartFields.Add(empl.FacultySalary.Program);
                        else //reload the field so that it contains all relationships
                            empl.FacultySalary.Program = Db.ChartFields.Where(cf => cf is Program && cf.Value == empl.FacultySalary.Program.Value).FirstOrDefault() as Program;

                        //Fund
                        if (!Db.ChartFields.Where(cf => cf is Fund && cf.Value == empl.FacultySalary.Fund.Value).Any())
                            Db.ChartFields.Add(empl.FacultySalary.Fund);
                        else //reload the field so that it contains all relationships
                            empl.FacultySalary.Fund = Db.ChartFields.Where(cf => cf is Fund && cf.Value == empl.FacultySalary.Fund.Value).FirstOrDefault() as Fund;

                        //DeptID
                        Department dept = Db.Departments.Where(d => d.Name == empl.DeptName).FirstOrDefault();
                        if (!Db.ChartFields.Where(cf => cf is DeptID && cf.Value == empl.FacultySalary.DeptId.Value).Any())
                        {
                            Db.ChartFields.Add(empl.FacultySalary.DeptId);

                            //Adding this DeptID to the current department of the employee
                            dept.DeptIDs.Add(empl.FacultySalary.DeptId);
                        }
                        else
                        {
                            //reload the field so that it contains all relationships and 
                            //ensure that the reloaded DeptID belongs to the current employee's department
                            empl.FacultySalary.DeptId = Db.ChartFields.Where(cf => cf is DeptID && cf.Value == empl.FacultySalary.DeptId.Value).FirstOrDefault() as DeptID;
                            if (!dept.DeptIDs.Select(d => empl.FacultySalary.DeptId.Id).Any())
                                throw new Exception("DeptId " + empl.FacultySalary.DeptId + " is already registered with the department " +
                                    empl.FacultySalary.DeptId.Department.Name + ", so it cannot be associated with an employee of " +
                                    dept.Name);
                        }

                        //Speedcode
                        if (!Db.Speedcodes.Where(sc => sc.Value == empl.FacultySalary.Speedcode.Value).Any())
                            Db.Speedcodes.Add(empl.FacultySalary.Speedcode);
                        else //reload the field so that it contains all relationships
                            empl.FacultySalary.Speedcode = Db.Speedcodes.Where(sc => sc.Value == empl.FacultySalary.Speedcode.Value).FirstOrDefault();


                        Db.SaveChanges();
                    }

                    //Creating chart strings
                    foreach (var empl in employees)
                    {
                        int? csId = GetChartStringIds(empl.Salary.DeptId.Id, empl.Salary.Fund.Id, empl.Salary.Program.Id, empl.Salary.Account.Id).FirstOrDefault();

                        ChartString cs;
                        if (!csId.HasValue || csId.Value == 0)
                        {
                            cs = new ChartString();
                            cs.DeptID = empl.Salary.DeptId;
                            cs.Fund = empl.Salary.Fund;
                            cs.Program = empl.Salary.Program;
                            cs.Account = empl.Salary.Account;

                            Db.ChartStrings.Add(cs);
                            Db.SaveChanges();
                        }
                        else
                            cs = Db.ChartStrings.Find(csId);
                    }

                    //Creating speedcodes
                    foreach (var empl in employees)
                    {
                        
                    }

                    //Adding position records
                    foreach (var empl in employees)
                    {
                        //Retrieve the person record of this employee from the database
                        Person person = Db.Persons.Where(p => p.EmployeeId == empl.EmployeeId).FirstOrDefault();

                        //Retrieve the active position record with the given position number from the database
                        Position position = Db.Positions
                            .Where(p => p.Number == empl.PositionNumber
                                && (p.StartDate.HasValue == false || p.StartDate <= DateTime.Today)
                                && (p.EndDate.HasValue == false || p.EndDate > DateTime.Today)
                                )
                            .FirstOrDefault();

                        if (position == null)
                        {
                            //Create a new position record
                            position = new Faculty();
                            position.StartDate = fiscalYearStartDate;
                            position.EndDate = null;
                            position.Number = empl.PositionNumber;
                            position.Title = empl.Rank;
                            position.Workload = empl.FtPtStatus;
                            position.ContractType = empl.ContractStatus;

                            Db.Positions.Add(position);
                            person.Positions.Add(position);
                            Db.SaveChanges();
                        }
                    }
                    
                    //Creating Position Accounts
                    foreach (var empl in employees)
                    {
                        var position = Db.Positions.Where(pos => pos.Number == empl.PositionNumber).FirstOrDefault();
                        var chartStringId = GetChartStringIds(empl.Salary.DeptId.Id, empl.Salary.Fund.Id, empl.Salary.Program.Id, empl.Salary.Account.Id).First();

                        if(!Db.PositionAccounts.Where(pa => pa.PositionId == position.Id && pa.ChartStringId == chartStringId).Any())
                        {
                            PositionAccount pa = new PositionAccount();
                            pa.ValuePercentage = 100;
                            position.PositionAccounts.Add(pa);
                            pa.ChartStringId = chartStringId;

                            Db.SaveChanges();
                        }
                    }

                    //Adding position assignments to positions
                    foreach (var empl in employees)
                    {
                        IQueryable<PositionAssignment> positionAssignments = GetPositionAssignments(null, empl.PositionNumber, sampleDate);

                        if (positionAssignments.Count() > 1)
                            throw new Exception("Data Error: Same position has multiple assign,emys at the time period containig " + sampleDate.Date);

                        if (positionAssignments.Count() == 0)
                        {
                            Position position = Db.Positions
                                .Include(pos => pos.PositionAssignments)
                                .Where(pos => pos.Number == empl.PositionNumber)
                                .FirstOrDefault();

                            if (position == null)
                                throw new Exception(string.Format("Data Error: Position with position number {0} does not exist", empl.PositionNumber));

                            PositionAssignment pa = new PositionAssignment();
                            pa.Status = PositionAssignment.eStatus.Active;
                            pa.StartDate = fiscalYearStartDate;
                            pa.EndDate = null;

                            position.PositionAssignments.Add(pa);

                            Db.SaveChanges();
                        }
                    }


                    //Updating employee salary information
                    foreach (var empl in employees)
                    {
                        //In this data ingestion, there will be only one position assignment for this position number.
                        IQueryable<PositionAssignment> positionAssignments = Db.PositionAssignments
                            .Include(a => a.Compensations)
                            .Where(a => a.Position.Number == empl.PositionNumber
                                && (!a.StartDate.HasValue || a.StartDate <= sampleDate)
                                && (!a.EndDate.HasValue || a.EndDate >= sampleDate)
                            );

                        if (positionAssignments.Count() != 1)
                            throw new Exception(string.Format("There must be one and only one position assignment. Found {0}", positionAssignments.Count()));

                        PositionAssignment pa = positionAssignments.FirstOrDefault();

                        string year = string.Format("{0}/{1}", olderYearRange[0], olderYearRange[1]);

                        //current year salary
                        Salary salary = Db.Salaries
                            .Where(s => s.Year == year && s.PositionAssignmentId == pa.Id)
                            .FirstOrDefault();
                        if (salary == null)
                        {
                            salary = new Salary()
                            {
                                Year = year,
                                Value = empl.Salary.CurrentSalary,
                                StartDate = fiscalYearStartDate,
                                EndDate = fiscalYearEndDate,
                                YearEndMeritDecision = (empl.Salary as FacultySalaryViewModel).MeritDecision,
                                YearEndMeritReason = (empl.Salary as FacultySalaryViewModel).MeritReason,
                                YearEndMerit = (empl.Salary as FacultySalaryViewModel).Merit,
                                YearEndPromotionStatus = (empl.Salary as FacultySalaryViewModel).IsPromoted
                            };
                            pa.Compensations.Add(salary);
                        }

                        //Year end Contract Settlement for the current year
                        string name = "Year-end Contract Settlement";
                        Adjustment atb = Db.Adjustments
                            .Where(a => a.PositionAssignmentId == pa.Id && a.Year == year && a.Name == name)
                            .FirstOrDefault();
                        if(atb == null)
                        {
                            atb = new Adjustment()
                            {
                                Year = year,
                                EndDate = fiscalYearEndDate,
                                Name = name,
                                StartDate = fiscalYearStartDate,
                                Value = empl.FacultySalary.ContractSettlement
                            };
                            pa.Compensations.Add(atb);
                        }
                        Db.SaveChanges();

                        //Year-end special adjustment for the current year
                        if (empl.FacultySalary.SpecialAdjustment > 0)
                        {
                            name = "Special Adjustment";
                            Adjustment sa = Db.Adjustments
                                .Where(a => a.PositionAssignmentId == pa.Id && a.Year == year && a.Name == name)
                                .FirstOrDefault();

                            if(sa == null)
                            {
                                sa = new Adjustment()
                                {
                                    Year = year,
                                    EndDate = fiscalYearEndDate,
                                    Name = name,
                                    StartDate = fiscalYearStartDate,
                                    Value = empl.FacultySalary.SpecialAdjustment
                                };
                                pa.Compensations.Add(sa);
                            }
                        }
                        Db.SaveChanges();

                        //Year-end market supplement for the current year
                        if (empl.FacultySalary.MarketSupplement > 0)
                        {
                            name = "Market Supplement";
                            Adjustment sa = Db.Adjustments
                                .Where(a => a.PositionAssignmentId == pa.Id && a.Year == year && a.Name == name)
                                .FirstOrDefault();

                            if (sa == null)
                            {
                                sa = new Adjustment()
                                {
                                    Year = year,
                                    EndDate = fiscalYearEndDate,
                                    Name = name,
                                    StartDate = fiscalYearStartDate,
                                    Value = empl.FacultySalary.MarketSupplement
                                };
                                pa.Compensations.Add(sa);
                            }
                        }
                        Db.SaveChanges();


                        //next year salary
                        year = string.Format("{0}/{1}", latterYearRange[0], latterYearRange[1]);
                        salary = Db.Salaries
                            .Where(s => s.Year == year && s.PositionAssignmentId == pa.Id)
                            .FirstOrDefault();
                        if (salary == null)
                        {
                            salary = new Salary()
                            {
                                Year = year,
                                Value = empl.Salary.NextSalary,
                                StartDate = fiscalYearStartDate.AddDays(1),
                                EndDate = nextFiscalYearEndDate,
                                YearEndMeritDecision = null,
                                YearEndMeritReason = null,
                                YearEndMerit = null,
                                YearEndPromotionStatus = false
                            };
                            pa.Compensations.Add(salary);
                        }


                        //////KR: Oct 24
                        ////var position = Db.Positions.Where(pos => pos.Number == empl.PositionNumber).FirstOrDefault();
                        ////var person = Db.Persons.Where(per => per.EmployeeId == empl.EmployeeId).FirstOrDefault();
                        ////var personPosition = GetPositionAssociations(person.Id, position.Id, sampleDate).FirstOrDefault();

                        ////var currentFiscalYear = fiscalYearStartDate.Year + "/" + fiscalYearEndDate.Year;
                        ////var nextFiscalYear = fiscalYearEndDate.Year + "/" + (fiscalYearEndDate.Year + 1);

                        //////Add current year's compensation records if it does not exist in the database
                        ////if(!personPosition.Compensations.Where(c => c.Year == currentFiscalYear).Any())
                        ////{
                        ////    FacultyCompensation c = new FacultyCompensation();
                        ////    c.Year = currentFiscalYear;
                        ////    c.Salary = empl.Salary.CurrentSalary;
                        ////    c.StartDate = fiscalYearStartDate.Date;
                        ////    c.EndDate = fiscalYearEndDate.Date;
                        ////    personPosition.Compensations.Add(c);
                        ////}

                        //////Add next year's compensation if 
                        ////FacultySalaryViewModel salary = empl.Salary as FacultySalaryViewModel;
                        ////if (!personPosition.Compensations.Where(c => c.Year == nextFiscalYear).Any())
                        ////{
                        ////    FacultyCompensation c = new FacultyCompensation();
                        ////    c.Year = nextFiscalYear;
                        ////    c.StartDate = fiscalYearEndDate.Date.AddDays(1);
                        ////    c.EndDate = nextFiscalYearEndDate;
                        ////    c.Salary = salary.NextSalary;
                        ////    c.MeritDecision = salary.MeritDecision;
                        ////    c.MeritReason = salary.MeritReason;
                        ////    c.Merit = salary.Merit;
                        ////    c.ContractSuppliment = salary.ContractSettlement;
                        ////    c.SpecialAdjustment = salary.SpecialAdjustment;
                        ////    c.MarketSupplement = salary.MarketSupplement;

                        ////    personPosition.Compensations.Add(c);
                        ////}

                        ////Db.SaveChanges();
                    }

                    //Updating 

                }
            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected IEnumerable<int> GetChartStringIds(int deptIdId, int fundId, int programId, int accountId)
        {
            var deptIdParentIds = Db.ChartField2ChartStringJoins.Where(join => join.ChartFieldId == deptIdId).Select(join => join.ChartStringId).ToList();
            var fundParentIds = Db.ChartField2ChartStringJoins.Where(join => join.ChartFieldId == fundId).Select(join => join.ChartStringId);
            var progParentIds = Db.ChartField2ChartStringJoins.Where(join => join.ChartFieldId == programId).Select(join => join.ChartStringId);
            var accountParentIds = Db.ChartField2ChartStringJoins.Where(join => join.ChartFieldId == accountId).Select(join => join.ChartStringId);

            IEnumerable<int> csIds = deptIdParentIds.Intersect(fundParentIds).Intersect(progParentIds).Intersect(accountParentIds);
            return csIds;
        }

        public void UpdateEmployee(FacultyEmployeeViewModel empl)
        {
            //Creating the employee if does not exist in the database
            Person emplDbRecord = Db.Persons.Where(p => p.EmployeeId == empl.EmployeeId).FirstOrDefault();
            if(emplDbRecord == null)
            {
                emplDbRecord = new Person()
                {
                    Name = empl.EmployeeName,
                    EmployeeId = empl.EmployeeId
                };
                Db.Persons.Add(emplDbRecord);
            }
            else
            {
                //Update the employee name if it's not null in the view model
                emplDbRecord.Name = empl.EmployeeName;
            }

            //Finding all Position records with the given position number
            

            Db.SaveChanges();
        }
    }
}
