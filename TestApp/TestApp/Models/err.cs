using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    public class err
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string s_descript { get; set; }
        public string f_descript { get; set; }
        
        public Status status { get; set; }
        public int statusId { get; set; }
        public Criticality criticality { get; set; }
        [ForeignKey("Criticality")]
        public int criticalityId { get; set; }
        
        public Urgency urgency { get; set; }
        public int urgencyId { get; set; }
        public ICollection<ErrorHistory> Histories { get; set; }
        public err ()
        {
            Histories = new List<ErrorHistory>();
        }
        
        public string user_id { get; set; }
         
    }

    public class Status
    {

        public int id { get; set; }
        public string text { get; set; }
         
        public ICollection<err> errors { get; set; }
        public Status() { errors = new List<err>(); }

    }
    public class Criticality
    {
        [Key]
        public int id { get; set; }
        public string text { get; set; }

        public ICollection<err> errors { get; set; }
        public Criticality() { errors = new List<err>(); }
    }

    public class Urgency
    {
        public int id { get; set; }
        public string text { get; set; }

        public ICollection<err> errors { get; set; }
        public Urgency() { errors = new List<err>(); }
    }
    public enum SortState
    {
        idAsc,
        idDesc,
        dateAsc,
        dateDesc,
        usernameAsc,
        usernameDesc
    }
}
