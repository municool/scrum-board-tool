Feature: Projects.
    Creating and managin Projects

    Scenario: Creating a new Project
        Given I have access to the scrum-board-tool
        When I press New Project
        When I enter name and description and press save
        Then New Project gets created
