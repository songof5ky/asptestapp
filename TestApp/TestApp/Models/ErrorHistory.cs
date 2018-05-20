using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace TestApp.Models
{
    public class ErrorHistory
    {
        //public string id { get; set; }
        [Key]
         
        public DateTime date { get; set; }
        public int errId { get; set; }
        public string act { get; set; }
        public string comnt { get; set; }
        public string user_id { get; set; }
         public err err { get; set; }
    }
}
