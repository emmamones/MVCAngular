using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataModel
{
    public partial class Customer
    {
        public static List<Customer> Create(MembershipType[] memberships )
        {
            List<Customer> customers =new List<Customer> () {
                new Customer {
                    Name = "Alberto",BirthDate = Convert.ToDateTime("01/01/1984")
                    , IsSubscribedToNewsLetter = false
                    , Created = DateTime.Now
                    , CreatedBy = "seed"
                    , MembershipType = memberships[0] }
                ,new Customer {
                    Name = "Federico",  BirthDate = Convert.ToDateTime("01/01/1986 ")
                    , IsSubscribedToNewsLetter = true
                    , Created = DateTime.Now
                    , CreatedBy = "seed"
                    , MembershipType = memberships[1] }
                ,new Customer {
                    Name = "Roberto",  BirthDate = Convert.ToDateTime("01/01/1986 ")
                    ,IsSubscribedToNewsLetter = true
                    , Created = DateTime.Now
                    , CreatedBy = "seed"
                    , MembershipType = memberships[2] }
               , new Customer {
                   Name = "Ismael",  BirthDate = Convert.ToDateTime("01/01/1986 ")
                   ,  IsSubscribedToNewsLetter = true
                   , Created = DateTime.Now
                   , CreatedBy = "seed"
                   , MembershipType =memberships[3] }
            };

            return customers;
        }
    }
}
