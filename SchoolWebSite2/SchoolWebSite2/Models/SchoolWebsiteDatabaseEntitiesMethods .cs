using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolWebSite2.Models
{
    public partial class SchoolWebsiteDatabaseEntities : DbContext
    {
        public bool IsLoginUnique(string login)
        {
            foreach(var user in User)
            {
                if(user.Login == login)
                {
                    return false;
                }
            }

            return true;
        }
        public bool IsClassUnique(Class classToAdd)
        {
            foreach (var cl in Class)
            {
                if (cl.Number == classToAdd.Number && cl.Code == classToAdd.Code && classToAdd.Id != cl.Id)
                {
                    return false;
                }
            }

            return true;
        }
    }
}