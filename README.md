# ops300-deliver-it-well
## Introduction

Azure Pipelines is a SaaS tool that easily allows a programmer to add continuous integration to their projects. By writing unit tests in a project, this allows a development team to merge and push code several times a day without running into merge conflicts or broken code on production.

After integrating Azure with your project and writing unit tests, any push or merge to the master branch will kick off a few events:

    Azure will check for a .yml file describing the machine and what branches to deploy
    A server will be spun up to run your app
    All of your dependencies will be installed on that server
    The app will start running
    Any tests located in your test suite will all be automatically run
    The image of your app will be destroyed on the server and the server itself will be spun down
    If all of your tests passed, your merge or push will be allowed to happen. If one fails you will be notified and the code will not merge into your master branch.

As you can see, this is a really useful approach to multiple coders integrating their branches into production, as it enforces unit testing and can prevent a lot of bugs if you have a well written test suite.

In this project we're going to: - Sync Azure to our Github account - Write a new test - Have Azure run your tests every time you push up a new change to the master branch - Automatically push your project to Azure after a passing build
## Step 1 - Sync Azure

    Use the same clone and intitialize process (to Fork this private repository) to your github.
    Publish this project like you did for deploy-all-the-thangs
    Login to Azure, and make sure your Github account is linked to it.
    From within go to App Services and select your newly published app.
    Select "Deployment Center", then choose Github as your source and click continue.
    Select "Azure Pipelines" for your build provider and click continue again.
    Select your organization, repository, and branch you want to deploy from.
    For Azure DevOps Organization, you'll want to select new, then name it "SDCD -" + Your Name Here
    Click Continue, and then finally click finish.
    In your project folder, make a circle.yml file. This will configure how Azure will deploy and test your project. Use the following to fill it out, keeping in mind that indentation matters:

version: 2
jobs:
  build:
    docker:
      # specify the version you desire here
      - image: circleci/node:8.11

    working_directory: ~/repo

    steps:
      - checkout

      # Download and cache dependencies
      - restore_cache:
          keys:
          - v1-dependencies-{{ checksum "package.json" }}
          # fallback to using the latest cache if no exact match is found
          - v1-dependencies-

      - run: yarn install

      - save_cache:
          paths:
            - node_modules
          key: v1-dependencies-{{ checksum "package.json" }}

      # run tests!
      - run: yarn test

    Commit and push this file to your Github repo.
    Now when you make pushes to master on your Github account, it will trigger the Azure service.

## Step 2 - Write a New Test

    We have a couple of basic tests in your repo folder for this exercise. Take a look inside DeliverItWellTests.cs. You'll see that it check that the Message is set to "Your application description page." in the model of the About Razor page when OnGet() is called (ie. we got to the /About page).
    We'd like to setup another test. You can copy the last test and change what PageModel you want to test for a Message set with OnGet().
    In the related pages cshtml.cs file, change the Message in OnGet() to match the new string you're testing for in Step 2.

## Step 3 - Continuously Integrate

    Open up your Azure dashboard so you can see the process in real time in our next step.
    Commit your changes and push them to your repo on Github.
    In your Azure dashboard you can watch the process as your container is provisioned, your app is started, tested, and then broken down.
    If your tests pass green, if you missed something and your tests fail, Azure will prevent the commit from merging and alert you.

## Exit Criteria

    Your Github account is linked to your Azure account
    You wrote a new test to check for text on a Razor page served by the provided app
    When you push a commit or merge to the master branch of your repo, Azure runs your tests and lets you know if they passed
    Your project gets automatically deployed to Azure

And that's it! Submit your github URL using the link at the bottom to navigate to the submission page, and after that page sumbit your Azure URL.
## Bonus

    Figure out how to deploy to AWS instead of Azure

