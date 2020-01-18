<h1>Git Workflow</h1>
<p>This tutorial/walkthrough will help with understanding the workflow when working with the GitHub repository. If you are not familiar with git, I'd recommend watching <a href="https://www.youtube.com/watch?v=0fKg7e37bQE">this video</a> (or finding your own video on youtube). Git is a common source control tool used by almost every software team in any environment (academia, industry, etc.).

<h2>High-Level Workflow</h2>
<p>The overall workflow is described below:
<ol>
    <li>Create a feature branch from the <code>master</code> branch</li>
    <li>Work in your feature branch until you have completed your user story</li>
    <li>Submit a pull request for your feature branch into master, essentially attempting to merge to master.</li>
    <li>Once reviewed, if changes must be made, complete those changes</li>
    <li>Touch base with the team notifying them of the changes and "gotchas" to look for.</li>
</ol>
<p>Now, we will go into more detail over each step of the process.

<h2>Clone the Repository</h2>
<b>If you already have the repo cloned, ignore this.</b>
<p>First, make sure the repo is cloned! Can't really work on code you don't have. 
<ol>
    <li>I'd recommend setting up your workspace with <a href="https://docs.gitlab.com/ee/ssh/">SSH Keys</a>. Refer to the "Generating a new SSH key pair" section of the code.</li>
    <li>Open GitHub. Click your Profile->Settings. Now click "SSH and GPG Keys". Ensure you don't have an existing key from the device you are using. Note that attu/cse machines will need their own SSH key separate from your local device. Attu/the CSE machines all share the same SSH key.</li>
    <li>Click "New SSH Key".</li>
    <li>Enter a descriptive title like "Ravi's Laptop" or "Attu/CSE Machines".</li>
    <li>Copy your public key that you generated in the first step.
    <ol>
        <li>Go to your home directory: <code>cd ~</code></li>
        <li>Open the ssh folder: <code>cd .ssh</code></li>
        <li>Copy the public key. You can view the contents like this: <code>cat id_rsa.pub</code></li>
    </ol>
    </li>
    <li>Paste the public key into the <b>Key</b> section in GitHub. Click Add.</li>
    <li>Clone the repo in your local machine:
    <ol>
        <li>Go to the repository and select "Clone/Download" on GitHub.</li>
        <li>Make sure the new popup shows "Clone using SSH". If it doesn't, select "Use SSH" in the popup.</li>
        <li>Click the clipboard icon that will copy the link for you.</li>
        <li>Go back to your terminal and enter the following: <code>git clone {link}</code></li>
    </ol>
    </li>
</ol>
<p>Perfect! You've got the repo cloned with SSH now!

<h2>Create a feature branch</h2>
<p>Everyone should be backing up their own work, but sometimes people will be working on the same field. To deal with this, everyone works in feature branches. Not only does this protect the master branch until you can confirm the functionality of your feature. It also allows you to backup your branch as much as you want and work in your own space without interfering with others! Trust me, it's worth it.
<ol>
    <li>Navigate to your repo in terminal (Git Bash/Ubuntu)</li>
    <li>Make sure you are on the master branch: <code>git checkout master</code></li>
    <li>Update your master branch: <code>git pull</code></li>
    <li>Create the feature branch <i>locally</i>: <code>git checkout -b {featureBranchName}</code></li>
    <li>Push the feature branch to origin: <code>git push -u origin {featureBranchName}</code></li>
</ol>

<h2>Working in the feature branch</h2>
<p>Great! Now you have your feature branch and we can all see it. Now, make commit and push often to your branch. That way if you break anything, you can get back to a good, working verison of your feature.
<b>Always pull before working on stuff. This will make your life easier.</b>
 <code>git pull</code>

<p>Follow the following steps to commit/push changes to origin.
<ol>
    <li>Make sure you are in your git repo in your terminal.</li>
    <li>Check the status of the repo: <code>git status</code>. The files in red have been changed but haven't been commited/pushed to git, so they aren't really backed up yet. There might be some red files under a section called "Untracked Files". These files were never being tracked by git in the first place, so it is up to you whether you think you need to add them. If you make a new view file, you will probably want to add it. If it is some output directory, we don't add stuff like that.</li>
    <li>Add the necessary files: <code>git add {fileName}</code>.</li>
    <li>Check the status of the repo after adding: <code>git status</code>. The files marked in green will be part of your next commit. Confirm that the correct files are there.</li>
    <li>If you want to restore any files to the state they are in on origin, run <code>git checkout {filename}</code>. Be careful, this will remove all changes you've made to it since the last commit.</li>
    <li>You are ready to commit! <code>git commit -m "Insert Title" -m "Insert Description"</code></li>
    <li>If successful, push to origin: <code>git push</code></li>
</ol>
<p>That's how you commit/push your changes to origin. Now, we can always revert your code back to any commit that you push. This is great when things break or we make mistakes using git.
    
<h2>Submit a Pull Request</h2>
<p>A pull request will allow you to merge your changes into the master branch cleanly. It requires someone to review your code, ensuring the correctness of the feature you added. Most likely, that reviewer will be an administrator.
<ol>
    <li>Ensure your feature branch is up-to-date in GitHub</li>
    <li>Go to the repo on GitHub.com and switch branch to your feature branch.</li>
    <li>Click "New Pull Request" next to the branch name.</li>
    <li>Select "Create Pull Request"</li>
    <li>Resolve any merge conflicts on your end if possible</li>
    <li>Wait for review/approval for your request</li>
    <li>Eventually, your pull request will get merged. Once merged, pull changes down onto your local machine on the master branch and ensure everything is still working properly.</li>
    <li>If you need to update the team about your feature, do so now as it's is part of the production code.</li>
</ol>

<h2>[WARNING] When Playing With Git</h2>
<p>There should NEVER be a time when you are trying to switch from your current branch to the master branch and try to do a merging yourself.
<ol>
    <li>If you find yourself to be in the master branch (say you did <code>git checkout master</code> at any point), what you should NEVER do is to perform a <code>git merge {yourBranch}</code>.</li>
</ol>
</p>

<h2>Summary</h2>
<p>You now should be able to work in your feature branches and complete user stories as needed. Final words:
<ul>
    <li>Commit early and often! It is a terrible feeling to lose progress on your code because you unintentionally introduced a bug. Commiting a bunch allows you to get back to a working state of your code or track where the bug was introduced.</li>
    <li>Do not submit changes for files you did not need to change in the first place. This makes tracking hard. What I like to do is do git checkouts on files I didn't need to change and then verifying that my new feature still works.</li>
    <li>This whole workflow is tough to grasp at first, but I promise similar approaches are used in industry (and even class) so it's better to get used to it now!</li>
    <li>Don't be worried about breaking the code. With git, we can always revert back to any commit in any branch. Code away!</li>
</ul>
