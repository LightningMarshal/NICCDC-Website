# ProjectSaturn
###### ReadMe last edited by Kason Summers 4/13/2022

### Goal of Project
This project stems from the QResume project and its goal is to integrate most of the functionality to the NICCDC Website.

### Database Setup (Redacted due to ppi restrictions)
~~This project should have 3 database files (Stored Procedures, Tables, and Permissions) with it when the first working version of the database is ready.~~

!!! ~~The three files in question are in the NICCDC_SQL_Files folder within this project~~

**~~Using the 3 files included:~~**

	-Run the code within *QResume_Table_Creation*

	-Run the code within *QResume_Stored_Procedures*

	-Run the code within *QResume_Grant_Execute*

### Project Code
The current focus on the development side is to convert the main functionality over to user stored and add a submission feature, where the application is emailed.

### Side Notes:
Currently the naming scheme is consistant, but could be better on both the front and back ends. I attempted to keep the scheme consistant when naming stored procedures,
controllers, etc.

### Future TODO List
Use ctrl + shift + f to find all instances of //TODO in order to find all of the open Todos.

(Redacted without Server)
~~-Add Analyst User features
	-Add analyst page
	-Add a user table for analyst users
		-A default admin user with large password
	-Add analyst add feature for analysts to add new users
~~
-Determine best data storage option that is on the client side
-Add new controller to compose an email with the data submission
-Test the new solution

-Add functionality to ensure users with same email are found and merged
