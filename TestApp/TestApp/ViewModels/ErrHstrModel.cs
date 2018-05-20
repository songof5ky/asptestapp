using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class ErrHstrModel
    {
        [Key]
        public DateTime date { get; set; }
        public int errId { get; set; }
        public string act { get; set; }
        public string comnt { get; set; }
        public errModel errModel { get; set; }
        public string user_id { get; set; }
         
    }
}
