using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Blacksys.Controllers
{
    public class UserIdentification
    {
        //Insert

        //shiftmodel.UpdatedByIp = null;
        //shiftmodel.UpdatedDateTime = null;
        //shiftmodel.UpdatedBy = null;

        //Update

        //db.ShiftModels.Attach(shiftmodel);
        //DbEntityEntry<ShiftModel> entry = db.Entry(shiftmodel);
        //entry.State = EntityState.Modified;
        //entry.Property(e => e.EntryBy).IsModified = false;
        //entry.Property(e => e.EntryDateTime).IsModified = false;
        //entry.Property(e => e.EntryByIp).IsModified = false;

        public UserIdentification()
        {
            //Franchise = "ARISIT";
            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            EntryBy = HttpContext.Current.User.Identity.Name;
            EntryDateTime = DateTime.UtcNow.AddHours(6);
            EntryByIp = ipAddress;

            UpdatedBy = HttpContext.Current.User.Identity.Name;
            UpdatedDateTime = DateTime.UtcNow.AddHours(6);
            UpdatedByIp = ipAddress;        
        }

        //[ScaffoldColumnAttribute(false)]
        //[MaxLength(10)]
        //public string Franchise { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        public string EntryBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? EntryDateTime { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        public string EntryByIp { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedDateTime { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        public string UpdatedByIp { get; set; }
    }
}