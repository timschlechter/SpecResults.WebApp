@Plugin@Plugin
Feature: Plain Text reporting
	Story
	=====
	In order to get testresults in plain text format
	As a developer
	I want to learn about [SpecResults.PlainText](https://www.nuget.org/packages/SpecResults.PlainText/)

@Howto:install
Scenario: Learn how to install the SpecResults.PlainText package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecResults.PlainText" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecResults.PlainText"
	Then I can read the instructions on how to install the package