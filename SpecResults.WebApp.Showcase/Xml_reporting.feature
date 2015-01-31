@Plugin@Plugin
Feature: XML reporting
	Story
	=====
	In order to get testresults in XML format
	As a developer
	I want to learn about [SpecResults.Xml](https://www.nuget.org/packages/SpecResults.Xml/)

@Howto:install
Scenario: Learn how to install the SpecResults.Xml package
	Given I'm on "http://nuget.org"
	When I enter searchtext "SpecResults.Xml" in "searchBoxInput"
	And I click the search button "searchBoxSubmit"	
	And I click the result with title "SpecResults.Xml"
	Then I can read the instructions on how to install the package