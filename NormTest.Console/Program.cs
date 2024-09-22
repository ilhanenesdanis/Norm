using Norm.Library.Concrete;

SqlServerQuery sqlServerQuery = new SqlServerQuery();

var result = await sqlServerQuery.QueryAsync<string>(new Norm.Library.Core.Models.Query
{
    TableName = "Users",
    Column = "FirstName",
    Size = 50,

}, new Norm.Library.Core.Models.SqlConnectionModel
{
    Database = "LoginExample",
    IntegratedSecurity = true,
    Password = "",
    Server = "DESKTOP-0Q6K9DL",
    UserId = ""
});

