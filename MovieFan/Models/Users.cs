using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieFan.Models
{
    public partial class Users
    {
        [NotMapped]
        public bool IsAdministrator
        {
      
            get => (this.IsAdmin == 1);
            set => this.IsAdmin = (value ? (byte)1 : (byte)0);

        }

        public string FullName
        {
            get => $"{this.Firstname} {this.Lastname}";
        }
    }
}
