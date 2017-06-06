using System;
using System.Data.Entity;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;

namespace BlackSys.Models
{   
    
    public class BlackSysEntities : DbContext
    {
        public BlackSysEntities()
            : base("name=BlackSysConnection")
        {
        }
        public virtual DbSet<ProductConditionModel> ProductConditionModels { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Component> Component { get; set; }
        public virtual DbSet<ComponentCategory> ComponentCategory { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomPermission> CustomPermission { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<MenuTemp> MenuTemp { get; set; }
        public virtual DbSet<PermissionMenu> PermissionMenu { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BlackSys.Models.AccountsSubHead> AccountsSubHeads { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.MembershipInfo> MembershipInfoes { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.Service> Services { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.ServicePayment> ServicePayments { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.OrderDetails> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Card> Cards { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Branch> Branchs { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.Department> Departments { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.Employee> Employees { get; set; }

        

        public System.Data.Entity.DbSet<BlackSys.Models.Education> Educations { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.Appointment> Appointments { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.ProductModel> Products { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.CategoryModel> Categorys { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Requisition> Requisitions { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.StockOutModel> StockOuts { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.StockInModel> Stockins { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.RequisitionItems> RequisitionItems { get; set; }        
        
        public DbSet<ProductPhotosModel> ProductPhotos { get; set; }
        
        public DbSet<File> Files { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<AccountsSubHead>()
                .Property(e => e.CategoryID)
                .HasColumnName("CategoryID");

            //modelBuilder.Entity<AccountsSubHead>()
            //   .Property(e => e.CategoryName)
            //   .HasColumnName("CategoryName");

            modelBuilder.Entity<AccountsSubHead>()
                .Property(e => e.AccountsName)
                .HasColumnName("AccountName");
            
            modelBuilder.Entity<Component>()
               .Property(e => e.PurchasingPrice)
               .HasPrecision(19, 4);

            modelBuilder.Entity<Component>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ComponentCategory>()
                .HasMany(e => e.Component)
                .WithRequired(e => e.ComponentCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.Permission)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.CustomPermission)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.CustomPermission)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Permission)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<BlackSys.Models.ViewModels.RequisitionN1ViewModel> RequisitionN1ViewModel { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Leave> Leaves { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Holiday> Holidays { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.LeaveType> LeaveTypes { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Roaster> Roasters { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.Vendor> Vendors { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Shift> Shifts { get; set; }

        
        public System.Data.Entity.DbSet<BlackSys.Models.WeekEnd> WeekEnds { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.RequisitionItemCategoryModel> RequisitionItemCategoryModels { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.PurchaseOrder> PurchaseOrders { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.ViewModels.PurchaseOrderViewModel> PurchaseOrderViewModels { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Stock> Stocks { get; set; }
        public DbSet<BlackSys.Models.AccountsHeadCategory> AccountsHeadCategorys { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.RePayment> RePayments { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.FeedbackRatingPoints> FeedbackRatingPoints { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.FeedbackQuest> FeedbackQuestion { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.ViewModels.FeedbackViewModel> FeedbackViewModels { get; set; }
        public DbSet<FeedbackDetails> FeedbackDetails { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.Feedback> Feedbacks { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.PaymentTermsModel> PaymentTerms { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.AssignShift> AssignShifts { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Allowance> Allowances { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Deduction> Deductions { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.GrossSalaryDistribution> GrossSalaryDistributions { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.AllowanceCategory> AllowanceCategories { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.AllowanceType> AllowanceTypes { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.FixedAllowance> FixedAllowances { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.VariableAllowance> VariableAllowances { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.FixedDeduction> FixedDeductions { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.VariableDeduction> VariableDeductions { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.CardType> CardTypes { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.UnitModel> UnitModels { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.Designation> Designations { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.ProductSales> ProductSales { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.AccountsHeadSubCategory> AccountsHeadSubCategories { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.MemberSource> MemberSources { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.SpecialDiscount> SpecialDiscounts { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.HappyHourModel> HappyHourModels { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.TargetLevel1> TargetLevels1 { get; set; }
        public System.Data.Entity.DbSet<BlackSys.Models.TargetLevel2> TargetLevels2 { get; set; }

        public System.Data.Entity.DbSet<BlackSys.Models.TargetVsAch> TargetVsAchs { get; set; }
 

    }
}
