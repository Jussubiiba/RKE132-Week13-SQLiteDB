using System.Data.SQLite;

FindCustomer (CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;
}


static void FindCustomer(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;
    String searchName;
    Console.WriteLine("Enter a first name to display customer data:");
    searchName = Console.ReadLine();
    command = myConnection.CreateCommand();
    command.CommandText = $"SELECT customer.rowid, customer.firstname, customer.lastname, status.statustype FROM customerStatus JOIN customer ON customer.rowid = customerStatus.customerid JOIN status on status.rowid = customerStatus.statusid WHERE firstname LIKE '{searchName}'";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerStringName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringStatus = reader.GetString(3);
        Console.WriteLine($"Search result: ID: {readerRowid}. {readerStringName} {readerStringLastName}. Status: {readerStringStatus}");
    }
    myConnection.Close();
}