using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using BlackSys.Models.ViewModels;
using System.Text;
using BlackSys.Filter;
using System.Data.Entity.Infrastructure;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;



namespace BlackSys
{
    public class CommonRepository 
    {
        public BlackSysEntities db = new BlackSysEntities();
       
        public static List<Branch> GetBranchList(string userId)
        {
            BlackSysEntities db = new BlackSysEntities();
            var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            return db.Branchs.Where(x => branchId == 1 ? x.BranchId != 0 : x.BranchId == branchId).ToList();
        }

        public static int GetBranchId(string userId)

        {
            BlackSysEntities db = new BlackSysEntities();
            var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            return db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();

        }
        public int[] GetComSepAllBranchId(string userId)
        {
            var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
           
                return String.Join(",", db.Branchs.Where(a=>a.BranchId== branchId).Select(s => s.BranchId).ToList()).Split(',').Select(int.Parse).ToArray();
           
        }
    public List<PaymentTermsModel> GetPaymentTermsList()
        {
            return db.PaymentTerms.ToList();
        }

        public List<SpecialDiscount> GetSpecialDiscountList()
        {
            return db.SpecialDiscounts.ToList();
        }

        public List<CategoryModel> GetServiceList()
        {
            return db.Categorys.ToList();
        }

        public List<AccountsHeadCategory> GetAccountHeadCategoryList()
        {
            return db.AccountsHeadCategorys.ToList();
        }

        public List<AccountsHeadSubCategory> GetAccountsHeadSubCategoryList()
        {
            return db.AccountsHeadSubCategories.ToList();
        }

        public List<AccountsSubHead> GetAccountsSubHeadList()
        {
            return db.AccountsSubHeads.ToList();
        }

        public List<MemberSource> GetMemberSourcedList()
        {
            return db.MemberSources.ToList();
        }

    }

}