﻿using PD.Models.ChartFields;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PD.Models
{
    /// <summary>
    /// ChartString
    /// Represents a chart string consists of a series of chart fields
    /// </summary>
    public class ChartString
    {
        [Key]
        public int Id { get; set; }
        public string SpeedCode { get; set; }
        public string ComboCode { get; set; }

        public virtual ICollection<ChartField2ChartStringJoin> ChartFields { get; set; } = new List<ChartField2ChartStringJoin>();

        public virtual ICollection<PositionAccount> PositionAccounts { get; set; } = new List<PositionAccount>();


        protected ChartField2ChartStringJoin GetChartFieldHolder<T>() where T : ChartField
        {
            ChartField2ChartStringJoin holder = ChartFields
                .Where(join => join.ChartField is T)
                .FirstOrDefault();

            return holder;
        }

        public T GetChartField<T>() where T : ChartField
        {
            ChartField2ChartStringJoin holder = GetChartFieldHolder<T>();

            T field = ChartFields
                .Where(join => join.ChartField is T)
                .Select(join => join.ChartField as T)
                .FirstOrDefault();

            return holder == null ? null : holder.ChartField as T;
        }

        /// <summary>
        /// Sets the given type of ChartField which is part of this ChartString.
        /// If the a ChartField of the given type already exists within the list 
        /// of fields, then this method will replace it. Otherwise, it will add it. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        public void SetChartField<T>(T field) where T : ChartField
        {
            ChartField2ChartStringJoin holder = GetChartFieldHolder<T>();
            if(holder == null)
            {
                holder = new ChartField2ChartStringJoin();
                holder.ChartStringId = Id;
                holder.ChartString = this;
                ChartFields.Add(holder);
            }
            holder.ChartFieldId = field.Id;
            holder.ChartField = field;
        }

        public string GetString(string separator = " : ")
        {
            return GetChartField<BusinessUnit>().Value
                + separator + GetChartField<Account>().Value
                + separator + GetChartField<Fund>().Value
                + separator + GetChartField<DeptID>().Value
                + separator + GetChartField<Program>().Value
                + separator + GetChartField<Class>().Value 
                + separator + GetChartField<Project>().Value
                + separator + GetChartField<Sponsor>().Value;
        }

        [NotMapped]
        [Display(Name ="Business Unit")]
        public virtual BusinessUnit BusinessUnit
        {
            get { return GetChartField<BusinessUnit>(); }
            set { SetChartField<BusinessUnit>(value); }
        }
        //private BusinessUnit _BusinessUnit;

        [NotMapped]
        public virtual Account Account
        {
            get { return GetChartField<Account>(); }
            set { SetChartField<Account>(value); }
        }
        //private Account _Account;

        [NotMapped]
        public virtual Class Class
        {
            get { return GetChartField<Class>(); }
            set { SetChartField<Class>(value); }
        }
        //private Class _Class;

        [Display(Name ="Dept ID")]
        [NotMapped]
        public virtual DeptID DeptID
        {
            get { return GetChartField<DeptID>(); }
            set { SetChartField<DeptID>(value); }
        }
        //private DeptID _DeptID;

        [NotMapped]
        public virtual Fund Fund
        {
            get { return GetChartField<Fund>(); }
            set { SetChartField<Fund>(value); }
        }
        //private Fund _Fund;

        [NotMapped]
        public virtual Program Program
        {
            get { return GetChartField<ChartFields.Program>(); }
            set { SetChartField<ChartFields.Program>(value); }
        }
        //private Program _Program;

        [NotMapped]
        public virtual Project Project
        {
            get { return GetChartField<Project>(); }
            set { SetChartField<Project>(value); }
        }
        //private Project _Project;

        [NotMapped]
        public virtual Sponsor Sponsor
        {
            get { return GetChartField<Sponsor>(); }
            set { SetChartField<Sponsor>(value); }
        }
        //private Sponsor _Sponsor;
    }
}
