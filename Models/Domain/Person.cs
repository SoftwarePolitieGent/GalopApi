using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GalopApi.Models.Domain
{
	public class Person
	{
        [Key]
        public int ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Geslacht { get; set; }
        public string Graad { get; set; }
        //[ForeignKey("Bureau")]
        //public int? BureauID { get; set; }

        //public Bureau Bureau { get; set; }
        //[ForeignKey("DienstID")]
        //public int? DienstID { get; set; }

        //public Dienst Dienst { get; set; }
        public List<KeyValuePair<String, String>> Communicatiemiddelen { get; set; }
        public Person(string Lastname, string Firstname, string Geslacht, string Graad) : this()
        {

            this.Lastname = Lastname;
            this.Firstname = Firstname;
            this.Geslacht = Geslacht;
            this.Graad = Graad;
        }
        public Person()
        {
            this.Communicatiemiddelen = new List<KeyValuePair<string, string>>();
        }
    }
}