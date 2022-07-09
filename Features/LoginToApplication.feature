Feature: Login Open and Login Authorization

Login Scenarios
@Login
Scenario: Validate that the user is able to open the Login page
	Given the user navigates to login page
	Then Login page should be visible to the user
	Then user should be able to view the Username, Password and Login elements on the screen

@Login
Scenario: Validate that user is able to login successfully when entering valid credentials
	Given the user navigates to login page
	Then Login page should be visible to the user
	When the user enters valid Username and valid password in the Username and password field
	And clicks on login button
	Then user should navigate successfully to the default home page

@Login
Scenario: Validate that the user is able to view error message when entering invalid credentials
	Given the user navigates to login page
	Then Login page should be visible to the user
	When the user enters invalid Username and valid password in the Username and password field
	And clicks on login button
	Then user should be able to see Error Message of Invalid credentials on the screen

@Login
Scenario: Validate that user is able to successfully Logout from the application
	Given Logout button is present in the default home page
	When the user clicks on the Logout button
	Then Login page should be visible to the user

