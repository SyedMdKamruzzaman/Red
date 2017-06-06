using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlackSys.Models
{
    public class ProductPhotosModel
    {        
        [Key]
        public int productphotoId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int productid { get; set; }
        public virtual ProductModel Products { get; set; }

    }
}