# Hello Welcome to the team 27 Repository

This Repository is based on a 2d fighting game, this game will

be built in c# with the Unity game engine. This

In order to use this repository, you need to be part of the collaborators (get in touch with Tahseen)

 clone the repository.

 # Making changes to your own branch

 To create a branch in git use the command 

 - git checkout -b newBranchName

 # Pushing your changes to the Remote Repository 

 - git add . 

 - git commit -m "Your commit"

 - git push origin your branch

 - Make a fork

 - create a pull request

 The scrum master will take care of merging your changes into the master branch

# Getting the latest change from the remote repository

    Someone may have pushed his changes to his remote branch then got these changes merged. In order to keep yourself up to date you may pull from the main branch. To make sure you have the previous at the top of your next commit, this is the command to use.
  - git pull --rebase origin main.

# Working with your commits

    You may have used the git add . and git commit -m "your commit" command but not being happy of the changes. You can revert the commit by using git rebase -i HEAD~n (for n the number of commits to be displayed). you will be able to use d,f,s,e etc...

    d: delete
    f: fix
    s: squash
    e: edit