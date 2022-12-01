Feature: Create a company
	As an authenticated user,
	I can create a company

Scenario: Not being authenticated returns the expected error status code
	When a user creates a company named "Company"
	Then an unauthorized error status code is returned