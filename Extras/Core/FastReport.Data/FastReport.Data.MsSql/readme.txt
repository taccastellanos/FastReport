How to use it:
- execute the following code once at the application start:
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
- now you should be able to create a new MsSql data connection from code:
Report report = new Report(); 
report.Load(@"YourReport.frx");
//... 
MsSqlDataConnection conn = new MsSqlDataConnection();
conn.ConnectionString = "your connection string";
conn.CreateAllTables();
report.Dictionary.Connections.Add(conn);
