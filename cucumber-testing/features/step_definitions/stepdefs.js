const { Builder, By, Capabilities, Key } = require('selenium-webdriver');
const { Given, When, Then, AfterAll } = require('@cucumber/cucumber');
const { expect } = require('chai');

require("chromedriver");

const capabilities = Capabilities.chrome();
capabilities.set('chromeOptions', { "w3c": false });
const driver = new Builder().withCapabilities(capabilities).build();

Given('I have access to the scrum-board-tool', async function () {
    await driver.get('https://scrumboardtool.azurewebsites.net/');
});

When('I press New Project', async function () {
    await driver.findElement(By.id('newproject')).click();
});

When('I enter name and description and press save', async function () {
    await driver.findElement(By.name("namefield")).sendKeys("TestProjectCucumber");
    await driver.findElement(By.name("descriptionfield")).sendKeys("This is a test description");
    await driver.findElement(By.name("save")).click();
});

Then('New Project gets created', async function () {
    // Write code here that turns the phrase above into concrete actions
    return 'pending';
});

AfterAll(async function () {
    await driver.quit();
});