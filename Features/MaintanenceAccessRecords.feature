Feature: Access Employee Records

Access Employee Records
Background: 
	Given the user navigates to login page
	When the user enters valid Username and valid password in the Username and password field
	And clicks on login button

@Maintanence
Scenario: View employee records by name
	Given user is logged in successfully and on homepage
	When user hovers on maintanence option and clicks on Access Records
	Then authenicate password option is displayed
	And user enters the valid password and clicks on submit button
	And user enters employee name and clicks on search button
	Then user should see employee Id

@Maintanence
Scenario: View employee records should not be visible when user enters wrong password
	Given user is logged in successfully and on homepage
	When user hovers on maintanence option and clicks on Access Records
	Then authenicate password option is displayed
	And user enters the wrong password and clicks on submit button
	Then user cannot access the employee records

@Maintanence
Scenario: Purge candidate records for vacancy type should be purged upon choosing purge
	Given user is logged in successfully and on homepage
	When user hovers on maintanence option and clicks on Candidate Records in Purge Records
	Then authenicate password option is displayed
	And user enters the valid password and clicks on submit button
	And user selects "<vacancy>" from drop down
	And clicks on Search button
	Then Selected vacancy type should be displayed
	And user clicks on purge records
	And user clicks on "Purge" in confirmation message
	Then candidate records or purged successfully
Examples:
| vacancy                  |
| Junior Account Assistant |
| Sales Representative     |
| Payroll Administrator    |

@Maintanence
Scenario: Purge candidate records for vacancy type should be visible choosing cancel
	Given user is logged in successfully and on homepage
	When user hovers on maintanence option and clicks on Candidate Records in Purge Records
	Then authenicate password option is displayed
	And user enters the valid password and clicks on submit button
	And user selects "Associate IT Manager" from drop down
	And clicks on Search button
	Then Selected vacancy type should be displayed
	And user clicks on purge records
	And user clicks on "Cancel" in confirmation message
	Then candidate records are not purged