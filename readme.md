# ProjectJanus
###### ReadMe last edited by Kason Summers 4/13/2022

### Goal of Project
This project stems from ProjectSaturn and its goal is to convert the NICCDC Website to Microsoft ASP.Net Core MVC.

### Cookie Storage
This project uses a cookie to store a list of models for all of the current models. These cookies need to be as
protected as possible due to them containing ppi. Please keep this in mind when administering changes to code that 
affects these cookies.

### Project Code
The current focus of this project is to convert the exsisting html pages to cshtml then connect them to the MVC 
controllers. The code that comes from ProjectSaturn will also just need finalizing before deployment.

### Side Notes:
Currently the naming scheme is consistant, but could be better on both the front and back ends. I attempted to 
keep the scheme consistant when naming stored procedures, controllers, etc.

### Future TODO List
Use ctrl + shift + f to find all instances of //TODO in order to find all of the open Todos.

### Conversion Details:
This section was created to better explain the changes from ProjectSaturn to ProjectJanus.

Files that are from RegularPages (NICCDC-Website) root are now located in /Views/NICCDC
Files that are from about_niatec are now located in /Views/NIATEC


##### File Conversion:
For the conversion, a new controller was created for each of the possible nests that a webpage could be under.
Within each controller is a set of actions that correspond to their pages. These actions will serve the pages,
which are actually located under /View/{controller name} in the project directory. These controllers and 
actions can be found below.

NICCDC Controller
	-Index
	-Media
	-About Niatec
	-Niccdc
	-Participants
	-Sponsors
	-Stay Connected
	-Volunteers

NIATEC Controller * This controller is having layout issues
	-Alumni * This page is currently under construction
	-Apply
	-Program * This page is currently under construction
	-Speakers

##### Header and Footers
With the transition to MVC, we have gained the functionality of Layouts. These are files that we can use to maintain
dynamic navigation headers and constant footer. The code that allows the switching is located in 
Views/_ViewStart.cshtml. One portion will render the header and the other will render the footer.
