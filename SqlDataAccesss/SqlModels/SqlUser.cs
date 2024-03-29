﻿using EBanking.Models;
using System.Data;
using System.Data.SqlClient;

namespace SqlDataAccesss.SqlModels
{
    internal class SqlUser : SqlEntity
    {
        public User User { private get; set; }
        public SqlUser(User entity) : base(entity)
        {
            User = entity;
        }
        public SqlUser()
        {
        }
        public override string GetTableName() { return "[dbo].[User]"; }
        public override IEntity GetEntityFromSqlReader(SqlDataReader reader)
        {
            return new User()
            {
                Id = reader.GetInt32("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Email = reader.GetString("Email"),
                Password = reader.GetString("Password")
            };
        }
        public override void SetSqlInsertCommand(SqlCommand command)
        {
            command.CommandText = $"insert into {GetTableName()}(FirstName, LastName, Email, Password) output inserted.ID values (@firstname, @lastname, @email, @password)";
            command.Parameters.AddWithValue("@firstname", User.FirstName);
            command.Parameters.AddWithValue("@lastname", User.LastName);
            command.Parameters.AddWithValue("@email", User.Email);
            command.Parameters.AddWithValue("@password", User.Password);
        }
        public override void SetSqlUpdateByIdCommand(SqlCommand command)
        {
            command.CommandText = $"UPDATE {GetTableName()} SET FirstName = '{User.FirstName}', LastName = '{User.LastName}', Email = '{User.Email}', Password = '{User.Password}' WHERE Id = {User.Id}";
        }
        public override void SetSqlDeleteByIdCommand(SqlCommand command)
        {
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE id={User.Id}";
        }
        public override void SetSqlSelectByIdCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()} WHERE id={User.Id}";
        }
        public override void SetSqlSelectAllCommand(SqlCommand command)
        {
            command.CommandText = $"SELECT * FROM {GetTableName()}";
        }
        public void SetSqlSelectAllAccountsByUserId(SqlCommand command)
        {
            command.CommandText = "select a.Id, a.Balance, a.Status, a.Number, u.Id as userID, u.FirstName, u.LastName, u.Email, u.Password, c.Id as currencyID, c.Name, c.Code " +
                $"from {new SqlAccount().GetTableName()} as a INNER JOIN {GetTableName()} as u ON (a.UserId = u.Id) INNER JOIN {new SqlCurrency().GetTableName()} as c ON (a.CurrencyId = c.Id) WHERE u.Id = " + User.Id;
        }
    }
}
