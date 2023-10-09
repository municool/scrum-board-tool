Feature: Projects.
    Creating and managin Projects

    Scenario: Creating a new Project
        Given I have access to the scrum-board-tool
        Given I have the Projectoverview
        When I press "New Project"
        Then New Project View gets shown
        When I enter name and description and press save
        Then New Project gets created
