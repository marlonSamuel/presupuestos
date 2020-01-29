namespace Model.NAV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPECIAL FRUITS & VEGETABLE$G_L Account")]
    public partial class SPECIAL_FRUITS___VEGETABLE_G_L_Account
    {
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] timestamp { get; set; }

        [Key]
        [StringLength(20)]
        public string No_ { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("Search Name")]
        [Required]
        [StringLength(50)]
        public string Search_Name { get; set; }

        [Column("Account Type")]
        public int Account_Type { get; set; }

        [Column("Global Dimension 1 Code")]
        [Required]
        [StringLength(20)]
        public string Global_Dimension_1_Code { get; set; }

        [Column("Global Dimension 2 Code")]
        [Required]
        [StringLength(20)]
        public string Global_Dimension_2_Code { get; set; }

        public int Income_Balance { get; set; }

        public int Debit_Credit { get; set; }

        [Column("No_ 2")]
        [Required]
        [StringLength(20)]
        public string No__2 { get; set; }

        public byte Blocked { get; set; }

        [Column("Direct Posting")]
        public byte Direct_Posting { get; set; }

        [Column("Reconciliation Account")]
        public byte Reconciliation_Account { get; set; }

        [Column("New Page")]
        public byte New_Page { get; set; }

        [Column("No_ of Blank Lines")]
        public int No__of_Blank_Lines { get; set; }

        public int Indentation { get; set; }

        [Column("Last Date Modified")]
        public DateTime Last_Date_Modified { get; set; }

        [Required]
        [StringLength(250)]
        public string Totaling { get; set; }

        [Column("Consol_ Translation Method")]
        public int Consol__Translation_Method { get; set; }

        [Column("Consol_ Debit Acc_")]
        [Required]
        [StringLength(20)]
        public string Consol__Debit_Acc_ { get; set; }

        [Column("Consol_ Credit Acc_")]
        [Required]
        [StringLength(20)]
        public string Consol__Credit_Acc_ { get; set; }

        [Column("Gen_ Posting Type")]
        public int Gen__Posting_Type { get; set; }

        [Column("Gen_ Bus_ Posting Group")]
        [Required]
        [StringLength(10)]
        public string Gen__Bus__Posting_Group { get; set; }

        [Column("Gen_ Prod_ Posting Group")]
        [Required]
        [StringLength(10)]
        public string Gen__Prod__Posting_Group { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [Column("Automatic Ext_ Texts")]
        public byte Automatic_Ext__Texts { get; set; }

        [Column("Tax Area Code")]
        [Required]
        [StringLength(20)]
        public string Tax_Area_Code { get; set; }

        [Column("Tax Liable")]
        public byte Tax_Liable { get; set; }

        [Column("Tax Group Code")]
        [Required]
        [StringLength(10)]
        public string Tax_Group_Code { get; set; }

        [Column("VAT Bus_ Posting Group")]
        [Required]
        [StringLength(10)]
        public string VAT_Bus__Posting_Group { get; set; }

        [Column("VAT Prod_ Posting Group")]
        [Required]
        [StringLength(10)]
        public string VAT_Prod__Posting_Group { get; set; }

        [Column("Exchange Rate Adjustment")]
        public int Exchange_Rate_Adjustment { get; set; }

        [Column("Default IC Partner G_L Acc_ No")]
        [Required]
        [StringLength(20)]
        public string Default_IC_Partner_G_L_Acc__No { get; set; }

        [Column("Omit Default Descr_ in Jnl_")]
        public byte Omit_Default_Descr__in_Jnl_ { get; set; }

        [Column("Cost Type No_")]
        [Required]
        [StringLength(20)]
        public string Cost_Type_No_ { get; set; }

        [Column("GIFI Code")]
        [Required]
        [StringLength(10)]
        public string GIFI_Code { get; set; }

        [Column("Budget Check")]
        public byte Budget_Check { get; set; }

        [Column("Budget Period")]
        [Required]
        [StringLength(20)]
        public string Budget_Period { get; set; }

        [Column("Budget Method")]
        public int Budget_Method { get; set; }

        [Column("Overhead Account")]
        public byte Overhead_Account { get; set; }

        [Column("Cost Category")]
        public int Cost_Category { get; set; }

        [Column("Unit Cost")]
        public decimal Unit_Cost { get; set; }

        [Column("Unit Price")]
        public decimal Unit_Price { get; set; }

        [Column("Is Retainage Acct_")]
        public byte Is_Retainage_Acct_ { get; set; }

        public List<SPECIAL_FRUITS___VEGETABLE_G_L_Account> GetAll()
        {
            var list = new List<SPECIAL_FRUITS___VEGETABLE_G_L_Account>();
            try
            {
                using (var ctx = new NavContext())
                {
                    list = ctx.SPECIAL_FRUITS___VEGETABLE_G_L_Account.ToList();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}
