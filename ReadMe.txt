1. Car Selection Tool Web Application Installation Software Requrements

- Microsoft Visual Studio 2019.
- Microsoft SQL Server 2019 Express.
- SQL Server Management Studio.

3 Create a new database called "GridbeyondDataBase" in SQL Server.

2. Restore the GridbeyondDataBase.bak file to sql server.

	OR

	Using SQL Server Management Studio to restore using .mdf file

	- Open SSMS and go to "Object Explorer".

	- Right-click the database you need to restore and select "Attach". Click the "Add" button when "Attach Database" Windows appears.

	- Browse the location of MDF file and select it. Then, click "OK". And SQL Server Management Studio will restore the database from the MDF file.

3. Change the connection string accrodingly in web.config file.

 	Example : connectionString="Data Source=DESKTOP-2IIHPSL;Initial Catalog=CarTool;Integrated Security=True"

4. Open the project using visual studio and build the project.

5. Run the application.

6. Uplaod the sample .CSV file data using upload link in home page.

7. View the data by navigating to Dashboard tab.

8. View data according to the selected date from the dropdown box.