@Plugin
Feature: Json reporting
	Story
	=====
	In order to get testresults in JSON format
	As a developer
	I want to learn about [SpecResults.Json](https://www.nuget.org/packages/SpecResults.Json/)

@Howto:install
Scenario: Learn how to install the SpecResults.Json package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecResults.Json" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecResults.Json"
	Then I can read the instructions on how to install the package
