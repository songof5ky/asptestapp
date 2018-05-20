using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestApp.ViewModels
{
    public class errModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        [Required(ErrorMessage = "Не заполненно краткое описание")]
        public string s_descript { get; set; }
        [Required(ErrorMessage = "Не заполененно полное описание")]
        public string f_descript { get; set; }
        public List<ErrHstrModel> hstr{get;set;}
       
        public Criticality criticality
        {
            get;set;
        }
        public int criticalityId { get; set; }

        public Status status { get; set; }
        public int statusId { get; set; }
        public Urgency urgency { get; set; }
        public int urgencyId { get; set; }




        public string user_id { get; set; }
        
    }
    public class Status
    {

        public int id { get; set; }

        string text { get; set; }

        public ICollection<errModel> errors { get; set; }
        public Status() { errors = new List<errModel>(); }

    }

    public class Criticality
    {
        public int id { get; set; }
        public string text { get; set; }

        public ICollection<errModel> errors { get; set; }
        public Criticality() { errors = new List<errModel>(); }
    }

    public class Urgency
    {

        public int id { get; set; }

        string text { get; set; }

        public ICollection<errModel> errors { get; set; }
        public Urgency() { errors = new List<errModel>(); }

    }

}
