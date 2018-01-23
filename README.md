# GoatJira
**GoatJira** is a Sparx Enterprise Architect Addin for synchronizing JIRA issues with the EA repository. Currently is being developed.

## Requirements:
* Sparx Enterprise Architect
* Atlasian JIRA account
* .NET Framework 4.5.2 or higher

## Licence:
* Goat image used in about dialog is copyrighted by [Leona Štastná](http://www.leona-stastna.cz) and you cannot use it without her permission.
* Rest of files are under MIT licence.

## Usage:
You can find all commands in Extend ribbon or in context menu in diagram or Project Browser.

![Ribbon](http://rydval.cz/res/GitHub/GoatJira/Ribbon.png)

First of all, define login information:

![Login Information](http://rydval.cz/res/GitHub/GoatJira/LoginInformation.png)

Login information are defined for every project (repository) and are stored in application data directory. 

Now, you can define JQL for any package:

![Package Connection Settings](http://rydval.cz/res/GitHub/GoatJira/PackageConnectionSettings.png)

Your favourite step: Read/Refresh issues in package. The progress is shown in System output window:

![System Output](http://rydval.cz/res/GitHub/GoatJira/SystemOutput.png)

Now, when you double click on jira issue element, you can see its properties:

![Goat Issue](http://rydval.cz/res/GitHub/GoatJira/JiraIssue.png)

You are ready to use the issues elements in your model. For instance, create relations such as realization to build the traceability.

![Diagram](http://rydval.cz/res/GitHub/GoatJira/MyIssuesDiagram.png)

For this, use new diagram type.

![Diagram Type](http://rydval.cz/res/GitHub/GoatJira/DiagramType.png)

The Jira diagram has its own tool box set:

![Tool Box](http://rydval.cz/res/GitHub/GoatJira/Toolbox.png)


## Download binaries:
* [binaries](http://rydval.cz/tmp/GoatJira.zip) - download the file, unzip it and as an administrator run install.bat
