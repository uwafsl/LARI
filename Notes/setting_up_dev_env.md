<h1>LARI - Laboratory Reconciliation and Information System</h1>
<h2>Setting up the development environment</h2>
    <ol>
        <li><h3>Download Visual Studio Community Edition (or other)</h3></li>
            <p>We are developing everything in Visual Studio for this project. Visual Studio works great with C#, and the lab has most of its experience with Visual Studio rather than another text editor. If you prefer a different IDE, feel free to use it.
            <p>If you are in the CSE major, keep in mind you get access to <a href="https://www.cs.washington.edu/lab/software">Visual Studio Enterprise Edition</a> already. Otherwise, download the <a href="https://visualstudio.microsoft.com/vs/community/">Visual Studio Community Edition (Free)</a>.
        <li><h3>Download the UWSDK from Perforce</h3></li>
            <p>Obviously, this implies that you already have Perforce setup. If you do not have it setup, please contact Dr. Lum or the Flight Operations Director to get setup.
            <p>The UWSDK holds the backend of our project. If you are not familiar with the terms "frontend" or "backend", I found a <a href="https://www.coursereport.com/blog/front-end-development-vs-back-end-development-where-to-start">blog post</a> that did a good job showing the differences. For CSE students, it's important to know the difference, especially at career fairs as recruiters will ask which you prefer (If you don't know, full-stack is always the safest option.)
        <li><h3>Build the UWSDK</h3></li>
            <p>Building the SDK will basically put all of our backend code into ONE magical file - a .dll file. We will obviously need to connect the frontend of our project to this file to have our application running.
            <ol>
                <li>Open the UW.sln file in perforce with Visual Studio:</li>
                <p><code>Perforce/UWSDK/UWSDK/UW.sln</code>
                <li>On the top menubar, click Build->Build Solution. This step will take a while.</li>
                <li>Once done, you should see that every build completed successfully in the project.</li>
            </ol>
        <li><h3>Clone the LARI frontend from GitHub</h3></li>
            <p>We call downloading the code from github (or any git server) "cloning" in the software world. There are two options I would recommend. I'd recommend the first one as git is a tool used by every software company, and using the terminal will allow you to really understand how to use git. The two options are shown <a href="https://help.github.com/articles/cloning-a-repository/">here</a>. GitHub Desktop is the option that doesn't use terminal.
        <li><h3>Add Reference to the Backend</h3></li>
            <p>You're almost there!! Now we just need to add a couple references to our backend so the buttons on our application can know what to do.
            <ol>
                <li>Open the LARI.sln file in Visual Studio</li>
                <li>Right-click the <code>LARI</code> dropdown in the <i>Solution Explorer</i>. Note that this is not equivalent to the <i>Solution 'LARI' (1 project)</i> option.</li>
                <li>Select <i>Add...</i>, then <i>Reference...</i></li>
                <li>Select the <i>Browse...</i> button.</li>
                <li>Navigate to <code>Perforce/UWSDK/UWSDK/UWLARI/bin/Debug</code> and select the <code>UWLARI.dll</code> file.</li>
                <li>Now, follow the same steps as before to add another reference to <code>Perforce/UWSDK/UWSDK/UWWPF/bin/Debug/UWWPF.dll</code></li>
            </ol>
            <p>Perfect! You should be setup now. The final (and fun) step is coming up next.
        <li><h3>Build the Application and Run it!</h3></li>
            <p>Now that all the references and everything is setup, let's run the application.
            <ol>
                <li>Open the LARI.sln file in perforce with Visual Studio:</li>
                <p><code>Perforce/UWSDK/UWSDK/LARI.sln</code>
                <li>On the top menubar, click Build->Build Solution.</li>
                <li>Once done, you should see that every build completed successfully in the project.</li>
                <li>Now navigate to LARI/LARI/bin/Debug/LARI.exe or click the green <i>Start</i> arrow in Visual Studio to run the application!
            </ol>
    </ol>
    <p> Great job! You are now ready to develop software on the LARI project. Be sure that before you ever push changes to GitHub, you make sure that you are able to rebuild the solution!