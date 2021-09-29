using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPolygon.Models;

namespace ServerLogicLibrary
{
    public class SqlCrud
    {
        private readonly DataAccess dataAccess=new();
        private readonly string connectionString;

        public SqlCrud(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public PersonModel GetPersonById(int id)
        {
            string sql = @"select nu.Name
                            from NewUsers nu
                            inner join ExistingUsers eu on eu.PersonId=nu.Id
                            where eu.PersonId=@Id";
            string userName = dataAccess.LoadData<string, dynamic>(sql,new { Id=id},connectionString).First();
            return null;
        }

        public void CreateNewPerson(PersonModel person)
        {
            string sql = "Insert into NewUsers(Name,Password,EmailAddress,PersonImage) values(@Name,@Password,@EmailAddress,@PersonImage)";
            dataAccess.SaveData(sql,new { person.Name,person.Password,person.EmailAddress,person.PersonImage},connectionString);
        }

        public bool CheckIsUserValid(string name, string password)
        {
            string sql = "select Id from NewUsers where Name=@Name and Password=@Password";
            int? isUserExist = dataAccess.LoadData<int?, dynamic>(sql,new { Name=name,Password=password},connectionString).First();
            if (isUserExist == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
